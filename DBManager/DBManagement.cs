using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DBManager
{
    public static class DBManagement
    {
        private const string CONNECTION_STRING_POPULATIONS = "Data Source=Populations.db;Version=3;";
        private const string CONNECTION_STRING_SIMULATIONS_PARAMETERS = "Data Source=SimulationsParameters.db;Version=3;";

        public static void SavePopulation(string tableName, IEnumerable<string> units)
        {
            using (var dbConnection = new SQLiteConnection(CONNECTION_STRING_POPULATIONS))
            {
                dbConnection.Open();
                
                using (var dropTableCommand = new SQLiteCommand($"DROP TABLE IF EXISTS {tableName};", dbConnection))
                using (var createPopulationTableCommand = new SQLiteCommand($"CREATE TABLE {tableName}(Unit TEXT NOT NULL);", dbConnection))
                using (var insertIntoPopulationCommand = new SQLiteCommand(QueryGenerator.InsertIntoPopulation(tableName, units).ToString(), dbConnection))
                {
                    dropTableCommand.ExecuteNonQuery();
                    createPopulationTableCommand.ExecuteNonQuery();
                    insertIntoPopulationCommand.ExecuteNonQuery();
                }
            }
        }

        public static IEnumerable<string> LoadPopulation(string tableName)
        {
            var population = new List<string>();

            using (var dbConnection = new SQLiteConnection(CONNECTION_STRING_POPULATIONS))
            {
                dbConnection.Open();

                using (SQLiteDataReader reader = new SQLiteCommand($"SELECT * FROM {tableName};", dbConnection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        population.Add(Convert.ToString(reader["Unit"]));
                    }
                }
            }

            return population;
        }

        public static IEnumerable<string> LoadSimulationsNames()
        {
            var simulationsNames = new List<string>();

            using (var dbConnection = new SQLiteConnection(CONNECTION_STRING_SIMULATIONS_PARAMETERS))
            {
                dbConnection.Open();

                using (SQLiteDataReader reader = new SQLiteCommand($"SELECT * FROM sqlite_master WHERE type = 'table';", dbConnection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        simulationsNames.Add(Convert.ToString(reader["tbl_name"]));
                    }
                }
            }

            return simulationsNames;
        }

        public static void SaveSimulationParameters(string tableName, string simulationParameters)
        {
            using (var dbConnection = new SQLiteConnection(CONNECTION_STRING_SIMULATIONS_PARAMETERS))
            {
                dbConnection.Open();
                
                using (var dropTableCommand = new SQLiteCommand($"DROP TABLE IF EXISTS {tableName};", dbConnection))
                using (var createTableCommand = new SQLiteCommand($"CREATE TABLE {tableName}(SimulationParameters TEXT NOT NULL);", dbConnection))
                using (var insertCommand = new SQLiteCommand($"INSERT INTO {tableName} (SimulationParameters) VALUES ('{simulationParameters}');", dbConnection))
                {
                    dropTableCommand.ExecuteNonQuery();
                    createTableCommand.ExecuteNonQuery();
                    insertCommand.ExecuteNonQuery();
                }
            }
        }

        public static string LoadSimulationsParameters(string tableName)
        {
            string simulationParameters;

            using (var dbConnection = new SQLiteConnection(CONNECTION_STRING_SIMULATIONS_PARAMETERS))
            {
                dbConnection.Open();

                using (SQLiteDataReader reader = new SQLiteCommand($"SELECT * FROM {tableName};", dbConnection).ExecuteReader())
                {
                    reader.Read();
                    simulationParameters = Convert.ToString(reader["SimulationParameters"]);
                }
            }

            return simulationParameters;
        }
    }
}
