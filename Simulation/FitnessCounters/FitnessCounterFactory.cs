using System;

namespace Simulations
{
    public static class FitnessCounterFactory
    {
        public static IFitnessCounter GetFitnessCounter(CountFitnessParameters parameters)
        {
            if (parameters.FitnessType == EFitnessType.Final)
            {
                return new FinalFitnessCounter(parameters);
            }
            else if (parameters.FitnessType == EFitnessType.Sum)
            {
                return new SumFitnessCounter(parameters);
            }
            else throw new ArgumentException();
        }
    }
}
