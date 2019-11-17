using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Spatial.Euclidean;

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
        
        public static List<Vector2D> PositionsInRelativeCoordinationSystem(this IEnumerable<Vector2D> positionsInOldCoordinationSystem, Vector2D newO, Vector2D anyPointOnPositiveSideOfNewOY)
        {
            var movedCenter = positionsInOldCoordinationSystem.Select(p => p - newO);

            anyPointOnPositiveSideOfNewOY -= newO;

            var d = anyPointOnPositiveSideOfNewOY.Length;

            var sin = anyPointOnPositiveSideOfNewOY.X / d;
            var cos = anyPointOnPositiveSideOfNewOY.Y / d;

            if (double.IsNaN(sin))
                sin = 0.0f;

            if (double.IsNaN(cos))
                cos = 1.0f;

            return movedCenter.Select(p => new Vector2D(p.X * cos - p.Y * sin, p.X * sin + p.Y * cos)).ToList();
        }
    }
}
