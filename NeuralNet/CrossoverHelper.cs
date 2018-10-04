using Auxiliary;
using System;

namespace NeuralNets
{
    static class CrossoverHelper
    {
        public static float[] Crossover(float[] parentA, float[] parentB)
        {
            if (parentA.Length != parentB.Length)
                throw new ArgumentException();

            var child = new float[parentA.Length];

            for (int i = 0; i < child.Length; i++)
                child[i] = CRandom.NextFloat() < 0.5f ? parentA[i] : parentB[i];

            return child;
        }

        public static float[,] Crossover(float[,] parentA, float[,] parentB)
        {
            if (parentA.GetLength(0) != parentB.GetLength(0) || parentA.GetLength(1) != parentB.GetLength(1))
                throw new ArgumentException();

            var child = new float[parentA.GetLength(0), parentA.GetLength(1)];

            for (int i = 0; i < child.GetLength(0); i++)
            {
                for (int j = 0; j < child.GetLength(1); j++)
                    child[i, j] = CRandom.NextFloat() < 0.5f ? child[i, j] = parentA[i, j] : child[i, j] = parentB[i, j];
            }

            return child;
        }
    }
}
