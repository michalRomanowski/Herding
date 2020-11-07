using System;
using System.Globalization;
using System.Linq;

namespace Auxiliary
{
    public static class JaggedArraySerializer
    {
        private static readonly char[] separators = { ' ', ';', '|' };

        public static string Serialize(double[] input)
        {
            return String.Join(separators[0].ToString(CultureInfo.InvariantCulture), input.Select(x => x.ToString(CultureInfo.InvariantCulture)));
        }

        public static string Serialize(double[][] input)
        {
            return String.Join(separators[1].ToString(CultureInfo.InvariantCulture), input.Select(x => Serialize(x)));
        }

        public static string Serialize(double[][][] input)
        {
            return String.Join(separators[2].ToString(CultureInfo.InvariantCulture), input.Select(x => Serialize(x)));
        }

        public static double[] DeserializeLevel0(string input)
        {
            if (String.IsNullOrEmpty(input))
                return new double[0];

            return input.Split(separators[0]).Select(x => Convert.ToDouble(x, CultureInfo.InvariantCulture)).ToArray();
        }

        public static double[][] DeserializeLevel1(string input)
        {
            if (String.IsNullOrEmpty(input))
                return new double[0][];

            return input.Split(separators[1]).Select(x => DeserializeLevel0(x)).ToArray();
        }

        public static double[][][] DeserializeLevel2(string input)
        {
            if (String.IsNullOrEmpty(input))
                return new double[0][][];

            return input.Split(separators[2]).Select(x => DeserializeLevel1(x)).ToArray();
        }
    }
}