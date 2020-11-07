using System.Collections.Generic;
using Agent;
using Auxiliary;
using MathNet.Spatial.Euclidean;

namespace Teams
{
    public abstract class Team : ICloneable<Team>
    {
        public List<ThinkingAgent> Members { get; set; }

        public Team()
        {
            Members = new List<ThinkingAgent>();
        }

        public abstract void Init(Team other);

        public abstract Team GetClone();

        public abstract Team Crossover(Team partner);
        
        public abstract void Mutate(double mutationPower);

        public void SetPositions(IList<Vector2D> positions)
        {
            for(int i = 0; i < positions.Count; i++)
            {
                Members[i].Position = new Vector2D(positions[i].X, positions[i].Y);
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