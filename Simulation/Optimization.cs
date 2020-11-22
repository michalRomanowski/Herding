using System;
using Auxiliary;
using Simulations.OptimizationStep;
using Simulations.Parameters;

namespace Simulations
{
    public class Optimization
    {
        private const int AUTOSAVE_FREQUENCY = 100;
        private const string DATETIME_FORMAT = "yyyyMMddHHmmss";

        private double bestFitness = double.MaxValue;
        private double controlFitness = double.MaxValue;

        private readonly OptimizationParameters parameters;
        private readonly Population population;
        
        private readonly ISimulationRepository repository;

        private readonly IFitnessCounter controlFitnessCounter;

        public Optimization(OptimizationParameters parameters, 
            Population population, 
            ISimulationRepository repository) : this(parameters, population, repository, null)
        { }

        public Optimization(OptimizationParameters parameters,
            Population population,
            ISimulationRepository repository,
            IFitnessCounter controlFitnessCounter)
        {
            this.parameters = parameters;
            this.population = population;
            this.repository = repository;
            this.controlFitnessCounter = controlFitnessCounter;
        }

        public void Optimize()
        {
            repository.Save($"START_{DateTime.Now.ToString(DATETIME_FORMAT)}", parameters, population);

            while(parameters.CurrentEra < parameters.NumberOfEras && bestFitness > parameters.TargetFitness)
            {
                Step(parameters.CurrentEra);
                
                if (parameters.CurrentEra % AUTOSAVE_FREQUENCY == 0)
                    repository.Save(DateTime.Now.ToString(DATETIME_FORMAT), parameters, population);

                Logger.Instance.AddLine("Era: " + parameters.CurrentEra);
                Logger.Instance.AddLine("Time: " + DateTime.Now.ToString());
                Logger.Instance.AddLine("Best fitness: " + bestFitness);
                if(controlFitnessCounter != null)
                {
                    Logger.Instance.AddLine("Control fitness: " + controlFitness);
                }

                parameters.CurrentEra++;
            }

            repository.Save($"END_{DateTime.Now.ToString(DATETIME_FORMAT)}", parameters, population);
        }

        private void Step(int stepNumber)
        {
            //var step = new ClassicOptimizationStep(parameters, population);
            var step = new SimplifiedOptimizationStep(parameters, population);
            step.Step(stepNumber);

            if (bestFitness != step.BestFitness && controlFitnessCounter != null)
                controlFitness = controlFitnessCounter.CountFitness(population.Best, parameters.SeedForRandomSheepForBest);

            bestFitness = step.BestFitness;
        }
    }
}