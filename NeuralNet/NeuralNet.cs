using ActivationFunctions;
using Auxiliary;
using System;
using System.IO;

namespace NeuralNets
{
    public class NeuralNet : ICompress<NeuralNet>
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
            {
                this.WagesBetweenHiddenLayers[i] = new float[hiddenLayerSize, hiddenLayerSize];
            }

            for (int i = 0; i < this.BiasesInHiddenLayers.Length; i++)
            {
                this.BiasesInHiddenLayers[i] = new float[hiddenLayerSize];
            }

            this.WagesBetweenLastHiddenAndOutputLayer = new float[hiddenLayerSize, outputLayerSize];
            this.BiasesInOutputLayer = new float[outputLayerSize];

            this.activationFunction = ActivationFunctionProvider.GetActivationFunction(activationFunctionType);
        }

        public NeuralNet(NeuralNet pattern)
        {
            // Copy input layer wages.
            this.WagesBetweenInputAndFirstHiddenLayer = 
                new float
                    [pattern.WagesBetweenInputAndFirstHiddenLayer.GetLength(0),
                    pattern.WagesBetweenInputAndFirstHiddenLayer.GetLength(1)];

            Array.Copy(
                pattern.WagesBetweenInputAndFirstHiddenLayer,
                this.WagesBetweenInputAndFirstHiddenLayer,
                pattern.WagesBetweenInputAndFirstHiddenLayer.GetLength(0) * pattern.WagesBetweenInputAndFirstHiddenLayer.GetLength(1));

            //Copy hidden layers wages and biases.
            this.WagesBetweenHiddenLayers = new float[pattern.WagesBetweenHiddenLayers.GetLength(0)][,];
            
            for (int i = 0; i < this.WagesBetweenHiddenLayers.Length; i++)
            {
                this.WagesBetweenHiddenLayers[i] = 
                    new float[pattern.WagesBetweenHiddenLayers[i].GetLength(0), pattern.WagesBetweenHiddenLayers[i].GetLength(1)];

                Array.Copy(
                pattern.WagesBetweenHiddenLayers[i],
                this.WagesBetweenHiddenLayers[i],
                pattern.WagesBetweenHiddenLayers[i].GetLength(0) * pattern.WagesBetweenHiddenLayers[i].GetLength(1));
            }

            this.BiasesInHiddenLayers = new float[pattern.BiasesInHiddenLayers.Length][];

            for(int i = 0; i < this.BiasesInHiddenLayers.Length; i++)
            {
                this.BiasesInHiddenLayers[i] = new float[pattern.BiasesInHiddenLayers[i].Length];

                Array.Copy(
                    pattern.BiasesInHiddenLayers[i],
                    this.BiasesInHiddenLayers[i],
                    pattern.BiasesInHiddenLayers[i].Length);
            }

            // Copy output layer wages and biases.
            this.WagesBetweenLastHiddenAndOutputLayer =
                new float
                    [pattern.WagesBetweenLastHiddenAndOutputLayer.GetLength(0),
                    pattern.WagesBetweenLastHiddenAndOutputLayer.GetLength(1)];

            Array.Copy(
                pattern.WagesBetweenLastHiddenAndOutputLayer,
                this.WagesBetweenLastHiddenAndOutputLayer,
                pattern.WagesBetweenLastHiddenAndOutputLayer.GetLength(0) * pattern.WagesBetweenLastHiddenAndOutputLayer.GetLength(1));

            this.BiasesInOutputLayer = new float[pattern.BiasesInOutputLayer.Length];

            Array.Copy(
                pattern.BiasesInOutputLayer,
                this.BiasesInOutputLayer,
                pattern.BiasesInOutputLayer.Length);

            // Copy activation function
            this.activationFunction = pattern.activationFunction;
        }

        public void Randomize()
        {
            WagesBetweenInputAndFirstHiddenLayer = CRandom.Randmize(WagesBetweenInputAndFirstHiddenLayer);


            for(int i = 0; i < WagesBetweenHiddenLayers.Length; i++)
            {
                WagesBetweenHiddenLayers[i] = CRandom.Randmize(WagesBetweenHiddenLayers[i]);
            }

            for (int i = 0; i < BiasesInHiddenLayers.Length; i++)
            {
                BiasesInHiddenLayers[i] = CRandom.Randmize(BiasesInHiddenLayers[i]);
            }

            WagesBetweenLastHiddenAndOutputLayer = CRandom.Randmize(WagesBetweenLastHiddenAndOutputLayer);

            BiasesInOutputLayer = CRandom.Randmize(BiasesInOutputLayer);
        }

        public float[] Think(float[] input)
        {
            float[] impulses = ThinkBetweenTwoLayers(input, WagesBetweenInputAndFirstHiddenLayer, BiasesInHiddenLayers[0]);

            for(int i = 0; i < WagesBetweenHiddenLayers.GetLength(0); i++)
            {
                impulses = ThinkBetweenTwoLayers(impulses, WagesBetweenHiddenLayers[i], BiasesInHiddenLayers[i+1]);
            }

            return ThinkBetweenTwoLayers(impulses, WagesBetweenLastHiddenAndOutputLayer, BiasesInOutputLayer);
        }

        private float[] ThinkBetweenTwoLayers(float[] input, float[,] wages, float[] biases)
        {
            float[] output = new float[wages.GetLength(1)];

            if(output.Length != biases.Length)
            {
                throw new ApplicationException("Biases size must be equal to output size.");
            }

            float net;

            for (int i = 0; i < output.Length; i++)
            {
                net = biases[i];

                for (int j = 0; j < wages.GetLength(0); j++)
                {
                    net += input[j] * wages[j, i];
                }

                output[i] = activationFunction.Impuls(net);
            }

            return output;
        }

        public void Mutate(float chanceOfMutation, float maxAddeValue = 1.0f)
        {
            WagesBetweenInputAndFirstHiddenLayer = Mutate(WagesBetweenInputAndFirstHiddenLayer, chanceOfMutation, maxAddeValue);

            for (int i = 0; i < WagesBetweenHiddenLayers.Length; i++ )
            {
                WagesBetweenHiddenLayers[i] = Mutate(WagesBetweenHiddenLayers[i], chanceOfMutation, maxAddeValue);
            }

            for (int i = 0; i < BiasesInHiddenLayers.Length; i++)
            {
                BiasesInHiddenLayers[i] = Mutate(BiasesInHiddenLayers[i], chanceOfMutation, maxAddeValue);
            }

            WagesBetweenLastHiddenAndOutputLayer = Mutate(WagesBetweenLastHiddenAndOutputLayer, chanceOfMutation, maxAddeValue);

            BiasesInOutputLayer = Mutate(BiasesInOutputLayer, chanceOfMutation, maxAddeValue);
        }

        private float[,] Mutate(float[,] valuesToMutate, float chanceOfMutation, float maxAddeValue = 1.0f)
        {
            for (int i = 0; i < valuesToMutate.GetLength(0); i++)
            {
                for (int j = 0; j < valuesToMutate.GetLength(1); j++)
                {
                    if (CRandom.NextFloat() < chanceOfMutation)
                    {
                        valuesToMutate[i, j] += CRandom.NextFloat(-maxAddeValue, maxAddeValue);
                    }
                }
            }

            return valuesToMutate;
        }

        private float[] Mutate(float[] valuesToMutate, float chanceOfMutation, float maxAddeValue = 1.0f)
        {
            for (int i = 0; i < valuesToMutate.GetLength(0); i++)
            {
                if (CRandom.NextFloat() < chanceOfMutation)
                {
                    valuesToMutate[i] += CRandom.NextFloat(-maxAddeValue, maxAddeValue);
                }
            }

            return valuesToMutate;
        }

        public NeuralNet Crossover(NeuralNet other)
        {
            if (other is NeuralNet == false)
            {
                throw new ApplicationException("'other' should be of type COneLayerNeuralNet to be able to crossover with COneLayerNeuralNet.");
            }

            var child = new NeuralNet(
                WagesBetweenInputAndFirstHiddenLayer.GetLength(0),
                WagesBetweenLastHiddenAndOutputLayer.GetLength(1),
                WagesBetweenInputAndFirstHiddenLayer.GetLength(1),
                WagesBetweenHiddenLayers.Length + 1);

            var castedOther = other as NeuralNet;

            child.WagesBetweenInputAndFirstHiddenLayer =
                CrossoverHelper.Crossover(WagesBetweenInputAndFirstHiddenLayer, castedOther.WagesBetweenInputAndFirstHiddenLayer);

            for (int i = 0; i < WagesBetweenHiddenLayers.Length; i++)
            {
                child.WagesBetweenHiddenLayers[i] =
                    CrossoverHelper.Crossover(WagesBetweenHiddenLayers[i], castedOther.WagesBetweenHiddenLayers[i]);
            }

            for (int i = 0; i < BiasesInHiddenLayers.Length; i++)
            {
                child.BiasesInHiddenLayers[i] =
                    CrossoverHelper.Crossover(BiasesInHiddenLayers[i], castedOther.BiasesInHiddenLayers[i]);
            }

            child.WagesBetweenLastHiddenAndOutputLayer =
                CrossoverHelper.Crossover(WagesBetweenLastHiddenAndOutputLayer, castedOther.WagesBetweenLastHiddenAndOutputLayer);

            child.BiasesInOutputLayer =
                CrossoverHelper.Crossover(BiasesInOutputLayer, castedOther.BiasesInOutputLayer);

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
    }
}
