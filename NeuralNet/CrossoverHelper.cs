using Auxiliary;
using System;

namespace NeuralNets
{
    static class CrossoverHelper
    {
        public static T[] Crossover<T>(T[] parentA, T[] parentB) where T : struct
        {
            if (parentA.Length != parentB.Length)
                throw new ArgumentException();

            var child = new T[parentA.Length];

            for (int i = 0; i < child.Length; i++)
                child[i] = CRandom.Instance.NextDouble() < 0.5 ? parentA[i] : parentB[i];

            return child;
        }

        public static T[][] Crossover<T>(T[][] parentA, T[][] parentB) where T : struct
        {
            if (parentA.Length != parentB.Length || parentA[0].Length != parentB[0].Length)
                throw new ArgumentException();
            
            var child = new T[parentA.Length][]; 

            for (int i = 0; i < child.GetLength(0); i++)
            {
                child[i] = Crossover<T>(parentA[i], parentB[i]);
            }

            return child;
        }
    }
}
