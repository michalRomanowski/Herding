using NeuralNets;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.NeuralNets
{
    [TestFixture]
    class ArrayCloneExtensionsTests
    {
        [TestCase]
        public void OneDimArrayGetCloneTest()
        {
            var array = new double[] { 1.0, 2.0};

            var result = array.GetClone();

            Assert.AreNotSame(array, result);
            Assert.AreEqual(array, result);
        }

        [TestCase]
        public void TwoDimArrayGetCloneTest()
        {
            var array = new double[,] { { 1.0, 2.0 }, { 3.0, 4.0 } };

            var result = array.GetClone();

            Assert.AreNotSame(array, result);
            Assert.AreEqual(array, result);
        }

        [TestCase]
        public void ArrayOfOneDimArraysGetCloneTest()
        {
            var array = new double[][] {
                new double[] { 1.0, 2.0 },
                new double[] { 5.0, 6.0 }};

            var result = array.GetClone();

            Assert.AreNotSame(array, result);
            Assert.AreEqual(array, result);
        }

        [TestCase]
        public void ArrayOfTwoDimArraysGetCloneTest()
        {
            var array = new double[][,] {  
                new double[,] { { 1.0, 2.0 }, { 3.0, 4.0 } },
                new double[,] { { 5.0, 6.0 }, { 7.0, 8.0 } }};

            var result = array.GetClone();

            Assert.AreNotSame(array, result);
            Assert.AreEqual(array, result);
        }
    }
}
