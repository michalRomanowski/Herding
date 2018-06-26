using Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using Team;

namespace Simulation
{
    class Tournament
    {
        public ITeam Winner { get; private set; }
        public ITeam Looser { get; private set; }

        private List<ITeam> participants = new List<ITeam>();

        private readonly List<List<Position>> positionsOfShepardsSet;
        private readonly List<List<Position>> positionsOfSheepSet;
        private readonly List<List<Position>> positionsOfWolfsSet;
        
        private IFitnessCounter fitnessCounter;

        private readonly SimulationParameters simulationParameters;
        private readonly Population population;
        
        public Tournament(SimulationParameters simulationParameters, Population population)
        {
            this.population = population;
            this.simulationParameters = simulationParameters;

            fitnessCounter = FitnessCounterProvider.GetFitnessCounter(simulationParameters);

            Random r = new Random();
            int randomIndex;

            for (int i = 0; i < simulationParameters.NumberOfParticipants; i++)
            {
                randomIndex = r.Next(simulationParameters.NumberOfShepards);
                participants.Add(population.Units[randomIndex]);
                population.Units.RemoveAt(randomIndex);
            }
        }

        public Tournament(SimulationParameters simulationParameters, Population population, List<Position> positionsOfShepards, List<Position> positionsOfSheep, List<Position> positionsOfWolfs)
            : this(simulationParameters, population)
        {
            this.positionsOfShepardsSet = new List<List<Position>> { positionsOfShepards };
            this.positionsOfSheepSet = new List<List<Position>> { positionsOfSheep };
            this.positionsOfWolfsSet = new List<List<Position>> { positionsOfWolfs };
        }

        public Tournament(SimulationParameters simulationParameters, Population population, List<List<Position>> positionsOfShepardsSet, List<List<Position>> positionsOfSheepSet, List<List<Position>> positionsOfWolfsSet)
            : this(simulationParameters, population)
        {
            this.positionsOfShepardsSet = positionsOfShepardsSet;
            this.positionsOfSheepSet = positionsOfSheepSet;
            this.positionsOfWolfsSet = positionsOfWolfsSet;
        }

        public void Attend()
        {
            var results = new float[participants.Count];

            var seed = CRandom.r.Next();

            for (int i = 0; i < participants.Count; i++)
                results[i] += fitnessCounter.CountFitness(
                    participants[i],
                    simulationParameters,
                    positionsOfShepardsSet, 
                    positionsOfSheepSet, 
                    positionsOfWolfsSet,
                    simulationParameters.SheepType, 
                    seed);
            
            SetWinner(results);
            SetLooser(results);
        }

        private void SetWinner(float[] results)
        {
            int indexOfWinner = 0;

            for (int i = 0; i < participants.Count; i++)
            {
                if (results[i] < results[indexOfWinner])
                    indexOfWinner = i;
            }

            Winner = participants.ElementAt(indexOfWinner);
        }

        private void SetLooser(float[] results)
        {
            int indexOfLooser = 0;

            for (int i = 0; i < participants.Count; i++)
            {
                if (results[i] > results[indexOfLooser])
                    indexOfLooser = i;
            }

            Looser = participants.ElementAt(indexOfLooser);
        }

        public void ReturnParticipants()
        {
            foreach (ITeam team in participants)
            {
                if (team != Looser)
                {
                    population.Units.Add(team);
                }
            }
        }
    }
}
