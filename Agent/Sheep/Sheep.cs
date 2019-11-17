using Auxiliary;
using System.Collections.Generic;
using System.Linq;
using MathNet.Spatial.Euclidean;

namespace Agent
{
    abstract class Sheep : IMovingAgent
    {
        public Vector2D Position { get; set; }
        public IList<Vector2D> Path { get; set; }

        protected Vector2D decision;

        private const double SIGHT_RANGE = 50.0;

        public double[] DecideOutput { get; private set; }

        public Sheep(double x, double y)
        {
            Path = new List<Vector2D>();

            Position = new Vector2D(x, y);
        }

        public virtual void Decide(double[] input)
        {
            decision = new Vector2D();

            for (int i = 0; i < input.Length; i += 2)
            {
                var d = new Vector2D(
                    input[i],
                    input[i + 1]);

                if (d.X > SIGHT_RANGE || d.X < -SIGHT_RANGE || d.Y > SIGHT_RANGE || d.Y < -SIGHT_RANGE)
                    continue;
                
                if (d.Length > SIGHT_RANGE)
                    continue;

                var speed = (SIGHT_RANGE - d.Length) * 4 / SIGHT_RANGE;

                d = d.Normalize();

                decision -= new Vector2D(d.X * speed, d.Y * speed);
            }

            decision = decision.Round();
        }

        public void Move()
        {
            if (decision.Length > 1.0)
                decision = decision.Normalize();

            Position += new Vector2D(decision.X, decision.Y);
        }

        public void StepBack()
        {
            if (!Path.Any()) return;

            Position = Path.Last();

            Path.RemoveAt(Path.Count - 1);
        }
    }
}