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
            IList<IList<Position>> positionsOfShepherdsSet,
            IList<IList<Position>> positionsOfSheepSet,
            ESheepType sheepType,
            int seed)
        {
            if (positionsOfShepherdsSet.Count != positionsOfSheepSet.Count)
                throw new ArgumentException();

            float fitness = 0.0f;

            for (int i = 0; i < positionsOfShepherdsSet.Count; i++)
            {
                fitness += CountFitness(
                    team,
                    simulationParameters,
                    positionsOfShepherdsSet[i],
                    positionsOfSheepSet[i],
                    sheepType,
                    seed);
            }

            return fitness;
        }

        private float CountFitness(
            Team team,
            SimulationParameters simulationParameters,
            IList<Position> positionsOfShepherds, 
            IList<Position> positionsOfSheep,
            ESheepType sheepType, 
            int seed)
        {
            var sheep = AgentFactory.GetSheep(positionsOfSheep, sheepType, seed);

            team.ClearPath();
            team.SetPositions(positionsOfShepherds);

            var world = new SimulationWorld(team, sheep, false);

            world.Work(simulationParameters.TurnsOfHerding);

            return world.Sheep.Select(x => x.Position).SumOfDistancesFromCenter();
        }
    }
}
