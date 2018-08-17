using Auxiliary;
using System.Collections.Generic;
using Team;

namespace Simulation
{
    public class BestTeamManager
    {
        public BestTeamManager(){}

        public void UpdateBestTeam(SimulationParameters simulationParameters, Population population, IList<ITeam> newTeams)
        {
            IFitnessCounter fitnessCounter = FitnessCounterProvider.GetFitnessCounter(simulationParameters);

            if (population.Best != null)
            {
                if (simulationParameters.RandomPositions == false)
                {
                    population.Best.Fitness = fitnessCounter.CountFitness(
                        population.Best,
                        simulationParameters,
                        simulationParameters.PositionsOfShepards,
                        simulationParameters.PositionsOfSheep,
                        simulationParameters.SheepType,
                        simulationParameters.SeedForRandomSheepForBest);
                }
                else if (simulationParameters.RandomPositions == true)
                {
                    population.Best.Fitness = fitnessCounter.CountFitness(
                        population.Best,
                        simulationParameters,
                        simulationParameters.RandomSetsForBest.PositionsOfShepardsSet,
                        simulationParameters.RandomSetsForBest.PositionsOfSheepSet,
                        simulationParameters.SheepType,
                        simulationParameters.SeedForRandomSheepForBest);
                }
            }

            lock (population.BestLocker)
            {
                foreach (ITeam team in newTeams)
                {
                    if (simulationParameters.RandomPositions == false)
                    {
                        team.Fitness = fitnessCounter.CountFitness(
                            team,
                            simulationParameters,
                            simulationParameters.PositionsOfShepards,
                            simulationParameters.PositionsOfSheep,
                            simulationParameters.SheepType,
                            simulationParameters.SeedForRandomSheepForBest);
                    }
                    else if (simulationParameters.RandomPositions == true)
                    {
                        team.Fitness = fitnessCounter.CountFitness(
                            team,
                            simulationParameters,
                            simulationParameters.RandomSetsForBest.PositionsOfShepardsSet,
                            simulationParameters.RandomSetsForBest.PositionsOfSheepSet,
                            simulationParameters.SheepType,
                            simulationParameters.SeedForRandomSheepForBest);
                    }

                    Logger.AddLine("New fitness: " + team.Fitness);

                    if (population.Best == null || team.Fitness < population.Best.Fitness)
                    {
                        population.Best = team;
                    }
                }
            }
        }
    }
}
