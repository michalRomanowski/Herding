using System;
using Auxiliary;

namespace Agent
{
    class WanderingSheep : Sheep
    {
        private Random r;

        private const float RANDOM_ROTATION_SPEED = 0.15f;
        private const float RANDOM_MOVEMENT_SPEED = 0.15f;

        private float randomMoveAngle
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
            randomMoveAngle = (float)r.NextDouble() * (float)Math.PI * 2.0f;
        }

        public override float[] Decide(float[] input)
        {
            base.Decide(input);

            randomMoveAngle += ((float)r.NextDouble() - 0.5f) * RANDOM_ROTATION_SPEED;

            float randomX = (float)Math.Cos(randomMoveAngle);
            float randomY = (float)Math.Sin(randomMoveAngle);

            CMath.ToVectorLengthOne(ref randomX, ref randomY);
            
            DecideOutput[0] += randomX * RANDOM_MOVEMENT_SPEED;
            DecideOutput[1] += randomY * RANDOM_MOVEMENT_SPEED;

            return DecideOutput;
        }
    }
}
