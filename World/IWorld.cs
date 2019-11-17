using Agent;
using System.Collections.Generic;
using System.Drawing;
using Teams;
using MathNet.Spatial.Euclidean;

namespace World
{
    public interface IWorld
    {
        Team Shepherds { get; }
        IList<IMovingAgent> Sheep { get; }

        IList<IList<Vector2D>> SheepPositionsRecord { get; }

        void Work(int numberOfSteps);
    }

    public interface IViewableWorld : IWorld
    {
        void Start(int numberOfSteps);
        void Stop();
        void Pause();
        void Resume();
        void StepForward();
        void StepBack();
        void SavePositions(string path);

        void Work(object numberOfSteps);

        void Draw(Graphics gfx, DrawingFlags flags);

        int Step { get; }
    }
}
