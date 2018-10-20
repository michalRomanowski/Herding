using System;
using System.Collections.Generic;
using System.Linq;
using Teams;
using Auxiliary;

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
                    new List<IList<Position>>() { simulationParameters.PositionsOfShepherds },
                    new List<IList<Position>>() { simulationParameters.PositionsOfSheep },
                    simulationParameters.SheepType,
                    simulationParameters.SeedForRandomSheepForBest);
            }

            var minFitness = teams.Min(x => x.Fitness);

            return teams.Where(x => x.Fitness == minFitness).First();
        }
    }
}
