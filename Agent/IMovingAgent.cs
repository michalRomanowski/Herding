using Auxiliary;
using System.Collections.Generic;

namespace Agent
{
    public interface IHavePosition
    {
        Position Position { get; set; }
    }

    public interface IHavePath : IHavePosition
    {
        IList<Position> Path { get; set; }
        void StepBack();
    }

    public interface IMovingAgent : IHavePath
    {
        float[] Decide(float[] input);
        float[] DecideOutput { get; }
    }
}
