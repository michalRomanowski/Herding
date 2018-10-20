using System;
using System.Collections.Generic;
using System.Linq;

namespace Auxiliary
{
    public class CRandom : Random
    {
        public static readonly CRandom Instance = new CRandom();

        public CRandom() : base()
        { }

        public float NextFloat(float min = 0.0f, float max = 1.0f)
        {
            return (float)NextDouble() * (max - min) + min;
        }
    }

    public class RandomUniqueSequence
    {
        HashSet<int> sequence;
        int max;

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

    public static class FloatArrayRandomizeExtensions
    {
        public static void Randmize(this float[] valuesToRandomize, float min = -1.0f, float max = 1.0f)
        {
            for (int i = 0; i < valuesToRandomize.Length; i++)
            {
                valuesToRandomize[i] = CRandom.Instance.NextFloat(min, max);
            }
        }

        public static void Randmize(this float[,] valuesToRandomize, float min = -1.0f, float max = 1.0f)
        {
            for (int i = 0; i < valuesToRandomize.GetLength(0); i++)
            {
                for (int j = 0; j < valuesToRandomize.GetLength(1); j++)
                {
                    valuesToRandomize[i, j] = CRandom.Instance.NextFloat(min, max);
                }
            }
        }
    }
}
