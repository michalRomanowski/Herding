using System;

namespace ActivationFunctions
{
    class Tanh : IActivationFunction
    {
        public double Impuls(double net)
        {
            return Math.Tanh(net);
        }
    }
}
