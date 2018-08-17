using Auxiliary;
using NeuralNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Agent
{
    [Serializable]
    public class Shepard : IThinkingAgent
    {
        private const int NEURAL_NET_OUTPUT_LAYER_SIZE = 2;

        public int NumberOfSeenShepards { get; private set; }
        public int NumberOfSeenSheep { get; private set; }

        public INeuralNet Brain { get; private set; }

        public Position Position { get; set; }

        public IList<Position> Path { get; set; }

        public List<KeyValuePair<IMovingAgent, float>> nearShepards;
        public List<KeyValuePair<ISheep, float>> nearSheep;

        public float[] DecideOutput { get; private set; }
        
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

        public Shepard(
            StringReader compressed) : this()
        {
            this.NumberOfSeenShepards = Convert.ToInt32(compressed.ReadLine());
            this.NumberOfSeenSheep = Convert.ToInt32(compressed.ReadLine());

            this.Brain = NeuralNetsProvider.DecompressMultiLayerNeuralNet(compressed);
        }

        public IThinkingAgent Clone()
        {
            var clone = new Shepard()
            {
                Brain = this.Brain.Clone(),
                NumberOfSeenShepards = this.NumberOfSeenShepards,
                NumberOfSeenSheep = this.NumberOfSeenSheep,
                Position = new Position(this.Position)
            };

            return clone;
        }

        public IThinkingAgent[] Crossover(IThinkingAgent partner)
        {
            Shepard[] children = new Shepard[2];

            children[0] = new Shepard(NumberOfSeenShepards, NumberOfSeenSheep);
            children[1] = new Shepard(NumberOfSeenShepards, NumberOfSeenSheep);


            var castedPartnerBrain = (partner as Shepard).Brain;

            children[0].Brain = Brain.Crossover(castedPartnerBrain);
            children[1].Brain = Brain.Crossover(castedPartnerBrain);

            return children;
        }

        public void Mutate(float mutationChance, float absoluteMutationFactor)
        {
            Brain.Mutate(mutationChance, absoluteMutationFactor);
        }

        public float[] Decide(float[] input)
        {
            DecideOutput = Brain.Think(input);

            return DecideOutput;
        }

        public void AdjustInputLayerSize(int numberOfSeenShepards, int numberOfSeenSheep)
        {
            NumberOfSeenSheep = numberOfSeenSheep;
            NumberOfSeenShepards = numberOfSeenShepards;

            Brain.AdjustInputLayerSize(2 + 2 * numberOfSeenShepards + 2 * numberOfSeenSheep);
        }

        public void AdjustHiddenLayersSize(int newSize)
        {
            Brain.AdjustHiddenLayersSize(newSize);
        }

        public void StepBack()
        {
            if (Path.Count < 1) return;

            Position = new Position(Path.Last());

            Path.RemoveAt(Path.Count - 1);
        }
        
        public string Compress()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{NumberOfSeenShepards.ToString()}\n");
            sb.Append($"{NumberOfSeenSheep.ToString()}\n");

            sb.Append(Brain.Compress());

            return sb.ToString();
        }

        public void DrawPath(Graphics gfx, int offsetX, int offsetY)
        {
            for (int i = 1; i < Path.Count; i++)
            {
                gfx.DrawLine(new Pen(Color.OrangeRed), new Point(offsetX + (int)Path[i - 1].X, offsetY + (int)Path[i - 1].Y), new Point(offsetX + (int)Path[i].X, offsetY + (int)Path[i].Y));
            }
        }

        public void DrawSight(Graphics gfx, int offsetX, int offsetY, IEnumerable<IMovingAgent> closeAgents, Color color)
        {
            foreach (IMovingAgent ca in closeAgents)
            {
                gfx.DrawLine(new Pen(color), new Point(offsetX + (int)Position.X, offsetY + (int)Position.Y), new Point(offsetX + (int)ca.Position.X, offsetY + (int)ca.Position.Y));
            }
        }

        public void Draw(Graphics gfx, int offsetX, int offsetY)
        {
            gfx.FillEllipse(new SolidBrush(Color.Red), new Rectangle((int)Position.X - 4 + offsetX, (int)Position.Y - 4 + offsetY, 8, 8));
        }
    }
}
