using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Auxiliary
{
    public static class VectorExtensions
    {
        public static double Distance(this Vector2D a, Vector2D b)
        {
            return (a - b).Length;
        }

        public static double SquaredDistance(this Vector2D a, Vector2D b)
        {
            var length = Distance(a, b);
            return length * length;
        }

        public static Vector2D CutToMaxLength(this Vector2D vector, double maxLength)
        {
            return vector.Length > maxLength ? vector.Normalize() * maxLength : vector;
        }

        public static Vector2D Round(this Vector2D vector)
        {
            return new Vector2D(
                Math.Round(vector.X, 7, MidpointRounding.ToEven),
                Math.Round(vector.Y, 7, MidpointRounding.ToEven));
        }

        public static Vector2D Center(this IEnumerable<Vector2D> positions)
        {
            if (!positions.Any())
                throw new ArgumentException();

            return new Vector2D(
                positions.Sum(x => x.X) / positions.Count(),
                positions.Sum(x => x.Y) / positions.Count());
        }

        public static double SumOfDistancesFromCenter(this IEnumerable<Vector2D> positions)
        {
            var center = positions.Center();
            return positions.Sum(x => center.Distance(x));
        }

        public static Vector2D PositionInRelativeCoordinationSystem(this Vector2D position, Vector2D newO, Vector2D anyPointOnPositiveSideOfNewOY)
        {
            var movedCenter = position - newO;
            return movedCenter.PositionInRotatedCoordinationSystem(anyPointOnPositiveSideOfNewOY - newO);
        }

        public static Vector2D PositionInRotatedCoordinationSystem(this Vector2D position, Vector2D anyPointOnPositiveSideOfNewOY)
        {
            var d = anyPointOnPositiveSideOfNewOY.Length;

            var sin = anyPointOnPositiveSideOfNewOY.X / d;
            var cos = anyPointOnPositiveSideOfNewOY.Y / d;

            if (double.IsNaN(sin))
                sin = 0.0;

            if (double.IsNaN(cos))
                cos = 1.0;

            return new Vector2D(
                position.X * cos - position.Y * sin,
                position.X * sin + position.Y * cos);
        }

        public static IEnumerable<Vector2D> Randomize(this IEnumerable<Vector2D> valuesToRandomize, double min, double max)
        {
            return valuesToRandomize
                .Select(x => new Vector2D(
                    StaticRandom.R.NextDouble(min, max),
                    StaticRandom.R.NextDouble(min, max)));
        }
    }
}
