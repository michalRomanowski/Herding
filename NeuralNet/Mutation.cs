using Auxiliary;

namespace NeuralNets
{
    static class Mutation
    {
        public static void Mutate(this double[][] valuesToMutate, double chanceOfMutation, double maxAddeValue = 1.0f)
        {
            for (int i = 0; i < valuesToMutate.GetLength(0); i++)
            {
                for (int j = 0; j < valuesToMutate[0].Length; j++)
                {
                    if (CRandom.Instance.NextDouble() < chanceOfMutation)
                        valuesToMutate[i][j] += CRandom.Instance.NextDouble(-maxAddeValue, maxAddeValue);
                }
            }
        }

        public static void Mutate(this double[] valuesToMutate, double chanceOfMutation, double maxAddeValue = 1.0f)
        {
            for (int i = 0; i < valuesToMutate.GetLength(0); i++)
            {
                if (CRandom.Instance.NextDouble() < chanceOfMutation)
                {
                    valuesToMutate[i] += CRandom.Instance.NextDouble(-maxAddeValue, maxAddeValue);
                }
            }
        }
    }
}
