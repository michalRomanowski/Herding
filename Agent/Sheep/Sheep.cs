using Auxiliary;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace Agent
{
    abstract class Sheep : IMovingAgent
    {
        private const float SIGHT_RANGE = 50.0f;

        public Position Position { get; set; }

        public IList<Position> Path { get; set; }

        public float[] DecideOutput { get; private set; }

        public Sheep(float x, float y)
        {
            Path = new List<Position>();

            Position = new Position();

            Position.X = x;
            Position.Y = y;
        }

        public virtual float[] Decide(float[] input)
        {
            var decision = new Vector2();

            for (int i = 0; i < input.Length; i += 2)
            {
                var d = new Vector2(
                    input[i],
                    input[i + 1]);

                if (d.X > SIGHT_RANGE || d.X < -SIGHT_RANGE || d.Y > SIGHT_RANGE || d.Y < -SIGHT_RANGE)
                    continue;

                float distance = d.Length();

                if (distance > SIGHT_RANGE)
                    continue;

                float speed = SIGHT_RANGE - distance;

                var normalized = Vector2.Normalize(d);

                decision.X -= normalized.X * speed;
                decision.Y -= normalized.Y * speed;
            }

            if (decision.Length() > 1.0f)
                decision = Vector2.Normalize(decision);

            DecideOutput = new float[2] { decision.X, decision.Y };
            
            return DecideOutput;
        }

        public void StepBack()
        {
            if (Path.Count < 1) return;

            Position.X = Path.Last().X;
            Position.Y = Path.Last().Y;

            Path.RemoveAt(Path.Count - 1);
        }
    }
}