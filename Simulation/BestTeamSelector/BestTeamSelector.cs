using System.Collections.Generic;
using System.Linq;
using Auxiliary;
using Teams;

namespace Simulations
{
    class TeamWithFitness
    {
        public Team Team;
        public double Fitness;

        public TeamWithFitness(Team team, double fitness)
        {
            Team = team;
            Fitness = fitness;
        }
    }

    class BestTeamSelector
    {
        private readonly IFitnessCounter fitnessCounter;

        public BestTeamSelector(IFitnessCounter fitnessCounter)
        {
            this.fitnessCounter = fitnessCounter;
        }

        public TeamWithFitness GetBestTeam(IEnumerable<Team> teams)
        {
            var bestTeamWithFitness = teams
                .AsParallel()
                .Select(x => new TeamWithFitness(x, fitnessCounter.CountFitness(x)))
                .OrderBy(x => x.Fitness).First();

            Logger.Instance.AddLine("Best fitness: " + bestTeamWithFitness.Fitness);

            return bestTeamWithFitness;
        }
    }
}
