using System;
using System.Collections.Generic;
using Agent;
using Auxiliary;
using Repository;
using Simulations;
using MathNet.Spatial.Euclidean;

namespace HerdingSimConsole
{
    class Program
    {
        private static XMLRepository repository = new XMLRepository(AppDomain.CurrentDomain.BaseDirectory);

        private static OptimizationParameters optimizationParameters;
        private static Population population;
        private static CountFitnessParameters controlFitnessParameters;

        private readonly static Dictionary<string, Action> commmands = new Dictionary<string, Action>
        {
            { "generate", Generate},
            { "list", () => Logger.Instance.AddLine(String.Join("\n", commmands.Keys))},
            { "load", Load},
            { "load population", LoadPopulation},
            { "load optimization parameters", LoadOptimizationParameters},
            { "execute optimization parameters", ExecuteOptimizationParameters},
            { "load control parameters", LoadControlParameters},
            { "save", Save},
            { "start", Start}
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

        private static void LoadPopulation()
        {
            Logger.Instance.AddLine("Insert full path of population:");
            var populationPath = Console.ReadLine();
            Logger.Instance.AddLine(populationPath);

            population = repository.LoadPopulationByPath(populationPath, optimizationParameters);
        }

        private static void LoadControlParameters()
        {
            Logger.Instance.AddLine("Insert full path of control parameters:");
            var controlParametersPath = Console.ReadLine();
            Logger.Instance.AddLine(controlParametersPath);

            controlFitnessParameters = repository.LoadOptimizationParametersByPath(controlParametersPath).GetCountFitnessParameters();
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
            Logger.Instance.AddLine("Starting optimization");
            if (population == null)
                population = new Population(optimizationParameters);

            new Optimization(optimizationParameters, population, repository, controlFitnessParameters).Start();
        }
    }
}
