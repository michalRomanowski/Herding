using ActivationFunctions;
using NeuralNets;
using NUnit.Framework;

namespace UnitTests.NeuralNets
{
    [TestFixture]
    class NeuralNetTests
    {
        [TestCase]
        public void GetCloneTest()
        {
            var neuralNetParameters = new NeuralNetParameters()
            {
                ActivationFunctionType = EActivationFunctionType.Tanh,
                InputLayerSize = 2,
                HiddenLayerSize = 3,
                OutputLayerSize = 2,
                NumberOfHiddenLayers = 1
            };

            var neuralNet = new NeuralNet(neuralNetParameters);
            neuralNet.Randomize();
            
            var input = new double[] { 1.0, -1.0 };

            var clone = neuralNet.GetClone();
            
            var result1 = neuralNet.Think(input);
            var result2 = clone.Think(input);

            Assert.AreEqual(2, result1.Length);
            Assert.AreEqual(2, result2.Length);
            Assert.AreEqual(result1[0], result2[0]);
            Assert.AreEqual(result1[1], result2[1]);
            Assert.AreNotSame(neuralNet, clone);
        }

        [TestCase]
        public void RandomizeTest()
        {
            var neuralNetParameters = new NeuralNetParameters()
            {
                ActivationFunctionType = EActivationFunctionType.Tanh,
                InputLayerSize = 2,
                HiddenLayerSize = 3,
                OutputLayerSize = 2,
                NumberOfHiddenLayers = 1
            };
            
            var neuralNet = new NeuralNet(neuralNetParameters);

            var input = new double[] { 1.0, -1.0 };

            neuralNet.Randomize();
            var result1 = neuralNet.Think(input);

            neuralNet.Randomize();
            var result2 = neuralNet.Think(input);
            
            Assert.AreEqual(2, result1.Length);
            Assert.AreEqual(2, result2.Length);
            Assert.AreNotEqual(result1[0], result2[0]);
            Assert.AreNotEqual(result1[1], result2[1]);
        }

        [TestCase]
        public void ThinkTest()
        {
            var neuralNetParameters = new NeuralNetParameters()
            {
                ActivationFunctionType = EActivationFunctionType.Tanh,
                InputLayerSize = 2,
                HiddenLayerSize = 3,
                OutputLayerSize = 2,
                NumberOfHiddenLayers = 1
            };

            var neuralNet = new NeuralNet(neuralNetParameters);

            var input = new double[] { 1.0, -1.0 };

            neuralNet.Randomize();
            var result = neuralNet.Think(input);

            Assert.AreEqual(2, result.Length);
            Assert.AreNotEqual(0.0, result[0]);
            Assert.AreNotEqual(0.0, result[1]);
        }

        [TestCase]
        public void MutateTest()
        {
            var neuralNetParameters = new NeuralNetParameters()
            {
                ActivationFunctionType = EActivationFunctionType.Tanh,
                InputLayerSize = 2,
                HiddenLayerSize = 3,
                OutputLayerSize = 2,
                NumberOfHiddenLayers = 1
            };

            var neuralNet = new NeuralNet(neuralNetParameters);

            var input = new double[] { 1.0, -1.0 };

            neuralNet.Randomize();
            var result1 = neuralNet.Think(input);

            neuralNet.Mutate(1.0);
            var result2 = neuralNet.Think(input);
            
            Assert.AreEqual(2, result1.Length);
            Assert.AreEqual(2, result2.Length);
            Assert.AreNotEqual(result1[0], result2[0]);
            Assert.AreNotEqual(result1[1], result2[1]);
        }

        [TestCase]
        public void MutateZeroChanceTest()
        {
            var neuralNetParameters = new NeuralNetParameters()
            {
                ActivationFunctionType = EActivationFunctionType.Tanh,
                InputLayerSize = 2,
                HiddenLayerSize = 3,
                OutputLayerSize = 2,
                NumberOfHiddenLayers = 1
            };

            var neuralNet = new NeuralNet(neuralNetParameters);

            var input = new double[] { 1.0, -1.0 };

            neuralNet.Randomize();
            var result1 = neuralNet.Think(input);

            neuralNet.Mutate(0.0);
            var result2 = neuralNet.Think(input);

            Assert.AreEqual(2, result1.Length);
            Assert.AreEqual(2, result2.Length);
            Assert.AreEqual(result1[0], result2[0]);
            Assert.AreEqual(result1[1], result2[1]);
        }

