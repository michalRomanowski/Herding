using System.Collections.Generic;
using System.Linq;
using Teams;

namespace Simulations
{
    class FixedPositionsBestTeamSelector : IBestTeamSelector
    {
        SimulationParameters simulationParameters;
        IFitnessCounter fitnessCounter;

        public FixedPositionsBestTeamSelector(SimulationParameters simulationParameters)
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
                        simulationParameters.PositionsOfShepherds,
                        simulationParameters.PositionsOfSheep,
                        simulationParameters.SheepType,
                        simulationParameters.SeedForRandomSheepForBest);
            }

            return teams.OrderBy(x => x.Fitness).First();
        }
    }
}
