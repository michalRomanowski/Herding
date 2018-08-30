using Auxiliary;
using System.Collections.Generic;
using System.Drawing;

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
}
