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
        private CountFitnessParameters parameters;

        public SumFitnessCounter(CountFitnessParameters parameters)
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
            int seed = 0)
        {
            var sheep = AgentFactory.GetSheep(positionsOfSheep, sheepType, seed);

            team.ClearPath();
            team.SetPositions(positionsOfShepherds);

            var world = new SimulationWorld(team, sheep, true);

            world.Work(TurnsOfHerding);

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
