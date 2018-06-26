using System.Collections.Generic;
using System.Linq;
using System.IO;
using Agent;
using Auxiliary;
using System.Text;
using System;

namespace Team
{
    public abstract class Team : ITeam
    {
        public IList<IThinkingAgent> Members { get; private set; }

        public float Fitness { get; set; }
        
        public Team()
        {
            Members = new List<IThinkingAgent>();
        }
        
        public abstract ITeam Clone();

        public abstract void AdjustSize(int newSize);

        public void ResetFitness()
        {
            Fitness = float.MaxValue;
        }

        public abstract ITeam[] Crossover(ITeam partner);

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

        public string Compress()
        {
            StringBuilder compressed = new StringBuilder();

            foreach (var m in Members)
            {
                compressed.Append(m.Compress());
            }

            return compressed.ToString();
        }
    }
}
