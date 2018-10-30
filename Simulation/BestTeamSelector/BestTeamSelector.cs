using System.Collections.Generic;
using System.Linq;
using Teams;

namespace Simulations
{
    class BestTeamSelector : IBestTeamSelector
    {
        private readonly IFitnessCounter fitnessCounter;

        public BestTeamSelector(IFitnessCounter fitnessCounter, CountFitnessParameters countFitnessParameters)
        {
            this.fitnessCounter = fitnessCounter;
        }

        public Team GetBestTeam(IEnumerable<Team> teams)
        {
            foreach (var t in teams)
            {
                t.Fitness = fitnessCounter.CountFitness(t);
            }

            var minFitness = teams.Min(x => x.Fitness);

            return teams.Where(x => x.Fitness == minFitness).First();
        }
    }
}
