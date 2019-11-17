using System.Collections.Generic;
using MathNet.Spatial.Euclidean;

namespace Agent
{
    public interface IHavePosition
    {
        Vector2D Position { get; set; }
    }

    public interface IHavePath : IHavePosition
    {
        IList<Vector2D> Path { get; set; }
        void StepBack();
    }

    public interface IMovingAgent : IHavePath
    {
        void Decide(double[] input);
        void Move();
    }
}
