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
        
        public Tournament(SimulationParameters simulationParameters, IList<IList<Position>> positionsOfShepherdsSet, IList<IList<Position>> positionsOfSheepSet, IEnumerable<Team> participants)
        {
            fitnessCounter = FitnessCounterFactory.GetFitnessCounter(simulationParameters.FitnessType,
               new CountFitnessParameters()
               {
                   TurnsOfHerding = simulationParameters.TurnsOfHerding,
                   PositionsOfShepherdsSet = positionsOfShepherdsSet,
                   PositionsOfSheepSet = positionsOfSheepSet,
                   SheepType = simulationParameters.SheepType,
                   Seed = CRandom.Instance.Next()
               });

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
