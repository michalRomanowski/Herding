using Agent;
using Auxiliary;
using System.Collections.Generic;
using Team;

namespace Simulation
{
    public interface IFitnessCounter
    {
        float CountFitness(
            ITeam team,
            SimulationParameters simulationParameters,
            List<Position> positionsOfShepards,
            List<Position> positionsOfSheep,
            List<Position> positionsOfWolfs,
            ESheepType sheepType,
            int seed);

        float CountFitness(
            ITeam team,
            SimulationParameters simulationParameters,
            List<List<Position>> positionsOfShepardsSet,
            List<List<Position>> positionsOfSheepSet,
            List<List<Position>> positionsOfWolfsSet,
            ESheepType sheepType,
            int seed);
    }
}
