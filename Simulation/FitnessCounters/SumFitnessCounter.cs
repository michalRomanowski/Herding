using Agent;
using Auxiliary;
using System;
using System.Collections.Generic;
using World;
using Teams;
using MathNet.Spatial.Euclidean;

namespace Simulations
{
    internal class SumFitnessCounter : IFitnessCounter
    {
        private CountFitnessParameters parameters;

        public SumFitnessCounter(CountFitnessParameters parameters)
        {
            this.parameters = parameters;
        }

        public double CountFitness(Team team)
        {
            if (parameters.PositionsOfShepherdsSet.Count != parameters.PositionsOfSheepSet.Count)
                throw new ArgumentException();
            
            var fitness = 0.0;

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

        private double CountFitness(
            Team team,
            int TurnsOfHerding,
            IList<Vector2D> positionsOfShepherds,
            IList<Vector2D> positionsOfSheep,
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

        private double CountFitness(IList<IList<Vector2D>> sheepPositionsRecord)
        {
            double fitness = 0;

            foreach (var positionsOfSheepAtTime in sheepPositionsRecord)
            {
                fitness += positionsOfSheepAtTime.SumOfDistancesFromCenter();
            }

            return fitness;
        }
    }
}
