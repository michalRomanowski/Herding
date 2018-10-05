using Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simulations
{
    public class RandomSetsList
    {
        public int Id { get; set; }
        
        public string CompressedPositionsOfShepherdsSet
        {
            get { return Compressor.Compress(PositionsOfShepherdsSet); }
            set { PositionsOfShepherdsSet = Compressor.DecompressJaggedList<Position>(value); }
        }

        public string CompressedPositionsOfSheepSet
        {
            get { return Compressor.Compress(PositionsOfSheepSet); }
            set { PositionsOfSheepSet = Compressor.DecompressJaggedList<Position>(value); }
        }

        [NotMapped]
        public IList<IList<Position>> PositionsOfShepherdsSet { get; private set; }
        [NotMapped]
        public IList<IList<Position>> PositionsOfSheepSet { get; private set; }
        
        public int Count { get { return PositionsOfShepherdsSet.Count; } }

        public RandomSetsList()
        {
            PositionsOfShepherdsSet = new  List<IList<Position>>();
            PositionsOfSheepSet = new List<IList<Position>>();
        }

        public RandomSetsList(int numberOfRandomSets, int numberOfShepherds, int numberOfSheep, int randomSeed)
        {
            Random r = new Random(randomSeed);

            PositionsOfShepherdsSet = GenerateRandomPositions(numberOfRandomSets, numberOfShepherds, r);
            PositionsOfSheepSet = GenerateRandomPositions(numberOfRandomSets, numberOfSheep, r);
        }

        private IList<IList<Position>> GenerateRandomPositions(int numberOfPositionsSets, int numberOfPositions, Random r)
        {
            var randomPositions = new List<IList<Position>>();

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

        
