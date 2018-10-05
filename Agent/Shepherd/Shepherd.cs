using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Auxiliary;
using NeuralNets;

namespace Agent
{
    [Serializable]
    public class Shepherd : ThinkingAgent
    {
        private const int NEURAL_NET_OUTPUT_LAYER_SIZE = 2;
        
        public Shepherd()
        {
            Position = new Position();
            Path = new List<Position>();
        }

        public Shepherd(int numberOfSeenShepherds, int numberOfSeenSheep)
            : this()
        {
            NumberOfSeenShepherds = numberOfSeenShepherds;
            NumberOfSeenSheep = numberOfSeenSheep;
        }

        public Shepherd(
            int numberOfSeenShepherds,
            int numberOfSeenSheep,
            int numberOfHiddenLayers,
            int sizeOfHiddenLayer)
            : this(
                numberOfSeenShepherds,
                numberOfSeenSheep)
        {
            Brain = NeuralNetsFactory.GetRandomMultiLayerNeuralNet((numberOfSeenShepherds + numberOfSeenSheep) * 2 + 2, NEURAL_NET_OUTPUT_LAYER_SIZE, sizeOfHiddenLayer, numberOfHiddenLayers);
        }

        public override ThinkingAgent GetClone()
        {
            return new Shepherd()
            {
                Brain = Brain.GetClone(),
                NumberOfSeenShepherds = NumberOfSeenShepherds,
                NumberOfSeenSheep = NumberOfSeenSheep,
                Position = new Position(Position)
            };
        }

        public override ThinkingAgent[] Crossover(ThinkingAgent partner)
        {
            Shepherd[] children = new Shepherd[2];

            children[0] = new Shepherd(NumberOfSeenShepherds, NumberOfSeenSheep);
            children[1] = new Shepherd(NumberOfSeenShepherds, NumberOfSeenSheep);
            
            var castedPartnerBrain = (partner as Shepherd).Brain;

            children[0].Brain = Brain.Crossover(castedPartnerBrain);
            children[1].Brain = Brain.Crossover(castedPartnerBrain);

            return children;
        }

        public override void Mutate(float mutationChance, float absoluteMutationFactor)
        {
            Brain.Mutate(mutationChance, absoluteMutationFactor);
        }

        public override float[] Decide(float[] input)
        {
            DecideOutput = Brain.Think(input);

            return DecideOutput;
        }

        public override void AdjustInputLayerSize(int numberOfSeenShepherds, int numberOfSeenSheep)
        {
            NumberOfSeenSheep = numberOfSeenSheep;
            NumberOfSeenShepherds = numberOfSeenShepherds;

            Brain.AdjustInputLayerSize(2 + 2 * numberOfSeenShepherds + 2 * numberOfSeenSheep);
        }

        public override void AdjustHiddenLayersSize(int newSize)
        {
            Brain.AdjustHiddenLayersSize(newSize);
        }

        public override void StepBack()
        {
            if (Path.Count < 1) return;

            Position = new Position(Path.Last());

            Path.RemoveAt(Path.Count - 1);
        }
    }
}
