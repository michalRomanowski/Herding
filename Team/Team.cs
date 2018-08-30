using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Agent;
using Auxiliary;

namespace Teams
{
    public abstract class Team
    {
        public int Id { get; set; }

        public List<ThinkingAgent> Members { get; set; }

        public float Fitness { get; set; }
        
        public Team()
        {
            Members = new List<ThinkingAgent>();
        }
        
        public abstract Team Clone();

        public abstract void AdjustSize(int newSize);

        public void ResetFitness()
        {
            Fitness = float.MaxValue;
        }

        public abstract Team[] Crossover(Team partner);

        public abstract void Mutate(float mutationPower, float absoluteMutationFactor);

        public void SetPositions(IList<Position> positions)
        {
            foreach (Position p in positions)
            {
                Members[positions.IndexOf(p)].Position = new Position(p);
            }
        }

        public void AdjustInputLayerSize(int numberOfSeenShepards, int numberOfSeenSheep)
        {
            foreach(var agent in Members)
            {
                agent.AdjustInputLayerSize(numberOfSeenShepards, numberOfSeenSheep);
            }
        }

        public void AdjustHiddenLayersSize(int newSize)
        {
            foreach (var agent in Members)
            {
                agent.AdjustHiddenLayersSize(newSize);
            }
        }

        public void ClearPath()
        {
            foreach (var a in Members)
            {
                a.Path.Clear();
            }
        }
    }
}
