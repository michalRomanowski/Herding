using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teams;

namespace Simulations
{
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

        public Tuple<Team, Team> Select()
        {
            var results = RunTournaments();

            return new Tuple<Team, Team>(
                results.Item1.First(),
                results.Item2.First());
        }

        private Tuple<IEnumerable<Team>, IEnumerable<Team>> RunTournaments()
        {
            var tournamentTasks = InitTournamentTasks();

            foreach (var t in tournamentTasks)
                t.Start();

            Task.WhenAll(tournamentTasks).Wait();
            
            var results = tournamentTasks.Select(x => x.Result);

            return new Tuple<IEnumerable<Team>, IEnumerable<Team>>(results.First(), results.Last());
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
