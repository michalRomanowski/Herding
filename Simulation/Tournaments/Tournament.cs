using System.Collections.Generic;
using System.Linq;
using Auxiliary;
using Teams;

namespace Simulations
{
    class Tournament : ITournament
    {
        private IEnumerable<Team> participants = new List<Team>();
        private IFitnessCounter fitnessCounter;
        
        public Tournament(EFitnessType fitnessType, CountFitnessParameters countFitnessParameters, IEnumerable<Team> participants)
        {
            this.fitnessCounter = FitnessCounterFactory.GetFitnessCounter(fitnessType, countFitnessParameters);
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
