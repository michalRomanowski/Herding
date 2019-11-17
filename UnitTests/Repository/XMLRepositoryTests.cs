using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Simulations;

namespace UnitTests.Repository
{
    [TestClass]
    public class XMLRepositoryTests
    {
        [TestMethod]
        public void SaveLoadOptimizationParameters1Test()
        {
            SaveLoadOptimizationParametersTest(TestObjects.GetOptimizationParameters1());
        }

        [TestMethod]
        public void SaveLoadOptimizationParameters2Test()
        {
            SaveLoadOptimizationParametersTest(TestObjects.GetOptimizationParameters2());
        }

        [TestMethod]
        public void SaveLoadOptimizationParameters3Test()
        {
            SaveLoadOptimizationParametersTest(TestObjects.GetOptimizationParametersWithRandomSets());
        }

        [TestMethod]
        public void SaveLoadPopulationTest()
        {
            var population = new Population();

            new XMLRepository(AppDomain.CurrentDomain.BaseDirectory).Save("test", population);



        }

        private void SaveLoadOptimizationParametersTest(OptimizationParameters parameters)
        {
            var savedParameters = parameters;

            new XMLRepository(AppDomain.CurrentDomain.BaseDirectory).Save("test", savedParameters);

            var loadedParameters = new XMLRepository(AppDomain.CurrentDomain.BaseDirectory).LoadOptimizationParameters("test");
            
            MyAssert.AreEqual(savedParameters, loadedParameters);
        }
    }
}
