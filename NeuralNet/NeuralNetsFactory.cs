namespace NeuralNets
{
    public static class NeuralNetsFactory
    {
        public static NeuralNet GetRandomMultiLayerNeuralNet(NeuralNetParameters parameters)
        {
            var multiLayerNeuralNet = GetMultiLayerNeuralNet(parameters);

            multiLayerNeuralNet.Randomize();

            return multiLayerNeuralNet;
        }

        public static NeuralNet GetMultiLayerNeuralNet(NeuralNetParameters parameters)
        {
            return new NeuralNet(parameters);
        }
    }
}
