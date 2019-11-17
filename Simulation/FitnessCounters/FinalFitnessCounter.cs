using System.Linq;
using Agent;
using Auxiliary;
using System;
using System.Collections.Generic;
using World;
using Teams;
using MathNet.Spatial.Euclidean;

namespace Simulations
{
    public class FinalFitnessCounter : IFitnessCounter
    {
        private readonly CountFitnessParameters parameters;

        public FinalFitnessCounter(CountFitnessParameters parameters)
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
                    parameters.PositionsOfShepherdsSet[i],
                    parameters.PositionsOfSheepSet[i]);
            }

            return fitness;
        }

        private double CountFitness(
            Team team,
            IList<Vector2D> positionsOfShepherds, 
            IList<Vector2D> positionsOfSheep)
        {
            var sheep = AgentFactory.GetSheep(positionsOfSheep, parameters.SheepType, parameters.Seed);

            team.ClearPath();
            team.SetPositions(positionsOfShepherds);

            var world = new SimulationWorld(team, sheep, false);

            world.Work(parameters.TurnsOfHerding);

            return world.Sheep.Select(x => x.Position).SumOfDistancesFromCenter();
        }
    }
}
