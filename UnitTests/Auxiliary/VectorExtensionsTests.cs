using Auxiliary;
using MathNet.Spatial.Euclidean;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests.Auxiliary
{
    [TestFixture]
    class VectorExtensionsTests
    {
        [TestCase]
        public void FixedDistanceTest()
        {
            var a = new Vector2D(2.0, 3.0);
            var b = new Vector2D(3.0, 4.0);

            Assert.AreEqual(1.4142, a.Distance(b), 0.0001);
        }

        [TestCase]
        public void DistanceTest()
        {
            var a = new Vector2D(2.0, 3.0);
            var b = new Vector2D(3.0, 4.0);

            Assert.AreEqual(a.Distance(b), b.Distance(a));
        }

        [TestCase]
        public void SquaredDistanceTest()
        {
            var a = new Vector2D(2.0, 3.0);
            var b = new Vector2D(3.0, 4.0);

            Assert.AreEqual(2.0, a.SquaredDistance(b), 0.0001);
        }

        [TestCase(5.0, 0.0, 1.0, 0.0, 1.0)]
        [TestCase(8.0, -8.0, 1.0, -1.0, 1.4142)]
        public void CutToMaxLengthTest(double x, double y, double expectedX, double expectedY, double maxLength)
        {
            var vector = new Vector2D(x, y);
            var result = vector.CutToMaxLength(maxLength);

            Assert.AreEqual(expectedX, result.X, 0.0001);
            Assert.AreEqual(expectedY, result.Y, 0.0001);
        }

        [TestCase]
        public void RoundTest()
        {
            var result = new Vector2D(-1.1111111111, 1.1111111111).Round();

            Assert.AreEqual(-1.1111111, result.X);
            Assert.AreEqual(1.1111111, result.Y);
        }

        [TestCase]
        public void CenterTest()
        {
            var points = new List<Vector2D>{
                new Vector2D(1.0, 1.0),
                new Vector2D(-1.0, 1.0),
                new Vector2D(1.0, -1.0),
                new Vector2D(-1.0, -1.0)
            };

            var result = points.Center();

            Assert.AreEqual(0.0, result.X);
            Assert.AreEqual(0.0, result.Y);
        }

        [TestCase]
        public void SumOfDistancesFromCenterTest()
        {
            var points = new List<Vector2D>{
                new Vector2D(1.0, 1.0),
                new Vector2D(-1.0, 1.0),
                new Vector2D(1.0, -1.0),
                new Vector2D(-1.0, -1.0)
            };

            var result = points.SumOfDistancesFromCenter();

            Assert.AreEqual(5.6568, result, 0.0001);
        }

        [TestCase]
        [Repeat(100)]
        public void RandomizeTest()
        {
            var collection = new List<Vector2D>() { new Vector2D(), new Vector2D() };

            var result = collection.Randomize(-1.0, 1.0);

            foreach (var element in result)
            {
                Assert.AreNotEqual(0.0, element.X);
                Assert.AreNotEqual(0.0, element.Y);
                Assert.GreaterOrEqual(element.X, -1.0);
                Assert.GreaterOrEqual(element.Y, -1.0);
                Assert.LessOrEqual(element.X, 1.0);
                Assert.LessOrEqual(element.Y, 1.0);
            }
        }

        [TestCase]
        public void PositionInRotatedCoordinationSystemTest()
        {
            var point = new Vector2D(1.0, 1.0);

            var result = point.PositionInRotatedCoordinationSystem(new Vector2D(1.0, 0.0));

            Assert.AreEqual(-1.0, result.X);
            Assert.AreEqual(1.0, result.Y);
        }

        [TestCase]
        public void PositionInRelativeCoordinationSystemTest()
        {
            var point = new Vector2D(1.0, 1.0);

            var result = point.PositionInRelativeCoordinationSystem(new Vector2D(-1.0, 0.0), new Vector2D(1.0, 0.0));

            Assert.AreEqual(-1.0, result.X);
            Assert.AreEqual(2.0, result.Y);
        }
    }
}