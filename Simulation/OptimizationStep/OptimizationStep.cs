using System.Collections.Generic;
using System.Linq;
using Teams;

namespace Simulations.OptimizationStep
{
    public abstract class OptimizationStep
    {
        public Team BestTeam { get; private set; }
        public double BestFitness { get; private set; }

        protected readonly OptimizationParameters parameters;
        protected readonly Population population;
        protected readonly IFitnessCounter bestFitnessCounter;

        protected const double ABSOLUTE_MUTATION_FACTOR = 1.0;

        protected OptimizationStep(OptimizationParameters parameters, Population population)
        {
            this.BestFitness = double.MaxValue;
            this.parameters = parameters;
            this.population = population;
            bestFitnessCounter = FitnessCounterFactory.GetFitnessCounter(parameters.GetCountFitnessParameters());
        }

        public abstract void Step();
        
        protected void UpdateBestTeam(IList<Team> pretenders)
        {
            pretenders.Add(population.Best);

            var bestPretenderWithFitness = GetBestTeam(pretenders);

            if (population.Best != bestPretenderWithFitness.Team)
            {
                population.Best = bestPretenderWithFitness.Team.GetClone();
            }

            BestFitness = bestPretenderWithFitness.Fitness;
        }

        private (Team Team, double Fitness) GetBestTeam(IEnumerable<Team> teams)
        {
            return teams
                .AsParallel()
                .Select(x => (Team: x, Fitness: bestFitnessCounter.CountFitness(x)))
                .OrderBy(x => x.Fitness).First();
        }
    }
}
