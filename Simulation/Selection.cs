using System.Collections.Generic;
using System.Linq;
using Auxiliary;
using Simulations.Parameters;
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
        private readonly Population population;
        private readonly int numberOfWinners;
        private readonly int numberOfParticipants;
        private readonly IFitnessCounter fitnessCounter;

        public Selection(OptimizationParameters optimizationParameters, Population population, int numberOfWinners)
        {
            this.population = population;
            this.numberOfWinners = numberOfWinners;
            this.numberOfParticipants = optimizationParameters.NumberOfParticipants;

            var randomSets = new RandomSetsList(
                optimizationParameters.NumberOfRandomSets,
                optimizationParameters.NumberOfShepherds,
                optimizationParameters.NumberOfSheep,
                StaticRandom.R.Next());

            this.fitnessCounter = FitnessCounterFactory.GetFitnessCounterForTraining(optimizationParameters);
        }

        public SelectionResult Select()
        {
            var participants = population.Units
                .OrderBy(x => StaticRandom.R.Next())
                .Take(numberOfParticipants);
            
            var results = participants
                .AsParallel()
                .OrderBy(x => fitnessCounter.CountFitness(x, StaticRandom.R.Next()))
                .ToList();

            return new SelectionResult(
                results.Take(numberOfWinners).ToList(),
                results.Skip(numberOfWinners).ToList());
        }
    }
}