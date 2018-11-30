using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Globalization;

namespace Auxiliary
{
    public class Position : ICompress<Position>
    {
        private const char SEPARATOR = ';';

        public float X
        {
            get { return vector.X; }
            set { vector.X = value; }
        }

        public float Y
        {
            get { return vector.Y; }
            set { vector.Y = value; }
        }

        private Vector2 vector = new Vector2();

        public Position(){}

        public Position(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public Position(Position value) : this(value.X, value.Y){ }

        public Position(string value)
        {
            var splitted = value.Split(SEPARATOR);

            X = Convert.ToInt32(splitted[0]);
            Y = Convert.ToInt32(splitted[1]);
        }

        public static Position operator+(Position a, Position b)
        {
            return new Position() { vector = a.vector + b.vector };
        }

        public static Position operator-(Position a, Position b)
        {
            return new Position() { vector = a.vector - b.vector };
        }

        public static float SquaredDistance(Position a, Position b)
        {
            return Vector2.DistanceSquared(a.vector, b.vector);
        }

        public static float Distance(Position a, Position b)
        {
            return Vector2.Distance(a.vector, b.vector);
        }

        public float Length()
        {
            return vector.Length();
        }

        #region ICompress

        public override string ToString()
        {
            return $"{X.ToString()}{SEPARATOR}{Y.ToString()}";
        }

        public Position FromString(string compressed)
        {
            var splitted = compressed.Split(SEPARATOR);

            X = (float)Convert.ToDouble(splitted[0], CultureInfo.InvariantCulture);
            Y = (float)Convert.ToDouble(splitted[1], CultureInfo.InvariantCulture);

            return this;
        }

        #endregion
    }

    public static class PositionCollectionsExtensions
    {
        public static Position Center(this IEnumerable<Position> positions)
        {
            if (!positions.Any())
                throw new ArgumentException();

            return new Position(
                positions.Sum(x => x.X) / positions.Count(),
                positions.Sum(x => x.Y) / positions.Count());
        }

        public static float SumOfDistancesFromCenter(this IEnumerable<Position> positions)
        {
            var center =
                positions.Center();

            return positions.Sum(x => Position.Distance(center, x));
        }

        public static List<Position> PositionsInRelativeCoordinationSystem(this IEnumerable<Position> positionsInOldCoordinationSystem, Position newO, Position anyPointOnPositiveSideOfNewOY)
        {
            var movedCenter = positionsInOldCoordinationSystem.Select(p => p - newO);

            anyPointOnPositiveSideOfNewOY -= newO;

            float d = anyPointOnPositiveSideOfNewOY.Length();

            float sin = anyPointOnPositiveSideOfNewOY.X / d;
            float cos = anyPointOnPositiveSideOfNewOY.Y / d;

            if (float.IsNaN(sin))
                sin = 0.0f;

            if (float.IsNaN(cos))
                cos = 1.0f;

            return movedCenter.Select(p => new Position(p.X * cos - p.Y * sin, p.X * sin + p.Y * cos)).ToList();
        }
    }
}
