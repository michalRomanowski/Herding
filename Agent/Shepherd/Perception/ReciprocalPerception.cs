using System;

namespace Agent
{
    class ReciprocalPerception : IPerception
    {
        public EPerceptionType PerceptionType => EPerceptionType.Reciprocal;

        private const double FRACTION = 4000.0;

        public double TransformPerception(double input)
        {
            if (input == 0.0)
                return FRACTION;

            if (input > 0.0)
                return Math.Min(FRACTION / input, FRACTION);
            else return Math.Max(FRACTION / input, -FRACTION);
        }
    }
}
