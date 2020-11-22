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

        private IPerception perception;

        public Shepherd()
        {
            Position = new Vector2D();
            Path = new List<Vector2D>();
        }

        public Shepherd(
            int numberOfSeenShepherds, 
            int numberOfSeenSheep,
            int numberOfNeuronsInHiddenLayer,
            int numberOfHiddenLayers,
            EPerceptionType perceptionType,
            bool randomizeNeuralNet)
            : this()
        {
            NumberOfSeenShepherds = numberOfSeenShepherds;
            NumberOfSeenSheep = numberOfSeenSheep;
            perception = PerceptionFactory.GetPerception(perceptionType);

            Brain = NeuralNetsFactory.GetMultiLayerNeuralNet(new NeuralNetParameters()
            {
                InputLayerSize = (numberOfSeenShepherds + numberOfSeenSheep) * 2 + 2,
                OutputLayerSize = NEURAL_NET_OUTPUT_LAYER_SIZE,
                HiddenLayerSize = numberOfNeuronsInHiddenLayer,
                NumberOfHiddenLayers = numberOfHiddenLayers
            });

            if(randomizeNeuralNet) Brain.Randomize();
        }

        public Shepherd(
            int numberOfSeenShepherds,
            int numberOfSeenSheep,
            EPerceptionType perceptionType,
            NeuralNet brain)
            : this()
        {
            NumberOfSeenShepherds = numberOfSeenShepherds;
            NumberOfSeenSheep = numberOfSeenSheep;
            perception = PerceptionFactory.GetPerception(perceptionType);

            Brain = brain;
        }

        public override ThinkingAgent GetClone()
        {
            return new Shepherd()
            {
                Brain = Brain.GetClone(),
                NumberOfSeenShepherds = NumberOfSeenShepherds,
                NumberOfSeenSheep = NumberOfSeenSheep,
                perception = PerceptionFactory.GetPerception(perception.PerceptionType),
                Position = new Vector2D(Position.X, Position.Y)
            };
        }

        public override ThinkingAgent Crossover(ThinkingAgent partner)
        {
            return new Shepherd(
                NumberOfSeenShepherds,
                NumberOfSeenSheep,
                perception.PerceptionType,
                Brain.Crossover((partner as Shepherd).Brain));
        }

        public override void Mutate(double mutationChance)
        {
            Brain.Mutate(mutationChance);
        }

        public override void Decide(double[] input)
        {
            var thinkingResult = Brain.Think(
                input.Select(x => perception.TransformPerception(x)).ToArray());

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