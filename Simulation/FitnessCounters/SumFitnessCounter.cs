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
            Team shepherdsTeam,
            SimulationParameters simulationParameters,
            IList<IList<Position>> positionsOfShepherdsSet,
            IList<IList<Position>> positionsOfSheepSet,
            ESheepType sheepType,
            int seed = 0)
        {
            if (positionsOfShepherdsSet.Count != positionsOfSheepSet.Count)
                throw new ArgumentException();
            
            float fitness = 0.0f;

            for (int i = 0; i < positionsOfShepherdsSet.Count; i++)
            {
                fitness += CountFitness(
                    shepherdsTeam,
                    simulationParameters,
                    positionsOfShepherdsSet[i],
                    positionsOfSheepSet[i],
                    sheepType,
                    seed);
            }

            return fitness;
        }

        private float CountFitness(
            Team shepherds,
            SimulationParameters simulationParameters,
            IList<Position> positionsOfShepherds,
            IList<Position> positionsOfSheep,
            ESheepType sheepType,
            int seed = 0)
        {
            var sheep = AgentFactory.GetSheep(positionsOfSheep, sheepType, seed);

            shepherds.ClearPath();
            shepherds.SetPositions(positionsOfShepherds);

            var world = new SimulationWorld(shepherds, sheep, true);

            world.Work(simulationParameters.TurnsOfHerding);

            return CountFitness(world.SheepPositionsRecord);
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
