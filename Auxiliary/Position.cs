﻿using System;
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

        public static float SquaredDistance(Position A, Position B)
        {
            float dx = A.X - B.X;
            float dy = A.Y - B.Y;
            return dx * dx + dy * dy;
        }

        public static float Length(Position A)
        {
            return A.X * A.X + A.Y * A.Y;
        }

        public static float Distance(Position A, Position B)
        {
            return (float)Math.Sqrt(Position.SquaredDistance(A, B));
        }

        public static List<Position> PositionsInRelativeCoordinationSystem(Position newO, Position anyPointOnPositiveSideOfNewOY, IEnumerable<Position> positionsInOldCoordinationSystem)
        {
            var movedCentre = positionsInOldCoordinationSystem.Select(p => new Position(p.X - newO.X, p.Y - newO.Y)).ToList<Position>();

            anyPointOnPositiveSideOfNewOY = new Position(
                anyPointOnPositiveSideOfNewOY.X - newO.X, anyPointOnPositiveSideOfNewOY.Y - newO.Y);
            
            float d = (float)Math.Sqrt(anyPointOnPositiveSideOfNewOY.X * anyPointOnPositiveSideOfNewOY.X + anyPointOnPositiveSideOfNewOY.Y * anyPointOnPositiveSideOfNewOY.Y);

            float sin = anyPointOnPositiveSideOfNewOY.X / d;
            float cos = anyPointOnPositiveSideOfNewOY.Y / d;

            if (float.IsNaN(sin))
                sin = 0.0f;

            if (float.IsNaN(cos))
                cos = 1.0f;

            return movedCentre.Select(p => new Position(p.X * cos - p.Y * sin, p.X * sin + p.Y * cos)).ToList();
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

    public static class PositionEnumerableExtensions
    {
        public static Position CentreOfGravity(this IEnumerable<Position> positions)
        {
            if (!positions.Any())
                throw new ArgumentException();

            Position centerOfGravity = new Position(0, 0);
            int count = 0;

            foreach (var p in positions)
            {
                centerOfGravity.X += p.X;
                centerOfGravity.Y += p.Y;
                count++;
            }

            centerOfGravity.X /= count;
            centerOfGravity.Y /= count;

            return centerOfGravity;
        }

        public static float SumOfDistancesFromCentreOfGravity(this IEnumerable<Position> positions)
        {
            var centerOfGravity =
                positions.CentreOfGravity();

            float sumOfDistances = 0.0f;

            foreach (var p in positions)
            {
                sumOfDistances += Position.Distance(centerOfGravity, p);
            }

            return sumOfDistances;
        }
    }
}
