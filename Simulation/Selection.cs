using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teams;

namespace Simulations
{
    class SelectionResult
    {
        public readonly IEnumerable<Team> Winners;
        public readonly IEnumerable<Team> Losers;

        public SelectionResult(IEnumerable<Team> winners, IEnumerable<Team> losers)
        {
            this.Winners = winners;
            this.Losers = losers;
        }
    }

    class Selection
    {
        private SimulationParameters simulationParameters;
        private Population population;
        
        private const int NUMBER_OF_TOURNAMENTS = 2;

        public Selection(SimulationParameters simulationParameters, Population population)
        {
            this.simulationParameters = simulationParameters;
            this.population = population;
        }

        public SelectionResult Select()
        {
            var results = RunTournaments();

            return new SelectionResult(
                results.Select(x => x.First()),
                results.Select(x => x.Last()));
        }

        private IEnumerable<IEnumerable<Team>> RunTournaments()
        {
            var tournamentTasks = InitTournamentTasks();

            foreach (var t in tournamentTasks)
                t.Start();

            Task.WhenAll(tournamentTasks).Wait();
            
            return tournamentTasks.Select(x => x.Result);
        }

        private Task<IEnumerable<Team>>[] InitTournamentTasks()
        {
            return InitTournaments().Select(x => new Task<IEnumerable<Team>>(() => x.Attend())).ToArray();
        }

        private IEnumerable<ITournament> InitTournaments()
        {
            var participants = population.GetRandomUniqueSubsets(NUMBER_OF_TOURNAMENTS, simulationParameters.NumberOfParticipants);

            return participants.Select(x => TournamentFactory.GetTournament(simulationParameters, x));
        }
    }
}
