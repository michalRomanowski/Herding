using System.Xml.Serialization;
using ActivationFunctions;
using Auxiliary;
using System;

namespace NeuralNets
{
    [Serializable]
    public class NeuralNet
    {
        #region Serializers

        [XmlElement]
        public string WagesBetweenInputAndFirstHiddenLayerSerializer
        {
            get { return JaggedArraySerializer.Serialize(WagesBetweenInputAndFirstHiddenLayer); }
            set { WagesBetweenInputAndFirstHiddenLayer = JaggedArraySerializer.DeserializeLevel1(value); }
        }

        [XmlElement]
        public string WagesBetweenHiddenLayersSerializer
        {
            get { return JaggedArraySerializer.Serialize(WagesBetweenHiddenLayers); }
            set { WagesBetweenHiddenLayers = JaggedArraySerializer.DeserializeLevel2(value); }
        }

        [XmlElement]
        public string WagesBetweenLastHiddenAndOutputLayerSerializer
        {
            get { return JaggedArraySerializer.Serialize(WagesBetweenLastHiddenAndOutputLayer); }
            set { WagesBetweenLastHiddenAndOutputLayer = JaggedArraySerializer.DeserializeLevel1(value); }
        }

        [XmlElement]
        public string BiasesInHiddenLayersSerializer
        {
            get { return JaggedArraySerializer.Serialize(BiasesInHiddenLayers); }
            set { BiasesInHiddenLayers = JaggedArraySerializer.DeserializeLevel1(value); }
        }

        [XmlElement]
        public string BiasesInOutputLayerSerializer
        {
            get { return JaggedArraySerializer.Serialize(BiasesInOutputLayer); }
            set { BiasesInOutputLayer = JaggedArraySerializer.DeserializeLevel0(value); }
        }

        #endregion

        private double[][] WagesBetweenInputAndFirstHiddenLayer;
        private double[][][] WagesBetweenHiddenLayers;
        private double[][] WagesBetweenLastHiddenAndOutputLayer;

        private double[][] BiasesInHiddenLayers;
        private double[] BiasesInOutputLayer;

        private const double RESIZE_DEFAULT_VALUE = 0.0;
        
        private ActivationFunction activationFunction =
            ActivationFunctionFactory.GetActivationFunction(EActivationFunctionType.Tanh);

        public NeuralNet() { }

        public NeuralNet(NeuralNetParameters parameters) : this(parameters.InputLayerSize, parameters.OutputLayerSize, parameters.HiddenLayerSize, parameters.NumberOfHiddenLayers, parameters.ActivationFunctionType)
        { }

        public NeuralNet(int inputLayerSize, int outputLayerSize, int hiddenLayerSize, int numberOfHiddenLayers, EActivationFunctionType activationFunctionType = EActivationFunctionType.Tanh)
        {
            WagesBetweenInputAndFirstHiddenLayer = ArrayFactory.Allocate<double>(inputLayerSize, hiddenLayerSize);
            WagesBetweenHiddenLayers = ArrayFactory.Allocate<double>(numberOfHiddenLayers - 1, hiddenLayerSize, hiddenLayerSize);
            WagesBetweenLastHiddenAndOutputLayer = ArrayFactory.Allocate<double>(hiddenLayerSize, outputLayerSize);
            BiasesInHiddenLayers = ArrayFactory.Allocate<double>(numberOfHiddenLayers, hiddenLayerSize);
            BiasesInOutputLayer = ArrayFactory.Allocate<double>(outputLayerSize);

            activationFunction = ActivationFunctionFactory.GetActivationFunction(activationFunctionType);
        }

        public NeuralNet GetClone()
        {
            return new NeuralNet
            {
                WagesBetweenInputAndFirstHiddenLayer = WagesBetweenInputAndFirstHiddenLayer.GetClone(),
                WagesBetweenHiddenLayers = WagesBetweenHiddenLayers.GetClone(),
                BiasesInHiddenLayers = BiasesInHiddenLayers.GetClone(),
                WagesBetweenLastHiddenAndOutputLayer = WagesBetweenLastHiddenAndOutputLayer.GetClone(),
                BiasesInOutputLayer = BiasesInOutputLayer.GetClone(),
                activationFunction = activationFunction
            };
        }

        public void Randomize()
        {
            WagesBetweenInputAndFirstHiddenLayer.Randmize();
            
            for(int i = 0; i < WagesBetweenHiddenLayers.Length; i++)
                WagesBetweenHiddenLayers[i].Randmize();

            for (int i = 0; i < BiasesInHiddenLayers.Length; i++)
                BiasesInHiddenLayers[i].Randmize();

            WagesBetweenLastHiddenAndOutputLayer.Randmize();

            BiasesInOutputLayer.Randmize();
        }

