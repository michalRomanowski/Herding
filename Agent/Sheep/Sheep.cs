using Auxiliary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Agent
{
    abstract class Sheep : ISheep
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

                CMath.ToVectorLengthOne(ref dX, ref dY);
                
                DecideOutput[0] -= dX * speed;
                DecideOutput[1] -= dY * speed;
            }

            if (DecideOutput[0] != 0 || DecideOutput[1] != 0)
                CMath.ToVectorLengthOne(ref DecideOutput[0], ref DecideOutput[1]);

            return DecideOutput;
        }

        public void StepBack()
        {
            if (Path.Count < 1) return;

            Position.X = Path.Last().X;
            Position.Y = Path.Last().Y;

            Path.RemoveAt(Path.Count - 1);
        }

        public void DrawPath(Graphics gfx, int offsetX, int offsetY)
        {
            for (int i = 1; i < Path.Count; i++)
            {
                gfx.DrawLine(new Pen(Color.DodgerBlue), new Point(offsetX + (int)Path[i - 1].X, offsetY + (int)Path[i - 1].Y), new Point(offsetX + (int)Path[i].X, offsetY + (int)Path[i].Y));
            }
        }

        public void DrawSight(Graphics gfx, int offsetX, int offsetY)
        {
            gfx.FillEllipse(new SolidBrush(Color.DarkBlue), new Rectangle(offsetX + (int)Position.X - (int)SIGHT_RANGE, offsetY + (int)Position.Y - (int)SIGHT_RANGE, (int)SIGHT_RANGE * 2, (int)SIGHT_RANGE * 2));
        }

        public void Draw(Graphics gfx, int offsetX, int offsetY)
        {
            gfx.FillEllipse(new SolidBrush(Color.Blue), new Rectangle((int)Position.X - 4 + offsetX, (int)Position.Y - 4 + offsetY, 8, 8));
        }
    }
}