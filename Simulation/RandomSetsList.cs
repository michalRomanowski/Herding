using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using MathNet.Spatial.Euclidean;

namespace Simulations
{
    [Serializable]
    public class RandomSetsList
    {
        [XmlArray]
        public List<List<Vector2D>> PositionsOfShepherdsSet { get; private set; }
        [XmlArray]
        public List<List<Vector2D>> PositionsOfSheepSet { get; private set; }

        [XmlIgnore]
        public int Count { get { return PositionsOfShepherdsSet.Count; } }

        public RandomSetsList()
        {
            PositionsOfShepherdsSet = new  List<List<Vector2D>>();
            PositionsOfSheepSet = new List<List<Vector2D>>();
        }

        public RandomSetsList(int numberOfRandomSets, int numberOfShepherds, int numberOfSheep, int randomSeed)
        {
            Random r = new Random(randomSeed);

            PositionsOfShepherdsSet = GenerateRandomPositions(numberOfRandomSets, numberOfShepherds, r);
            PositionsOfSheepSet = GenerateRandomPositions(numberOfRandomSets, numberOfSheep, r);
        }

        private List<List<Vector2D>> GenerateRandomPositions(int numberOfPositionsSets, int numberOfPositions, Random r)
        {
            var randomPositions = new List<List<Vector2D>>();

            for (int i = 0; i < numberOfPositionsSets; i++)
            {
                randomPositions.Add(new List<Vector2D>());

                for (int j = 0; j < numberOfPositions; j++)
                {
                    randomPositions.Last().Add(new Vector2D(r.Next(400), r.Next(400)));
                }
            }

            return randomPositions;
        }
    }
}

        
