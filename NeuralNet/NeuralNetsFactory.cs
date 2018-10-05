namespace NeuralNets
{
    public static class NeuralNetsFactory
    {
        public static INeuralNet GetRandomMultiLayerNeuralNet(int inputLayerSize, int outputLayerSize, int hiddenLayerSize, int numberOfHiddenLayers)
        {
            var randomMultiLayerNeuralNet = new NeuralNet(inputLayerSize, outputLayerSize, hiddenLayerSize, numberOfHiddenLayers);

            randomMultiLayerNeuralNet.Randomize();

            return randomMultiLayerNeuralNet;
        }

        public static INeuralNet GetMultiLayerNeuralNet(string compressed)
        {
            return new NeuralNet().FromString(compressed);
        }
    }
}
