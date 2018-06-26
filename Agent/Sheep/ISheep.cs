using System.Drawing;

namespace Agent
{
    public interface ISheep : IMovingAgent
    {
        void DrawSight(Graphics gfx, int offsetX, int offsetY);
    }
}
