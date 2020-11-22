using System;
using System.Collections.Generic;
using Auxiliary;
using Repository;
using Simulations;
using Simulations.Parameters;
using Teams;

namespace HerdingSimConsole
{
    class Program
    {
        private static XMLRepository repository = new XMLRepository(AppDomain.CurrentDomain.BaseDirectory);

        private static OptimizationParameters optimizationParameters;
        private static Population population;
        private static IFitnessCounter controlFitnessCounter;
        private static Team team;

        private readonly static Dictionary<string, Action> commmands = new Dictionary<string, Action>
        {
            { "generate", Generate },
            { "list", () => Logger.Instance.AddLine(String.Join("\n", commmands.Keys)) },
            { "load", Load },
            { "load population", LoadPopulation },
            { "load team", LoadTeam },
            { "load optimization parameters", LoadOptimizationParameters },
            { "load control fitness parameters", LoadControlFitnessParameters },
            { "execute optimization parameters", ExecuteOptimizationParameters },
            { "save", Save },
            { "start", Start },
            { "test team", TestTeam },
            { "continue", Continue }
        };

        static void Main(string[] args)
        {
            while (ExecuteCommand()) {}
        }

        private static bool ExecuteCommand()
        {
            Logger.Instance.AddLine("Awaiting Command:");
            var command = Console.ReadLine();
            Logger.Instance.AddLine(command);

            if (command.Equals("exit"))
                return false;

            try
            {
                commmands[command.ToLower()]();
            }
            catch(Exception ex)
            {
                Logger.Instance.AddLine(ex.Message);
                commmands["list"]();
            }

            return true;
        }

        private static void Generate()
        {
            foreach(var optimizationParametersSample in Samples.OptimizationParametersSamples)
            {
                Logger.Instance.AddLine($"Generating sample: {optimizationParametersSample.Key}");

                var populationSample = new Population(optimizationParametersSample.Value);

                repository.Save(optimizationParametersSample.Key, optimizationParametersSample.Value, populationSample);
            }
        }

        private static void Load()
        {
            Logger.Instance.AddLine("Insert name of simulation:");
            var name = Console.ReadLine();
            Logger.Instance.AddLine(name);

            optimizationParameters = repository.LoadOptimizationParameters(name);
            population = repository.LoadPopulation(name, optimizationParameters);
        }

        private static void LoadOptimizationParameters()
        {
            Logger.Instance.AddLine("Insert full path of optimization parameters:");
            var optimizationParametersPath = Console.ReadLine();
            Logger.Instance.AddLine(optimizationParametersPath);

            optimizationParameters = repository.LoadOptimizationParametersByPath(optimizationParametersPath);
        }

        private static void LoadControlFitnessParameters()
        {
            Logger.Instance.AddLine("Insert full path of control fitness parameters:");
            var controlFitnessParametersPath = Console.ReadLine();
            Logger.Instance.AddLine(controlFitnessParametersPath);
            var controlFitnessParameters = repository.LoadOptimizationParametersByPath(controlFitnessParametersPath);

            controlFitnessCounter = FitnessCounterFactory.GetFitnessCounterForBest(controlFitnessParameters);
        }

        private static void LoadPopulation()
        {
            Logger.Instance.AddLine("Insert full path of population:");
            var populationPath = Console.ReadLine();
            Logger.Instance.AddLine(populationPath);

            population = repository.LoadPopulationByPath(populationPath, optimizationParameters);
        }

        private static void LoadTeam()
        {
            Logger.Instance.AddLine("Insert full path of team:");
            var teamPath = Console.ReadLine();
            Logger.Instance.AddLine(teamPath);

            team = repository.LoadTeam(teamPath, optimizationParameters);
        }

        private static void ExecuteOptimizationParameters()
        {
            Logger.Instance.AddLine("Insert full path of optimization parameters:");
            var optimizationParametersPath = Console.ReadLine();
            Logger.Instance.AddLine(optimizationParametersPath);

            optimizationParameters = repository.LoadOptimizationParametersByPath(optimizationParametersPath);

            Start();
        }

        private static void Save()
        {
            Logger.Instance.AddLine("Insert name of saved simulation:");
            var name = Console.ReadLine();
            Logger.Instance.AddLine(name);

            repository.Save(name, optimizationParameters, population);
        }

        private static void Start()
        {
            if(optimizationParameters == null)
            {
                Logger.Instance.AddLine("Attempt to load optimization paramaters from default location");
                optimizationParameters = repository.LoadOptimizationParametersByPath("OptimizationParameters.xml");
            }

            if (population == null)
            {
                Logger.Instance.AddLine("Generating new population");
                population = new Population(optimizationParameters);
            }

            Logger.Instance.AddLine("Starting optimization");
            new Optimization(optimizationParameters, population, repository, controlFitnessCounter).Optimize();
        }

        private static void TestTeam()
        {
            if (optimizationParameters == null)
            {
                Logger.Instance.AddLine("No optimization parameters loaded");
                return;
            }

            if (team == null)
            {
                Logger.Instance.AddLine("No team loaded");
                return;
            }

            Logger.Instance.AddLine("Starting test");

            var fitnessCounter = FitnessCounterFactory.GetFitnessCounterForBest(optimizationParameters);

            var result = fitnessCounter.CountFitness(team, optimizationParameters.SeedForRandomSheepForBest, true);

            Logger.Instance.AddLine($"Total Fitness: {result}");
        }

        private static void Continue()
        {
            var name = repository.GetNewestSimulationName();

            if (string.IsNullOrEmpty(name))
            {
                Logger.Instance.AddLine("No saved simulation to continue");
            }

            optimizationParameters = repository.LoadOptimizationParameters(name);
            population = repository.LoadPopulation(name, optimizationParameters);

            Start();
        }
    }
}