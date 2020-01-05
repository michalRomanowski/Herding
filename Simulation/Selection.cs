using System.Collections.Generic;
using System.Linq;
using Auxiliary;
using Teams;

namespace Simulations
{
    class SelectionResult
    {
        public readonly IList<Team> Winners;
        public readonly IList<Team> Losers;

        public SelectionResult(
            IList<Team> winners,
            IList<Team> losers)
        {
            Winners = winners;
            Losers = losers;
        }
    }

    class Selection
    {
        private readonly OptimizationParameters optimizationParameters;
        private readonly Population population;
        private readonly int numberOfWinners;

        public Selection(OptimizationParameters optimizationParameters, Population population, int numberOfWinners)
        {
            this.optimizationParameters = optimizationParameters;
            this.population = population;
            this.numberOfWinners = numberOfWinners;
        }

        public SelectionResult Select()
        {
            var participants = population.Units
                .OrderBy(x => CRandom.Instance.Next())
                .Take(optimizationParameters.NumberOfParticipants);

            var results = participants
                .AsParallel()
                .OrderBy(x => FitnessCounterFactory.GetFitnessCounter(optimizationParameters.GetCountFitnessParameters()).CountFitness(x))
                .ToList();

            return new SelectionResult(
                results.Take(numberOfWinners).ToList(),
                results.Skip(numberOfWinners).ToList());
        }
    }
}
