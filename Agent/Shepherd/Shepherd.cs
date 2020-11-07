using System;
using System.Collections.Generic;
using System.Linq;
using Auxiliary;
using NeuralNets;
using MathNet.Spatial.Euclidean;

namespace Agent
{
    [Serializable]
    public class Shepherd : ThinkingAgent
    {
        private const int NEURAL_NET_OUTPUT_LAYER_SIZE = 2;
        private const double SPEED = 1.0;

        public Shepherd()
        {
            Position = new Vector2D();
            Path = new List<Vector2D>();
        }

        public Shepherd(int numberOfSeenShepherds, int numberOfSeenSheep)
            : this()
        {
            NumberOfSeenShepherds = numberOfSeenShepherds;
            NumberOfSeenSheep = numberOfSeenSheep;
        }

        public Shepherd(
            IShepherdParameters parameters)
            : this(
                parameters.NumberOfSeenShepherds,
                parameters.NumberOfSeenSheep)
        {
            Brain = NeuralNetsFactory.GetMultiLayerNeuralNet(new NeuralNetParameters()
            {
                InputLayerSize = (parameters.NumberOfSeenShepherds + parameters.NumberOfSeenSheep) * 2 + 2,
                OutputLayerSize = NEURAL_NET_OUTPUT_LAYER_SIZE,
                HiddenLayerSize = parameters.NumberOfNeuronsInHiddenLayer,
                NumberOfHiddenLayers = parameters.NumberOfHiddenLayers
            });

            Brain.Randomize();
        }

        public override ThinkingAgent GetClone()
        {
            return new Shepherd()
            {
                Brain = Brain.GetClone(),
                NumberOfSeenShepherds = NumberOfSeenShepherds,
                NumberOfSeenSheep = NumberOfSeenSheep,
                Position = new Vector2D(Position.X, Position.Y)
            };
        }

        public override ThinkingAgent Crossover(ThinkingAgent partner)
        {
            return new Shepherd(NumberOfSeenShepherds, NumberOfSeenSheep)
            {
                Brain = Brain.Crossover((partner as Shepherd).Brain)
            };
        }

        public override void Mutate(double mutationChance)
        {
            Brain.Mutate(mutationChance);
        }

        public override void Decide(double[] input)
        {
            var thinkingResult = Brain.Think(input);

            decision = new Vector2D(thinkingResult[0], thinkingResult[1]);
            decision = decision.Round();
        }

        public override void Move()
        {
            decision = decision.CutToMaxLength(SPEED);
            Position += decision;
        }

        public override void ResizeNeuralNet(int numberOfSeenShepherds, int numberOfSeenSheep, int numberOfHiddenLayers, int hiddenLayerSize)
        {
            NumberOfSeenShepherds = numberOfSeenShepherds;
            NumberOfSeenSheep = numberOfSeenSheep;

            Brain.Resize(
                InputLayerSize(numberOfSeenShepherds, numberOfSeenSheep), 
                numberOfHiddenLayers, 
                hiddenLayerSize);
        }

        public override void StepBack()
        {
            if (Path.Count < 1) return;

            Position = new Vector2D(Path.Last().X, Path.Last().Y);

            Path.RemoveAt(Path.Count - 1);
        }
    }
}