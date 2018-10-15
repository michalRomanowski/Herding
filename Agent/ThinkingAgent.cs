using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NeuralNets;
using Auxiliary;

namespace Agent
{
    public abstract class ThinkingAgent : IMovingAgent, ICloneable<ThinkingAgent>
    {
        public int Id { get; set; }

        public int NumberOfSeenShepherds { get; set; }
        public int NumberOfSeenSheep { get; set; }

        public string CompressedNeuralNet
        {
            get { return Brain.ToString(); }
            set
            {
                Brain = NeuralNetsFactory.GetMultiLayerNeuralNet(value);
            }
        }
        
        protected INeuralNet Brain { get; set; }

        [NotMapped]
        public float[] DecideOutput { get; protected set; }
        
        [NotMapped]
        public IList<Position> Path { get; set; }

        [NotMapped]
        public Position Position { get; set; }
        
        public abstract ThinkingAgent GetClone();
        public abstract ThinkingAgent[] Crossover(ThinkingAgent partner);
        public abstract void Mutate(float mutationChance, float absoluteMutationFactor);
        public abstract float[] Decide(float[] input);

        public abstract void ResizeNeuralNet(int numberOfSeenShepherds, int numberOfSeenSheep, int numberOfHiddenLayers, int hiddenLayerSize);

        public abstract void StepBack();

        protected static int InputLayerSize(int numberOfSeenShepherds, int numberOfSeenSheep)
        {
            return (numberOfSeenShepherds + numberOfSeenSheep + 1) * 2;
        }
    }
}
