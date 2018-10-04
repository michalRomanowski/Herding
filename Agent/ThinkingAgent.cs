using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;
using NeuralNets;
using Auxiliary;

namespace Agent
{
    public abstract class ThinkingAgent : IMovingAgent, ICloneable<ThinkingAgent>
    {
        public int Id { get; set; }

        public int NumberOfSeenShepards { get; set; }
        public int NumberOfSeenSheep { get; set; }

        public string CompressedNeuralNet
        {
            get { return Brain.ToString(); }
            set
            {
                Brain = new NeuralNet();
                Brain.FromString(value);
            }
        }
        
        protected NeuralNet Brain { get; set; }

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


        public abstract void AdjustInputLayerSize(int numberOfSeenShepards, int numberOfSeenSheep);
        public abstract void AdjustHiddenLayersSize(int newSize);

        public abstract void StepBack();

        public abstract void Draw(Graphics gfx, int offsetX, int offsetY);
        public abstract void DrawPath(Graphics gfx, int offsetX, int offsetY);
        public abstract void DrawSight(Graphics gfx, int offsetX, int offsetY, IEnumerable<IMovingAgent> closeAgents, Color color);
    }
}
