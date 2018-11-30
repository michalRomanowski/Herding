using System;
using System.IO;
using System.Globalization;

namespace Auxiliary
{
    public static class StreamHelper
    {
        public static void Compress(StringWriter sw, float[,] values)
        {
            sw.WriteLine(values.GetLength(0));
            sw.WriteLine(values.GetLength(1));

            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    sw.WriteLine(values[i, j]);
                }
            }
        }

        public static void Compress(StringWriter sw, float[] values)
        {
            sw.WriteLine(values.GetLength(0));

            for (int i = 0; i < values.GetLength(0); i++)
            {
                sw.WriteLine(values[i]);
            }
        }

        public static void Compress(StringWriter sw, float[][] values)
        {
            sw.WriteLine(values.GetLength(0));

            for (int i = 0; i < values.Length; i++)
            {
                Compress(sw, values[i]);
            }
        }

        public static float[,] Decompress2DimFloatArray(StringReader sr)
        {
            int length0 = Convert.ToInt32(sr.ReadLine());
            int length1 = Convert.ToInt32(sr.ReadLine());

            var wages = new float[length0, length1];

            for (int i = 0; i < wages.GetLength(0); i++)
            {
                for (int j = 0; j < wages.GetLength(1); j++)
                {
                    wages[i, j] = (float)Convert.ToDouble(sr.ReadLine().Replace(',', '.'), CultureInfo.InvariantCulture);
                }
            }

            return wages;
        }

        public static float[][] Decompress2DimFloatJaggedArray(StringReader sr)
        {
            int length = Convert.ToInt32(sr.ReadLine());

            var values = new float[length][];

            for (int i = 0; i < length; i++)
            {
                values[i] = StreamHelper.Decompress1DimFloatArray(sr);
            }

            return values;
        }

        public static float[] Decompress1DimFloatArray(StringReader sr)
        {
            int length = Convert.ToInt32(sr.ReadLine());

            var values = new float[length];

            for (int i = 0; i < length; i++)
            {
                values[i] = (float)Convert.ToDouble(sr.ReadLine().Replace(',', '.'), CultureInfo.InvariantCulture);
            }

            return values;
        }
    }
}
