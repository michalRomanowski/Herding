namespace NeuralNets
{
    public static class NeuralNetsFactory
    {
        public static INeuralNet GetRandomMultiLayerNeuralNet(NeuralNetParameters parameters)
        {
            var randomMultiLayerNeuralNet = new NeuralNet(parameters);

            randomMultiLayerNeuralNet.Randomize();

            return randomMultiLayerNeuralNet;
        }

        public static INeuralNet GetMultiLayerNeuralNet(string compressed)
        {
            return new NeuralNet().FromString(compressed);
        }
    }
}
