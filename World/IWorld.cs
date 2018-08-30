using Agent;
using Auxiliary;
using System.Collections.Generic;
using System.Drawing;
using Teams;

namespace World
{
    public interface IWorld
    {
        Team Shepards { get; }
        IList<ISheep> Sheep { get; }

        IList<IList<Position>> SheepPositionsRecord { get; }

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

        void Draw(
            Graphics gfx,
            int offsetX,
            int offsetY,
            bool showShepardsSight,
            bool showSheepRange,
            bool showCenterOfGravityOfSheep,
            bool showShepardsPath,
            bool showSheepPath);

        int Step { get; }
    }
}
