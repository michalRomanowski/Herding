using System;

namespace ActivationFunctions
{
    public static class ActivationFunctionProvider
    {
        public static IActivationFunction GetActivationFunction(EActivationFunctionType type)
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
