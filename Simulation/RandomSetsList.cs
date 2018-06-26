using Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simulation
{
    [Serializable]
    public class RandomSetsList
    {
        public List<List<Position>> PositionsOfShepardsSet { get; private set; }
        public List<List<Position>> PositionsOfSheepSet { get; private set; }
        public List<List<Position>> PositionsOfWolfsSet { get; private set; }
        
        public int Count { get { return PositionsOfShepardsSet.Count; } }

        public RandomSetsList()
        {
            PositionsOfShepardsSet = new  List<List<Position>>();
            PositionsOfSheepSet = new List<List<Position>>();
            PositionsOfWolfsSet = new List<List<Position>>();
        }

        public RandomSetsList(int numberOfRandomSets, int numberOfShepards, int numberOfSheep, int numberOfWolfs, int randomSeed)
        {
            Random r = new Random(randomSeed);

            PositionsOfShepardsSet = GenerateRandomPositions(numberOfRandomSets, numberOfShepards, r);
            PositionsOfSheepSet = GenerateRandomPositions(numberOfRandomSets, numberOfSheep, r);
            PositionsOfWolfsSet = GenerateRandomPositions(numberOfRandomSets, numberOfWolfs, r);
        }

        private List<List<Position>> GenerateRandomPositions(int numberOfPositionsSets, int numberOfPositions, Random r)
        {
            var randomPositions = new List<List<Position>>();

            for (int i = 0; i < numberOfPositionsSets; i++)
            {
                randomPositions.Add(new List<Position>());

                for (int j = 0; j < numberOfPositions; j++)
                {
                    randomPositions.Last().Add(new Position(r.Next(400), r.Next(400)));
                }
            }

            return randomPositions;
        }
    }
}

        
