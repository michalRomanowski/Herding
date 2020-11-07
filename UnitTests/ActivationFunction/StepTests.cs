using ActivationFunctions;
using NUnit.Framework;

namespace UnitTests.ActivationFunction
{
    [TestFixture]
    class StepTests
    {
        [TestCase]
        public void NegativeInputTest()
        {
            var function = ActivationFunctionFactory.Get(EActivationFunctionType.Sum);

            var result = function.Impuls(-1);

            Assert.AreEqual(-1, result);
        }

        [TestCase]
        public void ZeroInputTest()
        {
            var function = ActivationFunctionFactory.Get(EActivationFunctionType.Sum);

            var result = function.Impuls(0);

            Assert.AreEqual(0, result);
        }

        [TestCase]
        public void PositiveInputTest()
        {
            var function = ActivationFunctionFactory.Get(EActivationFunctionType.Sum);

            var result = function.Impuls(1);

            Assert.AreEqual(1, result);
        }
    }
}
