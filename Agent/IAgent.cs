using Auxiliary;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using NeuralNet;

namespace Agent
{
    public interface IMovingAgent
    {
        Position Position { get; set; }

        float[] Decide(float[] input);
        float[] DecideOutput { get; }

        IList<Position> Path { get; set; }
        void StepBack();

        void Draw(Graphics gfx, int offsetX, int offsetY);
        void DrawPath(Graphics gfx, int offsetX, int offsetY);
    }

    public interface IThinkingAgent : IMovingAgent
    {
        int NumberOfSeenShepards { get; }
        int NumberOfSeenSheep { get; }
        int NumberOfSeenWolfs { get; }

        INeuralNet Brain { get; }
        
        IThinkingAgent Clone();
        IThinkingAgent[] Crossover(IThinkingAgent partner);
        void Mutate(float mutationChance, float absoluteMutationFactor);

        void AdjustInputLayerSize(int numberOfSeenShepards, int numberOfSeenSheep);
        void AdjustHiddenLayersSize(int newSize);
        
        void DrawSight(Graphics gfx, int offsetX, int offsetY, IEnumerable<IMovingAgent> closeAgents, Color color);

        string Compress();
    }
}
