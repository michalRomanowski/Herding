using System;

namespace Simulations
{
    public static class FitnessCounterFactory
    {
        public static IFitnessCounter GetFitnessCounter(SimulationParameters simulationParameters)
        {
            if (simulationParameters.FitnessType == EFitnessType.Final)
            {
                return new FinalFitnessCounter();
            }
            else if (simulationParameters.FitnessType == EFitnessType.Sum)
            {
                return new SumFitnessCounter();
            }
            else throw new ArgumentException();
        }
    }
}
