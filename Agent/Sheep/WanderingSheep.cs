using System;
using MathNet.Spatial.Euclidean;

namespace Agent
{
    class WanderingSheep : Sheep
    {
        private Random r;

        private const double RANDOM_ROTATION_SPEED = 0.15f;
        private const double RANDOM_MOVEMENT_SPEED = 0.1f;

        private double RandomMoveAngle
        {
            get
            {
                return _randomMoveAngle;
            }
            set
            {
                _randomMoveAngle = value % (Math.PI * 2.0);
            }
        }

        private double _randomMoveAngle;

        public WanderingSheep(double x, double y, int seed) : base(x, y)
        {
            r = new Random(seed);
            RandomMoveAngle = r.NextDouble() * Math.PI * 2.0;
        }

        public override void Decide(double[] input)
        {
            base.Decide(input);

            RandomMoveAngle += (r.NextDouble() - 0.5f) * RANDOM_ROTATION_SPEED;

            var randomX = Math.Cos(RandomMoveAngle);
            var randomY = Math.Sin(RandomMoveAngle);

            decision += new Vector2D(randomX, randomY).Normalize() * RANDOM_MOVEMENT_SPEED;
        }
    }
}
