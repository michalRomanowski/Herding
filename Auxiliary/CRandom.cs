using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Spatial.Euclidean;

namespace Auxiliary
{
    public class CRandom : Random
    {
        public static readonly CRandom Instance = new CRandom();

        public CRandom() : base()
        { }

        public double NextDouble(double min, double max)
        {
            return NextDouble() * (max - min) + min;
        }
    }

    public class RandomUniqueSequence
    {
        private HashSet<int> sequence;
        private readonly int max;

        public RandomUniqueSequence(int max)
        {
            sequence = new HashSet<int>();
            this.max = max;
        }

        public IEnumerable<int> GetSubSequence(int size)
        {
            List<int> subSequence = new List<int>(size);

            for(int i = 0; i < size; i++)
            {
                var element = UniqueElement();
                sequence.Add(element);
                subSequence.Add(element);
            }

            return subSequence;
        }

        private int UniqueElement()
        {
            int element;

            do
            {
                element = CRandom.Instance.Next(max);

            } while (sequence.Contains(element));

            return element;
        }
    }

    public static class ArrayRandomizeExtensions
    {
        public static void Randmize(this double[] valuesToRandomize, double min = -1.0f, double max = 1.0f)
        {
            for (int i = 0; i < valuesToRandomize.Length; i++)
            {
                valuesToRandomize[i] = CRandom.Instance.NextDouble(min, max);
            }
        }

        public static void Randmize(this double[][] valuesToRandomize, double min = -1.0, double max = 1.0)
        {
            for (int i = 0; i < valuesToRandomize.Length; i++)
            {
                for (int j = 0; j < valuesToRandomize[0].Length; j++)
                {
                    valuesToRandomize[i][j] = CRandom.Instance.NextDouble(min, max);
                }
            }
        }
    }

    public static class IEnumerableRandomizeExtensions
    {
        public static IEnumerable<Vector2D> Randmize(this IEnumerable<Vector2D> valuesToRandomize, double min = 0, double max = 400)
        {
            return valuesToRandomize
                .Select(x => new Vector2D(
                    CRandom.Instance.NextDouble(min, max), 
                    CRandom.Instance.NextDouble(min, max)));
        }
    }
}
