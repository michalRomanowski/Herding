using Agent;
using Auxiliary;
using System.Collections.Generic;
using Teams;

namespace Simulations
{
    public interface IFitnessCounter
    {
        float CountFitness(
            Team team,
            SimulationParameters simulationParameters,
            IList<Position> positionsOfShepherds,
            IList<Position> positionsOfSheep,
            ESheepType sheepType,
            int seed);

        float CountFitness(
            Team team,
            SimulationParameters simulationParameters,
            IList<IList<Position>> positionsOfShepherdsSet,
            IList<IList<Position>> positionsOfSheepSet,
            ESheepType sheepType,
            int seed);
    }
}
