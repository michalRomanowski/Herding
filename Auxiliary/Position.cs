using System;
using System.Collections.Generic;
using System.Linq;

namespace Auxiliary
{
    public class Position : ICompress<Position>
    {
        private const char SEPARATOR = ';';
        
        public float X { get; set; }
        public float Y { get; set; }

        public class DistanceFromOOComparer : IComparer<Position>
        {
            public int Compare(Position a, Position b)
            {
                if (a.X * a.X + a.Y * a.Y > b.X * b.X + b.Y * b.Y)
                {
                    return 1;
                }
                else return -1;
            }
        }

        public Position()
        { }

        public Position(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public Position(Position value)
        {
            this.X = value.X;
            this.Y = value.Y;
        }

        public Position(string value)
        {
            var splitted = value.Split(SEPARATOR);

            X = Convert.ToInt32(splitted[0]);
            Y = Convert.ToInt32(splitted[1]);
        }

        public static Position operator+(Position a, Position b)
        {
            return new Position(
                a.X + b.X,
                a.Y + b.Y);
        }

        public static Position operator-(Position a, Position b)
        {
            return new Position(
                a.X - b.X,
                a.Y - b.Y);
        }

        public static float SquaredDistance(Position a, Position b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            return dx * dx + dy * dy;
        }

        public static float Distance(Position a, Position b)
        {
            return (float)Math.Sqrt(SquaredDistance(a, b));
        }

        #region ICompress

        public override string ToString()
        {
            return $"{X.ToString()}{SEPARATOR}{Y.ToString()}";
        }

        public Position FromString(string compressed)
        {
            var splitted = compressed.Split(SEPARATOR);

            X = Convert.ToInt32(splitted[0]);
            Y = Convert.ToInt32(splitted[1]);

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

            float d = Position.Distance(new Position(0, 0), anyPointOnPositiveSideOfNewOY);

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
