using System;
using Auxiliary;
using Simulations.OptimizationStep;

namespace Simulations
{
    public class Optimization
    {
        private const int AUTOSAVE_FREQUENCY = 1000;
        private const string DATETIME_FORMAT = "yyyyMMddHHmmss";

        private double bestFitness = double.MaxValue;
        private double controlFitness = double.MaxValue;

        private readonly OptimizationParameters parameters;
        private readonly Population shepherds;
        
        private ISimulationRepository repository;

        private readonly IFitnessCounter controlFitnessCounter;

        public Optimization(OptimizationParameters parameters, 
            Population shepherds, 
            ISimulationRepository repository) : this(parameters, shepherds, repository, null)
        { }

        public Optimization(OptimizationParameters parameters,
            Population shepherds,
            ISimulationRepository repository,
            IFitnessCounter controlFitnessCounter)
        {
            this.parameters = parameters;
            this.shepherds = shepherds;
            this.repository = repository;
            this.controlFitnessCounter = controlFitnessCounter;
        }

        public void Optimize()
        {
            repository.Save($"START_{DateTime.Now.ToString(DATETIME_FORMAT)}", parameters, shepherds);

            for (int era = 0; era < parameters.NumberOfEras && bestFitness > parameters.TargetFitness; era++)
            {
                Step();
                
                if (era % AUTOSAVE_FREQUENCY == 0)
                    repository.Save(DateTime.Now.ToString(DATETIME_FORMAT), parameters, shepherds);

                Logger.Instance.AddLine("Era: " + era);
                Logger.Instance.AddLine("Time: " + DateTime.Now.ToString());
                Logger.Instance.AddLine("Best fitness: " + bestFitness);
                if(controlFitnessCounter != null)
                {
                    Logger.Instance.AddLine("Control fitness: " + controlFitness);
                }
            }

            repository.Save($"END_{DateTime.Now.ToString(DATETIME_FORMAT)}", parameters, shepherds);
        }

        private void Step()
        {
            //var step = new ClassicOptimizationStep(parameters, shepherds);
            var step = new SimplifiedOptimizationStep(parameters, shepherds);
            step.Step();

            if (bestFitness != step.BestFitness && controlFitnessCounter != null)
                controlFitness = controlFitnessCounter.CountFitness(shepherds.Best);

            bestFitness = step.BestFitness;
        }
    }
}
