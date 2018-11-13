﻿using System.Collections.Generic;
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

        public Team(TeamParameters parameters)
        {
            Members = new List<ThinkingAgent>() { AgentFactory.GetShepherd(parameters.ShepherdParameters) };

            Resize(parameters.NumberOfShepherds);
        }

        public abstract Team GetClone();

        public abstract Team[] Crossover(Team partner);

        public abstract void Mutate(float mutationPower, float absoluteMutationFactor);

        public void SetPositions(IList<Position> positions)
        {
            for(int i = 0; i < positions.Count; i++)
            {
                Members[i].Position = new Position(positions[i]);
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
