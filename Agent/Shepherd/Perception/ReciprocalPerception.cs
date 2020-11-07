using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Shepherd.Perception
{
    class ReciprocalPerception : IPerception
    {
        const double FRACTION = 4000.0;

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
