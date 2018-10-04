using System;

namespace Auxiliary
{
    public static class CRandom
    {
        public static readonly Random r = new Random(DateTime.Now.Millisecond);

        public static float NextFloat(float min = 0.0f, float max = 1.0f)
        {
            return (float)r.NextDouble() * (max - min) + min;
        }
    }

    public static class FloatArrayRandomizeExtensions
    {
        public static void Randmize(this float[] valuesToRandomize, float min = -1.0f, float max = 1.0f)
        {
            for (int i = 0; i < valuesToRandomize.Length; i++)
            {
                valuesToRandomize[i] = CRandom.NextFloat(min, max);
            }
        }

        public static void Randmize(this float[,] valuesToRandomize, float min = -1.0f, float max = 1.0f)
        {
            for (int i = 0; i < valuesToRandomize.GetLength(0); i++)
            {
                for (int j = 0; j < valuesToRandomize.GetLength(1); j++)
                {
                    valuesToRandomize[i, j] = CRandom.NextFloat(min, max);
                }
            }
        }
    }
}
