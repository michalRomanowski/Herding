using Agent;
using Auxiliary;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using Teams;

namespace Simulations
{
    public abstract class FitnessCounter : IFitnessCounter
    {
        protected CountFitnessParameters parameters;

        public FitnessCounter(CountFitnessParameters parameters)
        {
            this.parameters = parameters;
        }

        public double CountFitness(Team team, int seed, bool verbose = false)
        {
            Random r = new Random(seed);

            var fitness = 0.0;

            for (int i = 0; i < parameters.PositionsOfShepherdsSet.Count; i++)
            {
                var seedForSheep = r.Next();

                var sheep = AgentFactory.GetSheep(parameters.PositionsOfSheepSet[i], parameters.SheepType, seedForSheep);

                team.ClearPath();
                team.SetPositions(parameters.PositionsOfShepherdsSet[i]);
                
                var countFitness = CountFitness(team, sheep);

                if (verbose)
                    Log(i, countFitness, seedForSheep, parameters.PositionsOfShepherdsSet[i], parameters.PositionsOfSheepSet[i]);

                if (!double.IsNaN(fitness))
                    fitness += countFitness;
            }

            return fitness;
        }

        protected abstract double CountFitness(Team team, IList<IMovingAgent> sheep);

        protected void Log(
            int setIndex, double fitness, int seed, 
            IEnumerable<Vector2D> positionsOfShepherds, 
            IEnumerable<Vector2D> positionsOfSheep)
        {
            Logger.Instance.AddLine("index: " + setIndex);
            Logger.Instance.AddLine("seed: " + seed);
            Logger.Instance.AddLine("fitness: " + fitness.ToString("0.######"));

            Logger.Instance.AddLine("positions of shepherds:");
            foreach(var positionOfShepherd in positionsOfShepherds)
                Logger.Instance.AddLine(positionOfShepherd.X + " " + positionOfShepherd.Y);

            Logger.Instance.AddLine("positions of sheep:");
            foreach (var positionOfSheep in positionsOfSheep)
                Logger.Instance.AddLine(positionOfSheep.X + " " + positionOfSheep.Y);
        }
    }
}