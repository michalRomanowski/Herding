using System;

namespace ActivationFunctions
{
    public class Tanh : ActivationFunction
    {
        public override double Impuls(double net)
        {
            return Math.Tanh(net);
        }
    }
}
