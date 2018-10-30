using System.Collections.Generic;
using System.Linq;
using Teams;

namespace Simulations
{
    class BestTeamSelector : IBestTeamSelector
    {
        private readonly IFitnessCounter fitnessCounter;

        public BestTeamSelector(IFitnessCounter fitnessCounter)
        {
            this.fitnessCounter = fitnessCounter;
        }

        public Team GetBestTeam(IEnumerable<Team> teams)
        {
            return TournamentFactory.GetTournament(fitnessCounter, teams).Attend().First();
        }
    }
}
