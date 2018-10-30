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
        private CountFitnessParameters parameters;

        public FinalFitnessCounter(CountFitnessParameters parameters)
        {
            this.parameters = parameters;
        }


        public float CountFitness(Team team)
        {
            if (parameters.PositionsOfShepherdsSet.Count != parameters.PositionsOfSheepSet.Count)
                throw new ArgumentException();

            float fitness = 0.0f;

            for (int i = 0; i < parameters.PositionsOfShepherdsSet.Count; i++)
            {
                fitness += CountFitness(
                    team,
                    parameters.TurnsOfHerding,
                    parameters.PositionsOfShepherdsSet[i],
                    parameters.PositionsOfSheepSet[i],
                    parameters.SheepType,
                    parameters.Seed);
            }

            return fitness;
        }

        private float CountFitness(
            Team team,
            int TurnsOfHerding,
            IList<Position> positionsOfShepherds, 
            IList<Position> positionsOfSheep,
            ESheepType sheepType, 
            int seed)
        {
            var sheep = AgentFactory.GetSheep(positionsOfSheep, sheepType, seed);

            team.ClearPath();
            team.SetPositions(positionsOfShepherds);

            var world = new SimulationWorld(team, sheep, false);

            world.Work(TurnsOfHerding);

            return world.Sheep.Select(x => x.Position).SumOfDistancesFromCenter();
        }
    }
}
