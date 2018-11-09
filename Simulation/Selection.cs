using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams;

namespace Simulations
{
    class SelectionParameters
    {
        public SimulationParameters SimulationParameters;
        public Population Population;
        public int NumberOfTournaments;
        public int WinnersPerTournament;
        public int LosersPerTournament;
    }

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
        private readonly SelectionParameters parameters;
        
        public Selection(SelectionParameters parameters)
        {
            this.parameters = parameters;
        }

        public SelectionResult Select()
        {
            var results = RunTournaments();

            return new SelectionResult(
                results.SelectMany(x => x.Take(parameters.WinnersPerTournament)),
                results.SelectMany(x => x.Skip(x.Count() - parameters.LosersPerTournament)));
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
            var participants = parameters.Population.GetRandomUniqueSubsets(parameters.NumberOfTournaments, parameters.SimulationParameters.NumberOfParticipants);

            return participants.Select(x => TournamentFactory.GetTournament(parameters.SimulationParameters, x));
        }
    }
}
