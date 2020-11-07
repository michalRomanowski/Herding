using Auxiliary;
using NUnit.Framework;
using System;

namespace UnitTests.Auxiliary
{
    [TestFixture]
    class RandomExtensionsTests
    {
        [TestCase]
        [Repeat(100)]
        public void NextDoubleTest()
        {
            var r = new Random();

            var result = r.NextDouble(1.0, 2.0);

            Assert.GreaterOrEqual(result, 1.0);
            Assert.LessOrEqual(result, 2.0);
        }
    }
}
