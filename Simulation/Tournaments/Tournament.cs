using System.Collections.Generic;
using System.Linq;
using Auxiliary;
using Teams;

namespace Simulations
{
    class Tournament : ITournament
    {
        public IEnumerable<Team> participants = new List<Team>();

        private readonly IList<IList<Position>> positionsOfShepherdsSet;
        private readonly IList<IList<Position>> positionsOfSheepSet;

        private IFitnessCounter fitnessCounter;

        private readonly SimulationParameters simulationParameters;

        public Tournament(SimulationParameters simulationParameters, IEnumerable<Team> participants)
        {
            this.simulationParameters = simulationParameters;

            fitnessCounter = FitnessCounterFactory.GetFitnessCounter(simulationParameters);

            this.participants = participants;
        }

        public Tournament(SimulationParameters simulationParameters, IList<IList<Position>> positionsOfShepherdsSet, IList<IList<Position>> positionsOfSheepSet, IEnumerable<Team> participants)
            : this(simulationParameters, participants)
        {
            this.positionsOfShepherdsSet = positionsOfShepherdsSet;
            this.positionsOfSheepSet = positionsOfSheepSet;
        }

        
        public IEnumerable<Team> Attend()
        {
            var seed = CRandom.Instance.Next();

            foreach (var t in participants)
            {
                t.Fitness = fitnessCounter.CountFitness(
                    t,
                    simulationParameters,
                    positionsOfShepherdsSet,
                    positionsOfSheepSet,
                    simulationParameters.SheepType,
                    seed);
            }

            return participants.OrderBy(x => x.Fitness);
        }
    }
}
