using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Auxiliary;
using NeuralNets;

namespace Agent
{
    [Serializable]
    public class Shepard : ThinkingAgent
    {
        private const int NEURAL_NET_OUTPUT_LAYER_SIZE = 2;
        
        public Shepard()
        {
            Position = new Position();
            Path = new List<Position>();
        }

        public Shepard(int numberOfSeenShepards, int numberOfSeenSheep)
            : this()
        {
            NumberOfSeenShepards = numberOfSeenShepards;
            NumberOfSeenSheep = numberOfSeenSheep;
        }

        public Shepard(
            float x,
            float y,
            int numberOfSeenShepards,
            int numberOfSeenSheep,
            int numberOfHiddenLayers,
            int sizeOfHiddenLayer)
            : this(
                numberOfSeenShepards,
                numberOfSeenSheep)
        {
            Position.X = x;
            Position.Y = y;

            Brain = NeuralNetsProvider.GetRandomMultiLayerNeuralNet((numberOfSeenShepards + numberOfSeenSheep) * 2 + 2, NEURAL_NET_OUTPUT_LAYER_SIZE, sizeOfHiddenLayer, numberOfHiddenLayers);
        }

        public override ThinkingAgent GetClone()
        {
            var clone = new Shepard()
            {
                Brain = Brain.GetClone(),
                NumberOfSeenShepards = NumberOfSeenShepards,
                NumberOfSeenSheep = NumberOfSeenSheep,
                Position = new Position(Position)
            };

            return clone;
        }

        public override ThinkingAgent[] Crossover(ThinkingAgent partner)
        {
            Shepard[] children = new Shepard[2];

            children[0] = new Shepard(NumberOfSeenShepards, NumberOfSeenSheep);
            children[1] = new Shepard(NumberOfSeenShepards, NumberOfSeenSheep);


            var castedPartnerBrain = (partner as Shepard).Brain;

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

        public override void AdjustInputLayerSize(int numberOfSeenShepards, int numberOfSeenSheep)
        {
            NumberOfSeenSheep = numberOfSeenSheep;
            NumberOfSeenShepards = numberOfSeenShepards;

            Brain.AdjustInputLayerSize(2 + 2 * numberOfSeenShepards + 2 * numberOfSeenSheep);
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
        
        public override void DrawPath(Graphics gfx, int offsetX, int offsetY)
        {
            for (int i = 1; i < Path.Count; i++)
            {
                gfx.DrawLine(new Pen(Color.OrangeRed), new Point(offsetX + (int)Path[i - 1].X, offsetY + (int)Path[i - 1].Y), new Point(offsetX + (int)Path[i].X, offsetY + (int)Path[i].Y));
            }
        }

        public override void DrawSight(Graphics gfx, int offsetX, int offsetY, IEnumerable<IMovingAgent> closeAgents, Color color)
        {
            foreach (IMovingAgent ca in closeAgents)
            {
                gfx.DrawLine(new Pen(color), new Point(offsetX + (int)Position.X, offsetY + (int)Position.Y), new Point(offsetX + (int)ca.Position.X, offsetY + (int)ca.Position.Y));
            }
        }

        public override void Draw(Graphics gfx, int offsetX, int offsetY)
        {
            gfx.FillEllipse(new SolidBrush(Color.Red), new Rectangle((int)Position.X - 4 + offsetX, (int)Position.Y - 4 + offsetY, 8, 8));
        }
    }
}
