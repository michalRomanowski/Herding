using System.Collections.Generic;
using System.Linq;
using Auxiliary;
using Teams;

namespace Simulations
{
    class SelectionParameters
    {
        public OptimizationParameters OptimizationParameters;
        public Population Population;
    }

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
        private const int WINNERS = 2;

        private readonly SelectionParameters parameters;
        
        public Selection(SelectionParameters parameters)
        {
            this.parameters = parameters;
        }

        public SelectionResult Select()
        {
            var participants = parameters.Population.Units
                .OrderBy(x => CRandom.Instance.Next())
                .Take(parameters.OptimizationParameters.NumberOfParticipants);

            var results = participants
                .AsParallel()
                .OrderBy(x => FitnessCounterFactory.GetFitnessCounter(parameters.OptimizationParameters.GetCountFitnessParameters()).CountFitness(x))
                .ToList();

            return new SelectionResult(
                results.Take(WINNERS).ToList(),
                results.Skip(WINNERS).ToList());
        }
    }
}
