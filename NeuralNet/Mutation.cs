using Auxiliary;

namespace NeuralNets
{
    static class Mutation
    {
        public static void Mutate(this float[,] valuesToMutate, float chanceOfMutation, float maxAddeValue = 1.0f)
        {
            for (int i = 0; i < valuesToMutate.GetLength(0); i++)
            {
                for (int j = 0; j < valuesToMutate.GetLength(1); j++)
                {
                    if (CRandom.Instance.NextFloat() < chanceOfMutation)
                        valuesToMutate[i, j] += CRandom.Instance.NextFloat(-maxAddeValue, maxAddeValue);
                }
            }
        }

        public static void Mutate(this float[] valuesToMutate, float chanceOfMutation, float maxAddeValue = 1.0f)
        {
            for (int i = 0; i < valuesToMutate.GetLength(0); i++)
            {
                if (CRandom.Instance.NextFloat() < chanceOfMutation)
                {
                    valuesToMutate[i] += CRandom.Instance.NextFloat(-maxAddeValue, maxAddeValue);
                }
            }
        }
    }
}
