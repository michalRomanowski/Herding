using System;
using MathNet.Spatial.Euclidean;

namespace Auxiliary
{
    public static class ArrayExtensions
    {
        public static void CutToMaxLength(this double[] input, double maxLength)
        {
            if (input.Length > 2)
                throw new ArgumentException();

            var inputVector = new Vector2D(input[0], input[1]);

            if (inputVector.Length > maxLength)
            {
                inputVector = inputVector.Normalize() * maxLength;
                input = new double[] { inputVector.X, inputVector.Y };
            }
        }
    }
}
