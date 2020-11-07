using MathNet.Spatial.Euclidean;
using System.Collections.Generic;
using System.Linq;

namespace Auxiliary
{
    public class VectorFactory
    {
        public static Vector2D RandomVector(int min, int max)
        {
            return new Vector2D(
                StaticRandom.R.Next(min, max),
                StaticRandom.R.Next(min, max));
        }

        public static List<List<Vector2D>> GenerateRandomPositions(int numberOfPositionsSets, int numberOfPositionsInSet, int min, int max)
        {
            var randomPositions = new List<List<Vector2D>>();

            for (int i = 0; i < numberOfPositionsSets; i++)
            {
                randomPositions.Add(new List<Vector2D>());

                for (int j = 0; j < numberOfPositionsInSet; j++)
                {
                    randomPositions.Last().Add(RandomVector(min, max));
                }
            }

            return randomPositions;
        }
    }
}