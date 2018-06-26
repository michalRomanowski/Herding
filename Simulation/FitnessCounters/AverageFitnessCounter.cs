using Team;

namespace Simulation
{
    interface IAverageFitnessCounter
    {
        float CountAverageFitness(SimulationParameters simulationParameters, Population population, int seed = 0);
    }

    class AverageFitnessCounter : IAverageFitnessCounter
    {
        private IFitnessCounter fitnessCounter;

        public AverageFitnessCounter(SimulationParameters simulationParameters)
        {
            fitnessCounter = FitnessCounterProvider.GetFitnessCounter(simulationParameters);
        }

        public float CountAverageFitness(SimulationParameters simulationParameters, Population population, int seed = 0)
        {
            float sumOfFitnesses = 0.0f;
            float fitness = 0.0f;

            foreach (ITeam team in population.Units)
            {
                if (simulationParameters.RandomPositions == false)
                {
                    fitness = fitnessCounter.CountFitness(
                        team,
                        simulationParameters,
                        simulationParameters.PositionsOfShepards,
                        simulationParameters.PositionsOfSheep,
                        simulationParameters.PositionsOfWolfs,
                        simulationParameters.SheepType,
                        seed);
                }
                else if (simulationParameters.RandomPositions == true)
                {
                    fitness = fitnessCounter.CountFitness(
                        team,
                        simulationParameters,
                        simulationParameters.RandomSetsForBest.PositionsOfShepardsSet,
                        simulationParameters.RandomSetsForBest.PositionsOfSheepSet,
                        simulationParameters.RandomSetsForBest.PositionsOfWolfsSet,
                        simulationParameters.SheepType,
                        seed);
                }

                team.Fitness = fitness;
                sumOfFitnesses += fitness;
            }

            return sumOfFitnesses / population.Units.Count;
        }
    }
}
