using System;

namespace NeuralNets
{
    static class ArrayClone
    {
        public static T[] GetClone<T>(this T[] origin)
        {
            var clone = new T[origin.Length];

            Array.Copy(origin, clone, origin.Length);

            return clone;
        }

        public static T[,] GetClone<T>(this T[,] origin)
        {
            var clone = new T[origin.GetLength(0), origin.GetLength(1)];

            Array.Copy(
                origin,
                clone,
                origin.GetLength(0) * origin.GetLength(1));

            return clone;
        }

        public static T[][,] GetClone<T>(this T[][,] origin)
        {
            var clone = new T[origin.GetLength(0)][,];

            for (int i = 0; i < origin.Length; i++)
            {
                clone[i] = origin[i].GetClone();
            }

            return clone;
        }

        public static double[][] GetClone(this double[][] origin)
        {
            var clone = new double[origin.Length][];

            for(int i = 0; i < origin.Length; i++)
            {
                clone[i] = origin[i].GetClone();
            }

            return clone;
        }
    }
}