        [TestCase]
        public void CrossoverDifferentNeuralNetsTest()
        {
            var neuralNetParameters = new NeuralNetParameters()
            {
                ActivationFunctionType = EActivationFunctionType.Tanh,
                InputLayerSize = 2,
                HiddenLayerSize = 3,
                OutputLayerSize = 2,
                NumberOfHiddenLayers = 1
            };

            var neuralNet1 = new NeuralNet(neuralNetParameters);
            var neuralNet2 = new NeuralNet(neuralNetParameters);

            neuralNet1.Randomize();
            neuralNet2.Randomize();
            
            var child = neuralNet1.Crossover(neuralNet2);

            var input = new double[] { 1.0, -1.0 };

            var neuralNet1Result = neuralNet1.Think(input);
            var neuralNet2Result = neuralNet2.Think(input);
            var childResult = child.Think(input);
            
            Assert.AreEqual(2, childResult.Length);
            Assert.AreNotEqual(neuralNet1Result[0], childResult[0]);
            Assert.AreNotEqual(neuralNet1Result[1], childResult[1]);
            Assert.AreNotEqual(neuralNet2Result[0], childResult[0]);
            Assert.AreNotEqual(neuralNet2Result[1], childResult[1]);
        }

        [TestCase]
        public void CrossoverIdenticalNeuralNetsTest()
        {
            var neuralNetParameters = new NeuralNetParameters()
            {
                ActivationFunctionType = EActivationFunctionType.Tanh,
                InputLayerSize = 2,
                HiddenLayerSize = 3,
                OutputLayerSize = 2,
                NumberOfHiddenLayers = 1
            };

            var neuralNet1 = new NeuralNet(neuralNetParameters);
            neuralNet1.Randomize();
            var neuralNet2 = neuralNet1.GetClone();
            
            var child = neuralNet1.Crossover(neuralNet2);

            var input = new double[] { 1.0, -1.0 };

            var neuralNet1Result = neuralNet1.Think(input);
            var neuralNet2Result = neuralNet2.Think(input);
            var childResult = child.Think(input);

            Assert.AreEqual(2, childResult.Length);
            Assert.AreEqual(neuralNet1Result[0], childResult[0]);
            Assert.AreEqual(neuralNet1Result[1], childResult[1]);
            Assert.AreEqual(neuralNet2Result[0], childResult[0]);
            Assert.AreEqual(neuralNet2Result[1], childResult[1]);
        }

        [TestCase]
        public void ResizeInputLayerTest()
        {
            var neuralNetParameters = new NeuralNetParameters()
            {
                ActivationFunctionType = EActivationFunctionType.Tanh,
                InputLayerSize = 2,
                HiddenLayerSize = 3,
                OutputLayerSize = 2,
                NumberOfHiddenLayers = 1
            };

            var neuralNet = new NeuralNet(neuralNetParameters);

            var input1 = new double[] { 1.0, -1.0 };
            var input2 = new double[] { 1.0, -1.0, 1.0 };

            neuralNet.Randomize();
            var result1 = neuralNet.Think(input1);

            neuralNet.Resize(3, 1, 3);
            var result2 = neuralNet.Think(input2);

            Assert.AreEqual(2, result2.Length);
            Assert.AreEqual(result1[0], result2[0]);
            Assert.AreEqual(result1[1], result2[1]);
        }

        [TestCase]
        public void ResizeHiddenLayerTest()
        {
            var neuralNetParameters = new NeuralNetParameters()
            {
                ActivationFunctionType = EActivationFunctionType.Tanh,
                InputLayerSize = 2,
                HiddenLayerSize = 3,
                OutputLayerSize = 2,
                NumberOfHiddenLayers = 1
            };

            var neuralNet = new NeuralNet(neuralNetParameters);

            var input = new double[] { 1.0, -1.0 };

            neuralNet.Randomize();
            var result1 = neuralNet.Think(input);

            neuralNet.Resize(2, 1, 4);
            var result2 = neuralNet.Think(input);

            Assert.AreEqual(2, result2.Length);
            Assert.AreEqual(result1[0], result2[0]);
            Assert.AreEqual(result1[1], result2[1]);
        }
    }
}
