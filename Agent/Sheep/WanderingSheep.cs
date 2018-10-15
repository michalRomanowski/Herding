using System;
using System.Numerics;

namespace Agent
{
    class WanderingSheep : Sheep
    {
        private Random r;

        private const float RANDOM_ROTATION_SPEED = 0.15f;
        private const float RANDOM_MOVEMENT_SPEED = 0.25f;

        private float RandomMoveAngle
        {
            get
            {
                return _randomMoveAngle;
            }
            set
            {
                _randomMoveAngle = value % ((float)Math.PI * 2.0f);
            }
        }

        private float _randomMoveAngle;

        public WanderingSheep(float x, float y, int seed) : base(x, y)
        {
            r = new Random(seed);
            RandomMoveAngle = (float)r.NextDouble() * (float)Math.PI * 2.0f;
        }

        public override float[] Decide(float[] input)
        {
            base.Decide(input);

            RandomMoveAngle += ((float)r.NextDouble() - 0.5f) * RANDOM_ROTATION_SPEED;

            float randomX = (float)Math.Cos(RandomMoveAngle);
            float randomY = (float)Math.Sin(RandomMoveAngle);

            var vectorLengthOne = Vector2.Normalize(new Vector2(randomX, randomY));
            
            DecideOutput[0] += vectorLengthOne.X * RANDOM_MOVEMENT_SPEED;
            DecideOutput[1] += vectorLengthOne.Y * RANDOM_MOVEMENT_SPEED;

            return DecideOutput;
        }
    }
}
