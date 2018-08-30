using System.Linq;
using Agent;
using Auxiliary;
using System;
using System.Collections.Generic;
using World;
using Teams;

namespace Simulations
{
    public class FinalFitnessCounter : IFitnessCounter
    {
        public float CountFitness(
            Team team,
            SimulationParameters simulationParameters,
            IList<Position> positionsOfShepards, 
            IList<Position> positionsOfSheep,
            ESheepType sheepType, 
            int seed)
        {
            var sheep = SheepProvider.GetSheep(positionsOfSheep, sheepType, seed);

            team.ClearPath();
            team.SetPositions(positionsOfShepards);

            var world = new SimulationWorld(team, sheep);

            world.Work(simulationParameters.TurnsOfHerding);

            return CenterOfGravityCalculator.SumOfDistancesFromCenterOfGravity(world.Sheep.Select(x => x.Position).ToList());
        }

        public float CountFitness(
            Team team,
            SimulationParameters simulationParameters,
            IList<IList<Position>> positionsOfShepardsSet,
            IList<IList<Position>> positionsOfSheepSet,
            ESheepType sheepType,
            int seed)
        {
            if(positionsOfShepardsSet.Count != positionsOfSheepSet.Count)
                throw new ArgumentException("positionsOfShepardsSet.Count != positionsOfSheepSet.Count");

            float fitness = 0.0f;

            for (int i = 0; i < positionsOfShepardsSet.Count; i++)
            {
                fitness += CountFitness(
                    team,
                    simulationParameters,
                    positionsOfShepardsSet[i],
                    positionsOfSheepSet[i],
                    sheepType,
                    seed);
            }

            return fitness;
        }
    }
}
