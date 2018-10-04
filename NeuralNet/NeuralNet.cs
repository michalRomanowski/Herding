using ActivationFunctions;
using Auxiliary;
using System;
using System.IO;

namespace NeuralNets
{
    public class NeuralNet : ICompress<NeuralNet>, ICloneable<NeuralNet>
    {
        private float[,] WagesBetweenInputAndFirstHiddenLayer { get; set; }
        private float[][,] WagesBetweenHiddenLayers { get; set; }
        private float[,] WagesBetweenLastHiddenAndOutputLayer { get; set; }

        private float[][] BiasesInHiddenLayers { get; set; }
        private float[] BiasesInOutputLayer { get; set; }

        public IActivationFunction activationFunction =
            ActivationFunctionProvider.GetActivationFunction(EActivationFunctionType.Tanh);

        public NeuralNet() { }

        public NeuralNet(int inputLayerSize, int outputLayerSize, int hiddenLayerSize, int numberOfHiddenLayers, EActivationFunctionType activationFunctionType = EActivationFunctionType.Tanh)
        {
            this.WagesBetweenInputAndFirstHiddenLayer = new float[inputLayerSize, hiddenLayerSize];

            this.WagesBetweenHiddenLayers = new float[numberOfHiddenLayers - 1][,];
            this.BiasesInHiddenLayers = new float[numberOfHiddenLayers][];

            for (int i = 0; i < this.WagesBetweenHiddenLayers.Length; i++ )
                this.WagesBetweenHiddenLayers[i] = new float[hiddenLayerSize, hiddenLayerSize];

            for (int i = 0; i < this.BiasesInHiddenLayers.Length; i++)
                this.BiasesInHiddenLayers[i] = new float[hiddenLayerSize];

            this.WagesBetweenLastHiddenAndOutputLayer = new float[hiddenLayerSize, outputLayerSize];
            this.BiasesInOutputLayer = new float[outputLayerSize];

            this.activationFunction = ActivationFunctionProvider.GetActivationFunction(activationFunctionType);
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

        public float[] Think(float[] input)
        {
            float[] impulses = ThinkBetweenTwoLayers(input, WagesBetweenInputAndFirstHiddenLayer, BiasesInHiddenLayers[0]);

            for(int i = 0; i < WagesBetweenHiddenLayers.GetLength(0); i++)
                impulses = ThinkBetweenTwoLayers(impulses, WagesBetweenHiddenLayers[i], BiasesInHiddenLayers[i+1]);

            return ThinkBetweenTwoLayers(impulses, WagesBetweenLastHiddenAndOutputLayer, BiasesInOutputLayer);
        }

        private float[] ThinkBetweenTwoLayers(float[] input, float[,] wages, float[] biases)
        {
            float[] output = new float[wages.GetLength(1)];

            if(output.Length != biases.Length)
                throw new ArgumentException();

            for (int i = 0; i < output.Length; i++)
            {
                float net = biases[i];

                for (int j = 0; j < wages.GetLength(0); j++)
                    net += input[j] * wages[j, i];

                output[i] = activationFunction.Impuls(net);
            }

            return output;
        }

        public void Mutate(float chanceOfMutation, float maxAddeValue = 1.0f)
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
            var child = new NeuralNet(
                WagesBetweenInputAndFirstHiddenLayer.GetLength(0),
                WagesBetweenLastHiddenAndOutputLayer.GetLength(1),
                WagesBetweenInputAndFirstHiddenLayer.GetLength(1),
                WagesBetweenHiddenLayers.Length + 1);

            child.WagesBetweenInputAndFirstHiddenLayer =
                CrossoverHelper.Crossover(WagesBetweenInputAndFirstHiddenLayer, other.WagesBetweenInputAndFirstHiddenLayer);

            for (int i = 0; i < WagesBetweenHiddenLayers.Length; i++)
            {
                child.WagesBetweenHiddenLayers[i] =
                    CrossoverHelper.Crossover(WagesBetweenHiddenLayers[i], other.WagesBetweenHiddenLayers[i]);
            }

            for (int i = 0; i < BiasesInHiddenLayers.Length; i++)
            {
                child.BiasesInHiddenLayers[i] =
                    CrossoverHelper.Crossover(BiasesInHiddenLayers[i], other.BiasesInHiddenLayers[i]);
            }

            child.WagesBetweenLastHiddenAndOutputLayer =
                CrossoverHelper.Crossover(WagesBetweenLastHiddenAndOutputLayer, other.WagesBetweenLastHiddenAndOutputLayer);

            child.BiasesInOutputLayer =
                CrossoverHelper.Crossover(BiasesInOutputLayer, other.BiasesInOutputLayer);

            return child;
        }

        public void AdjustInputLayerSize(int newSize)
        {
            var numberOfNeuronsToAdd = newSize - WagesBetweenInputAndFirstHiddenLayer.GetLength(0);

            if (numberOfNeuronsToAdd == 0)
                return;

            var newArray = new float[WagesBetweenInputAndFirstHiddenLayer.GetLength(0) + numberOfNeuronsToAdd, WagesBetweenInputAndFirstHiddenLayer.GetLength(1)];
            Array.Copy(WagesBetweenInputAndFirstHiddenLayer, newArray, WagesBetweenInputAndFirstHiddenLayer.GetLength(0));
            WagesBetweenInputAndFirstHiddenLayer = newArray;
        }

        public void AdjustHiddenLayersSize(int newSize)
        {
            throw new NotImplementedException();
        }

        #region ICompress

        public override string ToString()
        {
            StringWriter sw = new StringWriter();

            StreamHelper.Compress(sw, WagesBetweenInputAndFirstHiddenLayer);

            sw.WriteLine(WagesBetweenHiddenLayers.Length);

            for (int i = 0; i < WagesBetweenHiddenLayers.Length; i++)
            {
                StreamHelper.Compress(sw, WagesBetweenHiddenLayers[i]);
            }

            StreamHelper.Compress(sw, BiasesInHiddenLayers);
            StreamHelper.Compress(sw, WagesBetweenLastHiddenAndOutputLayer);
            StreamHelper.Compress(sw, BiasesInOutputLayer);

            return sw.ToString();
        }

        public NeuralNet FromString(string compressed)
        {
            using (var sr = new StringReader(compressed))
            {
                WagesBetweenInputAndFirstHiddenLayer = StreamHelper.Decompress2DimFloatArray(sr);

                var numberOfHiddenLayers = Convert.ToInt32(sr.ReadLine());
                WagesBetweenHiddenLayers = new float[numberOfHiddenLayers][,];

                for (int i = 0; i < WagesBetweenHiddenLayers.Length; i++)
                {
                    WagesBetweenHiddenLayers[i] = StreamHelper.Decompress2DimFloatArray(sr);
                }

                BiasesInHiddenLayers = StreamHelper.Decompress2DimFloatJaggedArray(sr);
                WagesBetweenLastHiddenAndOutputLayer = StreamHelper.Decompress2DimFloatArray(sr);
                BiasesInOutputLayer = StreamHelper.Decompress1DimFloatArray(sr);
            }

            return this;
        }

        #endregion
    }
}
