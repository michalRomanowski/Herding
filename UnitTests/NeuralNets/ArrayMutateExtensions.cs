using NeuralNets;
using NUnit.Framework;

namespace UnitTests.NeuralNets
{
    [TestFixture]
    class ArrayMutateExtensions
    {
        [Test]
        public void OneDimArrayMutateTest()
        {
            var array = new double[] { 1.0, 2.0 };

            array.Mutate(0.5);

            Assert.AreEqual(2, array.Length);
        }

        [Test]
        public void ArrayOfArraysMutateTest()
        {
            var array = new double[][] {
                new double[] { 1.0, 2.0 },
                new double[] { 5.0, 6.0 }};

            array.Mutate(0.5);

            Assert.AreEqual(2, array.Length);
            Assert.AreEqual(2, array[0].Length);
            Assert.AreEqual(2, array[1].Length);
        }
    }
}