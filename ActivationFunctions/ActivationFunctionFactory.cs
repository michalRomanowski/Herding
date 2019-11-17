using System;

namespace ActivationFunctions
{
    public static class ActivationFunctionFactory
    {
        public static ActivationFunction GetActivationFunction(EActivationFunctionType type)
        {
            switch(type)
            {
                case EActivationFunctionType.Sum:
                    return new Sum();
                case EActivationFunctionType.Tanh:
                    return new Tanh();
                case EActivationFunctionType.Step:
                    return new Step();
                default:
                    throw new ArgumentException(type.ToString() + " is not supported.");
            }
        }
    }
}
