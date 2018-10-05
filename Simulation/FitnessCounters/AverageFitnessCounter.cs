using Teams;

namespace Simulations
{
    public interface IAverageFitnessCounter
    {
        float CountAverageFitness(SimulationParameters simulationParameters, Population population, int seed = 0);
    }

    public class AverageFitnessCounter : IAverageFitnessCounter
    {
        public float CountAverageFitness(SimulationParameters simulationParameters, Population population, int seed = 0)
        {
            float sumOfFitnesses = 0.0f;
            float fitness = 0.0f;

            var fitnessCounter = FitnessCounterProvider.GetFitnessCounter(simulationParameters);

            foreach (Team team in population.Units)
            {
                if (simulationParameters.RandomPositions == false)
                {
                    fitness = fitnessCounter.CountFitness(
                        team,
                        simulationParameters,
                        simulationParameters.PositionsOfShepherds,
                        simulationParameters.PositionsOfSheep,
                        simulationParameters.SheepType,
                        seed);
                }
                else if (simulationParameters.RandomPositions == true)
                {
                    fitness = fitnessCounter.CountFitness(
                        team,
                        simulationParameters,
                        simulationParameters.RandomSetsForBest.PositionsOfShepherdsSet,
                        simulationParameters.RandomSetsForBest.PositionsOfSheepSet,
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
