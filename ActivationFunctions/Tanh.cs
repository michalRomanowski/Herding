using Auxiliary;

namespace ActivationFunctions
{
    class Tanh : IActivationFunction
    {
        public float Impuls(float net)
        {
            return CMath.Tanh(net);
        }
    }
}
