using Auxiliary;
using System.Collections.Generic;

namespace Agent
{
    public interface IHasPosition
    {
        Position Position { get; set; }
    }

    public interface IMobileAgent : IHasPosition
    {
        IList<Position> Path { get; set; }
        void StepBack();
    }

    public interface IMovingAgent : IMobileAgent
    {
        float[] Decide(float[] input);
        float[] DecideOutput { get; }
    }
}
