using Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using Teams;

namespace Simulations
{
    class Tournament
    {
        public Team Winner { get; private set; }
        public Team Looser { get; private set; }

        private List<Team> participants = new List<Team>();

        private readonly IList<IList<Position>> positionsOfShepardsSet;
        private readonly IList<IList<Position>> positionsOfSheepSet;

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

        public Tournament(SimulationParameters simulationParameters, Population population, IList<Position> positionsOfShepards, IList<Position> positionsOfSheep)
            : this(simulationParameters, population)
        {
            this.positionsOfShepardsSet = new List<IList<Position>> { positionsOfShepards };
            this.positionsOfSheepSet = new List<IList<Position>> { positionsOfSheep };
        }

        public Tournament(SimulationParameters simulationParameters, Population population, IList<IList<Position>> positionsOfShepardsSet, IList<IList<Position>> positionsOfSheepSet)
            : this(simulationParameters, population)
        {
            this.positionsOfShepardsSet = positionsOfShepardsSet;
            this.positionsOfSheepSet = positionsOfSheepSet;
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
            foreach (Team team in participants)
            {
                if (team != Looser)
                {
                    population.Units.Add(team);
                }
            }
        }
    }
}
