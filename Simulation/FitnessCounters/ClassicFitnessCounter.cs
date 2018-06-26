using System.Linq;
using Agent;
using Auxiliary;
using System;
using System.Collections.Generic;
using World;
using Team;

namespace Simulation
{
    public class FinalFitnessCounter : IFitnessCounter
    {
        public float CountFitness(
            ITeam team,
            SimulationParameters simulationParameters,
            List<Position> positionsOfShepards, 
            List<Position> positionsOfSheep,
            List<Position> positionsOfWolfs,
            ESheepType sheepType, 
            int seed)
        {
            var sheep = SheepProvider.GetSheep(positionsOfSheep, sheepType, seed);

            team.ClearPath();
            team.SetPositions(positionsOfShepards);

            var world = new SimulationWorld(team, sheep, new IdenticalTeam());

            world.Work(simulationParameters.TurnsOfHerding);

            return CenterOfGravityCalculator.SumOfDistancesFromCenterOfGravity(world.Sheep.Select(x => x.Position).ToList());
        }

        public float CountFitness(
            ITeam team,
            SimulationParameters simulationParameters,
            List<List<Position>> positionsOfShepardsSet,
            List<List<Position>> positionsOfSheepSet,
            List<List<Position>> positionsOfWolfsSet,
            ESheepType sheepType,
            int seed)
        {
            if(positionsOfShepardsSet.Count != positionsOfSheepSet.Count || positionsOfShepardsSet.Count != positionsOfWolfsSet.Count)
                throw new ArgumentException("positionsOfShepardsSet.Count != positionsOfSheepSet.Count || positionsOfShepardsSet.Count != positionsOfWolfsSet.Count");

            float fitness = 0.0f;

            for (int i = 0; i < positionsOfShepardsSet.Count; i++)
            {
                fitness += CountFitness(
                    team,
                    simulationParameters,
                    positionsOfShepardsSet[i],
                    positionsOfSheepSet[i],
                    positionsOfWolfsSet[i],
                    sheepType,
                    seed);
            }

            return fitness;
        }
    }
}
