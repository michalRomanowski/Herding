using System;

namespace Simulations
{
    public static class FitnessCounterFactory
    {
        public static IFitnessCounter GetFitnessCounter(EFitnessType fitnessType, CountFitnessParameters parameters)
        {
            if (fitnessType == EFitnessType.Final)
            {
                return new FinalFitnessCounter(parameters);
            }
            else if (fitnessType == EFitnessType.Sum)
            {
                return new SumFitnessCounter(parameters);
            }
            else throw new ArgumentException();
        }
    }
}
