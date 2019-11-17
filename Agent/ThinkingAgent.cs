using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Auxiliary;
using NeuralNets;
using MathNet.Spatial.Euclidean;

namespace Agent
{
    [Serializable]
    public abstract class ThinkingAgent : IMovingAgent, ICloneable<ThinkingAgent>
    {
        [XmlAttribute]
        public int NumberOfSeenShepherds { get; set; }
        [XmlAttribute]
        public int NumberOfSeenSheep { get; set; }
        
        [XmlElement]
        public NeuralNet Brain { get; set; }

        [XmlIgnore]
        public IList<Vector2D> Path { get; set; }

        [XmlIgnore]
        public Vector2D Position { get; set; }

        [XmlIgnore]
        public Vector2D decision;

        public abstract ThinkingAgent GetClone();
        public abstract ThinkingAgent Crossover(ThinkingAgent partner);
        public abstract void Mutate(double mutationChance, double absoluteMutationFactor);
        public abstract void Decide(double[] input);
        public abstract void Move();

        public abstract void ResizeNeuralNet(int numberOfSeenShepherds, int numberOfSeenSheep, int numberOfHiddenLayers, int hiddenLayerSize);

        public abstract void StepBack();

        protected static int InputLayerSize(int numberOfSeenShepherds, int numberOfSeenSheep)
        {
            return (numberOfSeenShepherds + numberOfSeenSheep + 1) * 2;
        }
    }
}
