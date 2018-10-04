using System;

namespace NeuralNets
{
    static class ArrayClone
    {
        public static float[] GetClone(this float[] origin)
        {
            var clone = new float[origin.Length];

            Array.Copy(origin, clone, origin.Length);

            return clone;
        }

        public static float[,] GetClone(this float[,] origin)
        {
            var clone = new float[origin.GetLength(0), origin.GetLength(1)];

            Array.Copy(
                origin,
                clone,
                origin.GetLength(0) * origin.GetLength(1));

            return clone;
        }

        public static float[][,] GetClone(this float[][,] origin)
        {
            var clone = new float[origin.GetLength(0)][,];

            for (int i = 0; i < origin.Length; i++)
            {
                clone[i] = origin[i].GetClone();
            }

            return clone;
        }

        public static float[][] GetClone(this float[][] origin)
        {
            var clone = new float[origin.Length][];

            for(int i = 0; i < origin.Length; i++)
            {
                clone[i] = origin[i].GetClone();
            }

            return clone;
        }
    }
}
