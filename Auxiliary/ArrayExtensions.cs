namespace Auxiliary
{
    public static class ArrayExtensions
    {
        public static void Randomize(this double[] valuesToRandomize, double min, double max)
        {
            for (int i = 0; i < valuesToRandomize.Length; i++)
            {
                valuesToRandomize[i] = StaticRandom.R.NextDouble(min, max);
            }
        }

        public static void Randomize(this double[][] valuesToRandomize, double min, double max)
        {
            for (int i = 0; i < valuesToRandomize.Length; i++)
            {
                for (int j = 0; j < valuesToRandomize[0].Length; j++)
                {
                    valuesToRandomize[i][j] = StaticRandom.R.NextDouble(min, max);
                }
            }
        }
    }
}