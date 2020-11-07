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
            get { return JaggedArraySerializer.Serialize(wagesBetweenInputAndFirstHiddenLayer); }
            set { wagesBetweenInputAndFirstHiddenLayer = JaggedArraySerializer.DeserializeLevel1(value); }
        }

        [XmlElement]
        public string WagesBetweenHiddenLayersSerializer
        {
            get { return JaggedArraySerializer.Serialize(wagesBetweenHiddenLayers); }
            set { wagesBetweenHiddenLayers = JaggedArraySerializer.DeserializeLevel2(value); }
        }

        [XmlElement]
        public string WagesBetweenLastHiddenAndOutputLayerSerializer
        {
            get { return JaggedArraySerializer.Serialize(wagesBetweenLastHiddenAndOutputLayer); }
            set { wagesBetweenLastHiddenAndOutputLayer = JaggedArraySerializer.DeserializeLevel1(value); }
        }

        [XmlElement]
        public string BiasesInHiddenLayersSerializer
        {
            get { return JaggedArraySerializer.Serialize(biasesInHiddenLayers); }
            set { biasesInHiddenLayers = JaggedArraySerializer.DeserializeLevel1(value); }
        }

        [XmlElement]
        public string BiasesInOutputLayerSerializer
        {
            get { return JaggedArraySerializer.Serialize(biasesInOutputLayer); }
            set { biasesInOutputLayer = JaggedArraySerializer.DeserializeLevel0(value); }
        }

        #endregion
        
        private double[][] wagesBetweenInputAndFirstHiddenLayer;
        private double[][][] wagesBetweenHiddenLayers;
        private double[][] wagesBetweenLastHiddenAndOutputLayer;

        private double[][] biasesInHiddenLayers;
        private double[] biasesInOutputLayer;

        private EActivationFunctionType activationFunctionType = EActivationFunctionType.Tanh;

        private IActivationFunction activationFunction =
            ActivationFunctionFactory.Get(EActivationFunctionType.Tanh);

        private NeuralNet() { }

        public NeuralNet(NeuralNetParameters parameters)
        {
            wagesBetweenInputAndFirstHiddenLayer = ArrayAllocatorUtils.Allocate<double>(parameters.InputLayerSize, parameters.HiddenLayerSize);
            wagesBetweenHiddenLayers = ArrayAllocatorUtils.Allocate<double>(parameters.NumberOfHiddenLayers - 1, parameters.HiddenLayerSize, parameters.HiddenLayerSize);
            wagesBetweenLastHiddenAndOutputLayer = ArrayAllocatorUtils.Allocate<double>(parameters.HiddenLayerSize, parameters.OutputLayerSize);
            biasesInHiddenLayers = ArrayAllocatorUtils.Allocate<double>(parameters.NumberOfHiddenLayers, parameters.HiddenLayerSize);
            biasesInOutputLayer = ArrayAllocatorUtils.Allocate<double>(parameters.OutputLayerSize);

            this.activationFunctionType = parameters.ActivationFunctionType;
            activationFunction = ActivationFunctionFactory.Get(activationFunctionType);
        }

        public NeuralNet GetClone()
        {
            return new NeuralNet
            {
                wagesBetweenInputAndFirstHiddenLayer = wagesBetweenInputAndFirstHiddenLayer.GetClone(),
                wagesBetweenHiddenLayers = wagesBetweenHiddenLayers.GetClone(),
                biasesInHiddenLayers = biasesInHiddenLayers.GetClone(),
                wagesBetweenLastHiddenAndOutputLayer = wagesBetweenLastHiddenAndOutputLayer.GetClone(),
                biasesInOutputLayer = biasesInOutputLayer.GetClone(),
                activationFunctionType = activationFunctionType,
                activationFunction = activationFunction
            };
        }

        public void Randomize()
        {
            wagesBetweenInputAndFirstHiddenLayer.Randomize(-1.0, 1.0);
            
            for(int i = 0; i < wagesBetweenHiddenLayers.Length; i++)
                wagesBetweenHiddenLayers[i].Randomize(-1.0, 1.0);

            for(int i = 0; i < biasesInHiddenLayers.Length; i++)
                biasesInHiddenLayers[i].Randomize(-1.0, 1.0);

            wagesBetweenLastHiddenAndOutputLayer.Randomize(-1.0, 1.0);

            biasesInOutputLayer.Randomize(-1.0, 1.0);
        }

        public double[] Think(double[] input)
        {
            var impulses = ThinkBetweenTwoLayers(input, wagesBetweenInputAndFirstHiddenLayer, biasesInHiddenLayers[0]);

            for(int i = 0; i < wagesBetweenHiddenLayers.Length; i++)
                impulses = ThinkBetweenTwoLayers(impulses, wagesBetweenHiddenLayers[i], biasesInHiddenLayers[i+1]);

            return ThinkBetweenTwoLayers(impulses, wagesBetweenLastHiddenAndOutputLayer, biasesInOutputLayer);
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

        public void Mutate(double chanceOfMutation)
        {
            float maxAddedValue = 1.0f;

            wagesBetweenInputAndFirstHiddenLayer.Mutate(chanceOfMutation, maxAddedValue);

            foreach (var wages in wagesBetweenHiddenLayers)
                wages.Mutate(chanceOfMutation, maxAddedValue);

            foreach (var biases in biasesInHiddenLayers)
                biases.Mutate(chanceOfMutation, maxAddedValue);
            
            wagesBetweenLastHiddenAndOutputLayer.Mutate(chanceOfMutation, maxAddedValue);

            biasesInOutputLayer.Mutate(chanceOfMutation, maxAddedValue);
        }

        public NeuralNet Crossover(NeuralNet other)
        {
            if (other is NeuralNet == false)
                throw new ArgumentException();

            var parsedOther = other as NeuralNet;

            var child = new NeuralNet(
                new NeuralNetParameters()
                {
                    ActivationFunctionType = activationFunctionType,
                    InputLayerSize = wagesBetweenInputAndFirstHiddenLayer.Length,
                    HiddenLayerSize = wagesBetweenLastHiddenAndOutputLayer[0].Length,
                    NumberOfHiddenLayers = wagesBetweenHiddenLayers.Length + 1,
                    OutputLayerSize = biasesInOutputLayer.Length
                });

            child.wagesBetweenInputAndFirstHiddenLayer =
                CrossoverUtils.Crossover(wagesBetweenInputAndFirstHiddenLayer, parsedOther.wagesBetweenInputAndFirstHiddenLayer);
            
            for (int i = 0; i < wagesBetweenHiddenLayers.Length; i++)
            {
                child.wagesBetweenHiddenLayers[i] =
                    CrossoverUtils.Crossover(wagesBetweenHiddenLayers[i], parsedOther.wagesBetweenHiddenLayers[i]);
            }
            for (int i = 0; i < biasesInHiddenLayers.Length; i++)
            {
                child.biasesInHiddenLayers[i] =
                    CrossoverUtils.Crossover(biasesInHiddenLayers[i], parsedOther.biasesInHiddenLayers[i]);
            }

            child.wagesBetweenLastHiddenAndOutputLayer =
                CrossoverUtils.Crossover(wagesBetweenLastHiddenAndOutputLayer, parsedOther.wagesBetweenLastHiddenAndOutputLayer);

            child.biasesInOutputLayer =
                CrossoverUtils.Crossover(biasesInOutputLayer, parsedOther.biasesInOutputLayer);

            if(StaticRandom.R.NextDouble() < 0.5)
            {
                child.activationFunctionType = other.activationFunctionType;
            }

            child.activationFunction = ActivationFunctionFactory.Get(child.activationFunctionType);

            return child;
        }

        public void Resize(int inputLayerSize, int numberOfHiddenLayers, int hiddenLayerSize)
        {
            wagesBetweenInputAndFirstHiddenLayer = wagesBetweenInputAndFirstHiddenLayer.Resize(inputLayerSize, hiddenLayerSize, 0.0);
            
            wagesBetweenHiddenLayers = wagesBetweenHiddenLayers.Resize(numberOfHiddenLayers - 1, hiddenLayerSize, hiddenLayerSize, 0.0);
            wagesBetweenLastHiddenAndOutputLayer = wagesBetweenLastHiddenAndOutputLayer.Resize(hiddenLayerSize, 2, 0.0);

            biasesInHiddenLayers = biasesInHiddenLayers.Resize(numberOfHiddenLayers, hiddenLayerSize, 0.0);
        }
    }
}
