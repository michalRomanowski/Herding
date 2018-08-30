using System.IO;

namespace NeuralNets
{
    public static class NeuralNetsProvider
    {
        public static NeuralNet GetRandomMultiLayerNeuralNet(int inputLayerSize, int outputLayerSize, int hiddenLayerSize, int numberOfHiddenLayers)
        {
            var randomMultiLayerNeuralNet = new NeuralNet(inputLayerSize, outputLayerSize, hiddenLayerSize, numberOfHiddenLayers);

            randomMultiLayerNeuralNet.Randomize();

            return randomMultiLayerNeuralNet;
        }
    }
}
