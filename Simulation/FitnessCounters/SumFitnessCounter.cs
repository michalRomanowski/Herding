using Agent;
using Auxiliary;
using System;
using System.Collections.Generic;
using World;
using Team;

namespace Simulation
{
    internal class SumFitnessCounter : IFitnessCounter
    {
        public float CountFitness(
            ITeam shepards,
            SimulationParameters simulationParameters,
            List<Position> positionsOfShepards,
            List<Position> positionsOfSheep,
            List<Position> positionsOfWolfs,
            ESheepType sheepType,
            int seed = 0)
        {
            var sheep = SheepProvider.GetSheep(positionsOfSheep, sheepType, seed);

            shepards.ClearPath();
            shepards.SetPositions(positionsOfShepards);

            var world = new SimulationWorld(shepards, sheep, new IdenticalTeam(), true);

            world.Work(simulationParameters.TurnsOfHerding);

            return CountFitness(world.SheepPositionsRecord);
        }

        public float CountFitness(
            ITeam shepardsTeam,
            SimulationParameters simulationParameters,
            List<List<Position>> positionsOfShepardsSet,
            List<List<Position>> positionsOfSheepSet,
            List<List<Position>> positionsOfWolfsSet,
            ESheepType sheepType,
            int seed = 0)
        {
            if (positionsOfShepardsSet.Count != positionsOfSheepSet.Count || positionsOfShepardsSet.Count != positionsOfWolfsSet.Count)
                throw new ArgumentException("positionsOfShepardsSet.Count != positionsOfSheepSet.Count || positionsOfShepardsSet.Count != positionsOfWolfsSet.Count");


            float fitness = 0.0f;

            for (int i = 0; i < positionsOfShepardsSet.Count; i++)
            {
                fitness += CountFitness(
                    shepardsTeam,
                    simulationParameters,
                    positionsOfShepardsSet[i],
                    positionsOfSheepSet[i],
                    positionsOfWolfsSet[i],
                    sheepType,
                    seed);
            }

            return fitness;
        }

        private float CountFitness(IList<IList<Position>> sheepPositionsRecord)
        {
            float fitness = 0.0f;

            foreach (var positionsOfSheepAtTime in sheepPositionsRecord)
            {
                fitness += CenterOfGravityCalculator.SumOfDistancesFromCenterOfGravity(positionsOfSheepAtTime);
            }

            return fitness;
        }
    }
}
