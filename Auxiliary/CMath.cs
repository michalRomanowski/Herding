using System;
using System.Numerics;

namespace Auxiliary
{
    public static class CMath
    {
        public const float PI = (float)Math.PI;

        public static float Tanh(float x)
        {
            return (float)Math.Tanh(x);
        }

        public static float[] NormalizeToOne(float x, float y)
        {
            var normalized = new float[2];

            Vector2.Normalize(new Vector2(x, y)).CopyTo(normalized);

            return normalized;
        }

        public static float[] NormalizeToOne(float[] input)
        {
            if (input.Length != 2)
                throw new ArgumentException();

            return NormalizeToOne(input[0], input[1]);
        }
    }
}
