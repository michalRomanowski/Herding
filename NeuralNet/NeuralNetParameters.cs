using ActivationFunctions;

namespace NeuralNets
{
    public class NeuralNetParameters
    {
        public int InputLayerSize { get; set; }
        public int OutputLayerSize { get; set; }
        public int HiddenLayerSize { get; set; }
        public int NumberOfHiddenLayers { get; set; }
        public EActivationFunctionType ActivationFunctionType = EActivationFunctionType.Tanh;
    }
}
