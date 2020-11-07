using NeuralNets;
using NUnit.Framework;

namespace UnitTests.NeuralNets
{
    [TestFixture]
    class CrossoverUtilsTests
    {
        [TestCase]
        [Repeat(100)]
        public void ArrayCrossoverTest()
        {
            var array1 = new double[] { 1.0, 2.0 };
            var array2 = new double[] { 3.0, 4.0 };

            var result = CrossoverUtils.Crossover(array1, array2);

            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(1.0 == result[0] || 3.0 == result[0]);
            Assert.IsTrue(2.0 == result[1] || 4.0 == result[1]);
        }

        [TestCase]
        [Repeat(100)]
        public void ArrayOfArraysCrossoverTest()
        {
            var array1 = new double[][] { new double[] { 1.0, 2.0 }, new double[] { 3.0, 4.0 }, };
            var array2 = new double[][] { new double[] { 5.0, 6.0 }, new double[] { 7.0, 8.0 }, };

            var result = CrossoverUtils.Crossover(array1, array2);

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(2, result[0].Length);
            Assert.AreEqual(2, result[1].Length);
            Assert.IsTrue(1.0 == result[0][0] || 5.0 == result[0][0]);
            Assert.IsTrue(2.0 == result[0][1] || 6.0 == result[0][1]);
            Assert.IsTrue(3.0 == result[1][0] || 7.0 == result[1][0]);
            Assert.IsTrue(4.0 == result[1][1] || 8.0 == result[1][1]);
        }
    }
}