namespace NeuralNets
{
    public static class NeuralNetsFactory
    {
        public static NeuralNet GetRandomMultiLayerNeuralNet(NeuralNetParameters parameters)
        {
            var randomMultiLayerNeuralNet = new NeuralNet(parameters);

            randomMultiLayerNeuralNet.Randomize();

            return randomMultiLayerNeuralNet;
        }
    }
}
