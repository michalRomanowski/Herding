using System.Collections.Generic;
using System.Linq;
using Teams;

namespace Simulations
{
    class RandomPositionsBestTeamSelector : IBestTeamSelector
    {
        SimulationParameters simulationParameters;
        IFitnessCounter fitnessCounter;

        public RandomPositionsBestTeamSelector(SimulationParameters simulationParameters)
        {
            this.simulationParameters = simulationParameters;
            this.fitnessCounter = FitnessCounterFactory.GetFitnessCounter(simulationParameters);
        }

        public Team GetBestTeam(IEnumerable<Team> teams)
        {
            foreach (var t in teams)
            {
                t.Fitness = fitnessCounter.CountFitness(
                        t,
                        simulationParameters,
                        simulationParameters.RandomSetsForBest.PositionsOfShepherdsSet,
                        simulationParameters.RandomSetsForBest.PositionsOfSheepSet,
                        simulationParameters.SheepType,
                        simulationParameters.SeedForRandomSheepForBest);
            }

            return teams.OrderBy(x => x.Fitness).First();
        }
    }
}
