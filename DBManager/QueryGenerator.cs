using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBManager
{
    static class QueryGenerator
    {
        public static string InsertIntoPopulation(string tableName, string unit)
        {
            return $"INSERT INTO {tableName} (Unit) VALUES ('{unit}');";
        }

        public static StringBuilder InsertIntoPopulation(string tableName, IEnumerable<string> units)
        {
            var query = new StringBuilder();

            query.Append($"INSERT INTO {tableName} (Unit) VALUES ");
            
            foreach (var u in units)
            {
                query.Append($"('{u}')");
                query.Append(", ");
            }

            query.Length -= 2;
            query.Append(";");

            return query;
        }
    }
}
