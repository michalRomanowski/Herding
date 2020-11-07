using Auxiliary;
using NUnit.Framework;

namespace UnitTests.Auxiliary
{
    [TestFixture]
    class ArrayAllocatorUtilsTests
    {
        [TestCase]
        public void AllocateArrayTest()
        {
            var result = ArrayAllocatorUtils.Allocate<double>(2);

            Assert.AreEqual(2, result.Length);
        }

        [TestCase]
        public void AllocateArrayOfArraysTest()
        {
            var result = ArrayAllocatorUtils.Allocate<double>(2, 3);

            Assert.AreEqual(2, result.Length);
            for(int i = 0; i < 2; i++)
            {
                Assert.AreEqual(3, result[i].Length);
            }
        }

        [TestCase]
        public void AllocateArrayOfArraysOfArraysTest()
        {
            var result = ArrayAllocatorUtils.Allocate<double>(2, 3, 4);

            Assert.AreEqual(2, result.Length);
            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual(3, result[i].Length);

                for (int j = 0; j < 2; j++)
                {
                    Assert.AreEqual(4, result[i][j].Length);
                }
            }
        }
    }
}