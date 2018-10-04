using Agent;
using Auxiliary;
using System;
using System.Collections.Generic;
using World;
using Teams;

namespace Simulations
{
    internal class SumFitnessCounter : IFitnessCounter
    {
        public float CountFitness(
            Team shepards,
            SimulationParameters simulationParameters,
            IList<Position> positionsOfShepards,
            IList<Position> positionsOfSheep,
            ESheepType sheepType,
            int seed = 0)
        {
            var sheep = SheepProvider.GetSheep(positionsOfSheep, sheepType, seed);

            shepards.ClearPath();
            shepards.SetPositions(positionsOfShepards);

            var world = new SimulationWorld(shepards, sheep, true);

            world.Work(simulationParameters.TurnsOfHerding);

            return CountFitness(world.SheepPositionsRecord);
        }

        public float CountFitness(
            Team shepardsTeam,
            SimulationParameters simulationParameters,
            IList<IList<Position>> positionsOfShepardsSet,
            IList<IList<Position>> positionsOfSheepSet,
            ESheepType sheepType,
            int seed = 0)
        {
            if (positionsOfShepardsSet.Count != positionsOfSheepSet.Count)
                throw new ArgumentException("positionsOfShepardsSet.Count != positionsOfSheepSet.Countt");


            float fitness = 0.0f;

            for (int i = 0; i < positionsOfShepardsSet.Count; i++)
            {
                fitness += CountFitness(
                    shepardsTeam,
                    simulationParameters,
                    positionsOfShepardsSet[i],
                    positionsOfSheepSet[i],
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
                fitness += positionsOfSheepAtTime.SumOfDistancesFromCenter();
            }

            return fitness;
        }
    }
}
