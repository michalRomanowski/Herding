using System.Collections.Generic;
using Agent;
using Auxiliary;

namespace Teams
{
    public abstract class Team : ICloneable<Team>
    {
        public int Id { get; set; }

        public List<ThinkingAgent> Members { get; set; }

        public float Fitness { get; set; }
        
        public Team()
        {
            Members = new List<ThinkingAgent>();
        }

        public abstract Team GetClone();
        
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

        public abstract void Resize(int newSize);
        
        public abstract void ResizeNeuralNet(int numberOfSeenShepherds, int numberOfSeenSheep, int numberOfHiddenLayers, int hiddenLayerSize);

        public void ClearPath()
        {
            foreach (var a in Members)
            {
                a.Path.Clear();
            }
        }
    }
}
