using Auxiliary;
using NUnit.Framework;

namespace UnitTests.Auxiliary
{
    [TestFixture]
    class ArrayExtensionsTests
    {
        [TestCase]
        [Repeat(100)]
        public void RandomizeArrayTest()
        {
            var array = new double[] { 0.0, 0.0 };

            array.Randomize(-1.0, 1.0);

            foreach (var element in array)
            {
                Assert.AreNotEqual(element, 0.0);
                Assert.GreaterOrEqual(element, -1.0);
                Assert.LessOrEqual(element, 1.0);
            }
        }

        [TestCase]
        [Repeat(100)]
        public void RandomizeArrayOfArraysTest()
        {
            var array = ArrayAllocatorUtils.Allocate<double>(2, 3);

            array.Randomize(-1.0, 1.0);

            foreach(var a in array)
            {
                foreach(var element in a)
                {
                    Assert.AreNotEqual(element, 0.0);
                    Assert.GreaterOrEqual(element, - 1.0);
                    Assert.LessOrEqual(element, 1.0);
                }
            }
        }
    }
}