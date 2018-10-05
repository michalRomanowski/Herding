using Auxiliary;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            DecideOutput = new float[] { 0.0f, 0.0f };

            for (int i = 0; i < input.Length; i += 2)
            {
                float dX = input[i];
                float dY = input[i + 1];

                if (dX > SIGHT_RANGE || dX < -SIGHT_RANGE || dY > SIGHT_RANGE || dY < -SIGHT_RANGE)
                    continue;

                float distance = (float)Math.Sqrt(dX * dX + dY * dY);

                if (distance > SIGHT_RANGE)
                    continue;

                float speed = SIGHT_RANGE - distance;

                var normalized = CMath.NormalizeToOne(dX, dY);

                DecideOutput[0] -= normalized[0] * speed;
                DecideOutput[1] -= normalized[1] * speed;
            }

            if (DecideOutput[0] != 0 || DecideOutput[1] != 0)
                DecideOutput = CMath.NormalizeToOne(DecideOutput);

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