using System.Collections.Generic;
using System.Linq;

namespace NeuralNets
{
    static class Resizing
    {
        public static T[] Resize<T>(this T[] toResize, int newSize, T defaultValue)
        {
            var resized = new T[newSize];

            int i = 0;

            for(; i < newSize && i < toResize.Length; i++)
                resized[i] = toResize[i];

            for (; i < newSize; i++)
                resized[i] = defaultValue;

            return resized;
        }

        public static T[][] Resize<T>(this T[][] toResize, int newSizeX, int newSizeY, T defaultValue)
        {
            var resized = new T[newSizeX][];

            int x = 0;

            for (; x < toResize.GetLength(0) && x < newSizeX; x++)
            {
                resized[x] = toResize[x].Resize(newSizeY, defaultValue);
            }

            for (; x < newSizeX; x++)
            {
                for (int i = 0; i < newSizeY; i++)
                    resized[x][i] = defaultValue;
            }

            return resized;
        }
        
        public static T[][][] Resize<T>(this T[][][] toResize, int newSizeX, int newSizeY, int newSizeZ, T defaultValue)
        {
            var resized = new T[newSizeX][][];

            int x = 0;

            for(; x < toResize.GetLength(0) && x < newSizeX; x++)
                resized[x] = toResize[x].Resize(newSizeY, newSizeZ, defaultValue);

            for(; x < newSizeX; x++)
            {
                resized[x] = new T[newSizeY][];

                for(int y = 0; y < newSizeY; y++)
                {
                    for(int z = 0; z < newSizeZ; z++)
                        resized[x][y][z] = defaultValue;
                }
            }

            return resized;
        }
    }
}
