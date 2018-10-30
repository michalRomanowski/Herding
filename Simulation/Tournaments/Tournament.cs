using System.Collections.Generic;
using System.Linq;
using Teams;

namespace Simulations
{
    class Tournament : ITournament
    {
        private IEnumerable<Team> participants = new List<Team>();
        private IFitnessCounter fitnessCounter;

        public Tournament(IFitnessCounter fitnessCounter, IEnumerable<Team> participants)
        {
            this.fitnessCounter = fitnessCounter;
            this.participants = participants;
        }

        public IEnumerable<Team> Attend()
        {
            foreach (var t in participants)
            {
                t.Fitness = fitnessCounter.CountFitness(t);
            }

            return participants.OrderBy(x => x.Fitness);
        }
    }
}