        public double[] Think(double[] input)
        {
            var impulses = ThinkBetweenTwoLayers(input, WagesBetweenInputAndFirstHiddenLayer, BiasesInHiddenLayers[0]);

            for(int i = 0; i < WagesBetweenHiddenLayers.Length; i++)
                impulses = ThinkBetweenTwoLayers(impulses, WagesBetweenHiddenLayers[i], BiasesInHiddenLayers[i+1]);

            return ThinkBetweenTwoLayers(impulses, WagesBetweenLastHiddenAndOutputLayer, BiasesInOutputLayer);
        }

        private double[] ThinkBetweenTwoLayers(double[] input, double[][] wages, double[] biases)
        {
            var output = new double[wages[0].Length];

            if(output.Length != biases.Length)
                throw new ArgumentException();

            for (int i = 0; i < output.Length; i++)
            {
                var net = biases[i];

                for (int j = 0; j < wages.GetLength(0); j++)
                    net += input[j] * wages[j][i];

                output[i] = activationFunction.Impuls(net);
            }

            return output;
        }

        public void Mutate(double chanceOfMutation, double maxAddeValue = 1.0f)
        {
            WagesBetweenInputAndFirstHiddenLayer.Mutate(chanceOfMutation, maxAddeValue);

            foreach (var wages in WagesBetweenHiddenLayers)
                wages.Mutate(chanceOfMutation, maxAddeValue);

            foreach (var biases in BiasesInHiddenLayers)
                biases.Mutate(chanceOfMutation, maxAddeValue);
            
            WagesBetweenLastHiddenAndOutputLayer.Mutate(chanceOfMutation, maxAddeValue);

            BiasesInOutputLayer.Mutate(chanceOfMutation, maxAddeValue);
        }

        public NeuralNet Crossover(NeuralNet other)
        {
            if (other is NeuralNet == false)
                throw new ArgumentException();

            var parsedOther = other as NeuralNet;

            var child = new NeuralNet(
                WagesBetweenInputAndFirstHiddenLayer.Length,
                WagesBetweenLastHiddenAndOutputLayer[0].Length,
                WagesBetweenInputAndFirstHiddenLayer[0].Length,
                WagesBetweenHiddenLayers.Length + 1);

            child.WagesBetweenInputAndFirstHiddenLayer =
                CrossoverHelper.Crossover(WagesBetweenInputAndFirstHiddenLayer, parsedOther.WagesBetweenInputAndFirstHiddenLayer);

            for (int i = 0; i < WagesBetweenHiddenLayers.Length; i++)
            {
                child.WagesBetweenHiddenLayers[i] =
                    CrossoverHelper.Crossover(WagesBetweenHiddenLayers[i], parsedOther.WagesBetweenHiddenLayers[i]);
            }

            for (int i = 0; i < BiasesInHiddenLayers.Length; i++)
            {
                child.BiasesInHiddenLayers[i] =
                    CrossoverHelper.Crossover(BiasesInHiddenLayers[i], parsedOther.BiasesInHiddenLayers[i]);
            }

            child.WagesBetweenLastHiddenAndOutputLayer =
                CrossoverHelper.Crossover(WagesBetweenLastHiddenAndOutputLayer, parsedOther.WagesBetweenLastHiddenAndOutputLayer);

            child.BiasesInOutputLayer =
                CrossoverHelper.Crossover(BiasesInOutputLayer, parsedOther.BiasesInOutputLayer);

            return child;
        }

        public void Resize(int inputLayerSize, int numberOfHiddenLayers, int hiddenLayerSize)
        {
            if (WagesBetweenInputAndFirstHiddenLayer.GetLength(0) == inputLayerSize
                && WagesBetweenHiddenLayers.GetLength(0) == numberOfHiddenLayers - 1
                && BiasesInHiddenLayers[0].GetLength(0) == hiddenLayerSize)
                return;

            WagesBetweenInputAndFirstHiddenLayer = WagesBetweenInputAndFirstHiddenLayer.Resize(inputLayerSize, hiddenLayerSize, RESIZE_DEFAULT_VALUE);
            WagesBetweenHiddenLayers = WagesBetweenHiddenLayers.Resize(numberOfHiddenLayers - 1, hiddenLayerSize, hiddenLayerSize, RESIZE_DEFAULT_VALUE);
            WagesBetweenLastHiddenAndOutputLayer = WagesBetweenLastHiddenAndOutputLayer.Resize(hiddenLayerSize, 2, RESIZE_DEFAULT_VALUE);

            BiasesInHiddenLayers = BiasesInHiddenLayers.Resize(numberOfHiddenLayers, hiddenLayerSize, RESIZE_DEFAULT_VALUE);
        }
    }
}
