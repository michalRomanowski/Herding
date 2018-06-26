using System;

namespace Simulation
{
    public static class FitnessCounterProvider
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
            else throw new ApplicationException("No such EFitnessType recognized.");
        }
    }
}
