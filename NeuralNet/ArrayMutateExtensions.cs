using Auxiliary;

namespace NeuralNets
{
    public static class ArrayMutateExtensions
    {
        public static void Mutate(this double[][] valuesToMutate, double chanceOfMutation, double maxAddeValue = 1.0f)
        {
            for (int i = 0; i < valuesToMutate.GetLength(0); i++)
            {
                for (int j = 0; j < valuesToMutate[0].Length; j++)
                {
                    if (StaticRandom.R.NextDouble() < chanceOfMutation)
                        valuesToMutate[i][j] += StaticRandom.R.NextDouble(-maxAddeValue, maxAddeValue);
                }
            }
        }

        public static void Mutate(this double[] valuesToMutate, double chanceOfMutation, double maxAddeValue = 1.0f)
        {
            for (int i = 0; i < valuesToMutate.GetLength(0); i++)
            {
                if (StaticRandom.R.NextDouble() < chanceOfMutation)
                {
                    valuesToMutate[i] += StaticRandom.R.NextDouble(-maxAddeValue, maxAddeValue);
                }
            }
        }
    }
}