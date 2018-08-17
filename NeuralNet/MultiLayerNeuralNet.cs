using ActivationFunctions;
using Auxiliary;
using System;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ShepardUnitTestProject")]

namespace NeuralNet
{
    class MultiLayerNeuralNet : INeuralNet
    {
        private float[,] wagesBetweenInputAndFirstHiddenLayer;
        private float[][,] wagesBetweenHiddenLayers;
        private float[,] wagesBetweenLastHiddenAndOutputLayer;

        private float[][] biasesInHiddenLayers;
        private float[] biasesInOutputLayer;

        private IActivationFunction activationFunction = 
            ActivationFunctionProvider.GetActivationFunction(EActivationFunctionType.Tanh);

        public MultiLayerNeuralNet(int inputLayerSize, int outputLayerSize, int hiddenLayerSize, int numberOfHiddenLayers, EActivationFunctionType activationFunctionType = EActivationFunctionType.Tanh)
        {
            this.wagesBetweenInputAndFirstHiddenLayer = new float[inputLayerSize, hiddenLayerSize];

            this.wagesBetweenHiddenLayers = new float[numberOfHiddenLayers - 1][,];
            this.biasesInHiddenLayers = new float[numberOfHiddenLayers][];

            for (int i = 0; i < this.wagesBetweenHiddenLayers.Length; i++ )
            {
                this.wagesBetweenHiddenLayers[i] = new float[hiddenLayerSize, hiddenLayerSize];
            }

            for (int i = 0; i < this.biasesInHiddenLayers.Length; i++)
            {
                this.biasesInHiddenLayers[i] = new float[hiddenLayerSize];
            }

            this.wagesBetweenLastHiddenAndOutputLayer = new float[hiddenLayerSize, outputLayerSize];
            this.biasesInOutputLayer = new float[outputLayerSize];

            this.activationFunction = ActivationFunctionProvider.GetActivationFunction(activationFunctionType);
        }

        public MultiLayerNeuralNet(MultiLayerNeuralNet pattern)
        {
            // Copy input layer wages.
            this.wagesBetweenInputAndFirstHiddenLayer = 
                new float
                    [pattern.wagesBetweenInputAndFirstHiddenLayer.GetLength(0),
                    pattern.wagesBetweenInputAndFirstHiddenLayer.GetLength(1)];

            Array.Copy(
                pattern.wagesBetweenInputAndFirstHiddenLayer,
                this.wagesBetweenInputAndFirstHiddenLayer,
                pattern.wagesBetweenInputAndFirstHiddenLayer.GetLength(0) * pattern.wagesBetweenInputAndFirstHiddenLayer.GetLength(1));

            //Copy hidden layers wages and biases.
            this.wagesBetweenHiddenLayers = new float[pattern.wagesBetweenHiddenLayers.GetLength(0)][,];
            
            for (int i = 0; i < this.wagesBetweenHiddenLayers.Length; i++)
            {
                this.wagesBetweenHiddenLayers[i] = 
                    new float[pattern.wagesBetweenHiddenLayers[i].GetLength(0), pattern.wagesBetweenHiddenLayers[i].GetLength(1)];

                Array.Copy(
                pattern.wagesBetweenHiddenLayers[i],
                this.wagesBetweenHiddenLayers[i],
                pattern.wagesBetweenHiddenLayers[i].GetLength(0) * pattern.wagesBetweenHiddenLayers[i].GetLength(1));
            }

            this.biasesInHiddenLayers = new float[pattern.biasesInHiddenLayers.Length][];

            for(int i = 0; i < this.biasesInHiddenLayers.Length; i++)
            {
                this.biasesInHiddenLayers[i] = new float[pattern.biasesInHiddenLayers[i].Length];

                Array.Copy(
                    pattern.biasesInHiddenLayers[i],
                    this.biasesInHiddenLayers[i],
                    pattern.biasesInHiddenLayers[i].Length);
            }

            // Copy output layer wages and biases.
            this.wagesBetweenLastHiddenAndOutputLayer =
                new float
                    [pattern.wagesBetweenLastHiddenAndOutputLayer.GetLength(0),
                    pattern.wagesBetweenLastHiddenAndOutputLayer.GetLength(1)];

            Array.Copy(
                pattern.wagesBetweenLastHiddenAndOutputLayer,
                this.wagesBetweenLastHiddenAndOutputLayer,
                pattern.wagesBetweenLastHiddenAndOutputLayer.GetLength(0) * pattern.wagesBetweenLastHiddenAndOutputLayer.GetLength(1));

            this.biasesInOutputLayer = new float[pattern.biasesInOutputLayer.Length];

            Array.Copy(
                pattern.biasesInOutputLayer,
                this.biasesInOutputLayer,
                pattern.biasesInOutputLayer.Length);

            // Copy activation function
            this.activationFunction = pattern.activationFunction;
        }

        public MultiLayerNeuralNet(StringReader compressed)
        {
            this.wagesBetweenInputAndFirstHiddenLayer = StreamHelper.Decompress2DimFloatArray(compressed);

            var numberOfHiddenLayers = Convert.ToInt32(compressed.ReadLine());
            wagesBetweenHiddenLayers = new float[numberOfHiddenLayers][,];

            for (int i = 0; i < this.wagesBetweenHiddenLayers.Length; i++)
            {
                this.wagesBetweenHiddenLayers[i] = StreamHelper.Decompress2DimFloatArray(compressed);
            }

            this.biasesInHiddenLayers = StreamHelper.Decompress2DimFloatJaggedArray(compressed);
            this.wagesBetweenLastHiddenAndOutputLayer = StreamHelper.Decompress2DimFloatArray(compressed);
            this.biasesInOutputLayer = StreamHelper.Decompress1DimFloatArray(compressed);
        }

        public void Randomize()
        {
            CRandom.Randmize(ref this.wagesBetweenInputAndFirstHiddenLayer);

            for(int i = 0; i < this.wagesBetweenHiddenLayers.Length; i++)
            {
                CRandom.Randmize(ref this.wagesBetweenHiddenLayers[i]);
            }

            for (int i = 0; i < this.biasesInHiddenLayers.Length; i++)
            {
                CRandom.Randmize(ref this.biasesInHiddenLayers[i]);
            }

            CRandom.Randmize(ref this.wagesBetweenLastHiddenAndOutputLayer);

            CRandom.Randmize(ref this.biasesInOutputLayer);
        }

        public float[] Think(float[] input)
        {
            float[] impulses = this.ThinkBetweenTwoLayers(input, this.wagesBetweenInputAndFirstHiddenLayer, this.biasesInHiddenLayers[0]);

            for(int i = 0; i < this.wagesBetweenHiddenLayers.GetLength(0); i++)
            {
                impulses = this.ThinkBetweenTwoLayers(impulses, this.wagesBetweenHiddenLayers[i], this.biasesInHiddenLayers[i+1]);
            }

            return this.ThinkBetweenTwoLayers(impulses, this.wagesBetweenLastHiddenAndOutputLayer, this.biasesInOutputLayer);
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

                output[i] = this.activationFunction.Impuls(net);
            }

            return output;
        }

        public void Mutate(float chanceOfMutation, float maxAddeValue = 1.0f)
        {
            this.Mutate(ref this.wagesBetweenInputAndFirstHiddenLayer, chanceOfMutation, maxAddeValue);

            for (int i = 0; i < this.wagesBetweenHiddenLayers.Length; i++ )
            {
                this.Mutate(ref this.wagesBetweenHiddenLayers[i], chanceOfMutation, maxAddeValue);
            }

            for (int i = 0; i < this.biasesInHiddenLayers.Length; i++)
            {
                this.Mutate(ref this.biasesInHiddenLayers[i], chanceOfMutation, maxAddeValue);
            }

            this.Mutate(ref this.wagesBetweenLastHiddenAndOutputLayer, chanceOfMutation, maxAddeValue);

            this.Mutate(ref this.biasesInOutputLayer, chanceOfMutation, maxAddeValue);
        }

        private void Mutate(ref float[,] valuesToMutate, float chanceOfMutation, float maxAddeValue = 1.0f)
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
        }

        private void Mutate(ref float[] valuesToMutate, float chanceOfMutation, float maxAddeValue = 1.0f)
        {
            for (int i = 0; i < valuesToMutate.GetLength(0); i++)
            {
                if (CRandom.NextFloat() < chanceOfMutation)
                {
                    valuesToMutate[i] += CRandom.NextFloat(-maxAddeValue, maxAddeValue);
                }
            }
        }

        public INeuralNet Crossover(INeuralNet other)
        {
            if (other is MultiLayerNeuralNet == false)
            {
                throw new ApplicationException("'other' should be of type COneLayerNeuralNet to be able to crossover with COneLayerNeuralNet.");
            }

            var child = new MultiLayerNeuralNet(
                this.wagesBetweenInputAndFirstHiddenLayer.GetLength(0),
                this.wagesBetweenLastHiddenAndOutputLayer.GetLength(1),
                this.wagesBetweenInputAndFirstHiddenLayer.GetLength(1),
                this.wagesBetweenHiddenLayers.Length + 1);

            var castedOther = other as MultiLayerNeuralNet;

            child.wagesBetweenInputAndFirstHiddenLayer =
                CrossoverHelper.Crossover(this.wagesBetweenInputAndFirstHiddenLayer, castedOther.wagesBetweenInputAndFirstHiddenLayer);

            for (int i = 0; i < this.wagesBetweenHiddenLayers.Length; i++)
            {
                child.wagesBetweenHiddenLayers[i] =
                    CrossoverHelper.Crossover(this.wagesBetweenHiddenLayers[i], castedOther.wagesBetweenHiddenLayers[i]);
            }

            for (int i = 0; i < this.biasesInHiddenLayers.Length; i++)
            {
                child.biasesInHiddenLayers[i] =
                    CrossoverHelper.Crossover(this.biasesInHiddenLayers[i], castedOther.biasesInHiddenLayers[i]);
            }

            child.wagesBetweenLastHiddenAndOutputLayer =
                CrossoverHelper.Crossover(this.wagesBetweenLastHiddenAndOutputLayer, castedOther.wagesBetweenLastHiddenAndOutputLayer);

            child.biasesInOutputLayer =
                CrossoverHelper.Crossover(this.biasesInOutputLayer, castedOther.biasesInOutputLayer);

            return child;
        }

        public void AdjustInputLayerSize(int newSize)
        {
            var numberOfNeuronsToAdd = newSize - wagesBetweenInputAndFirstHiddenLayer.GetLength(0);

            if (numberOfNeuronsToAdd == 0)
                return;

            var newArray = new float[wagesBetweenInputAndFirstHiddenLayer.GetLength(0) + numberOfNeuronsToAdd, wagesBetweenInputAndFirstHiddenLayer.GetLength(1)];
            Array.Copy(wagesBetweenInputAndFirstHiddenLayer, newArray, wagesBetweenInputAndFirstHiddenLayer.GetLength(0));
            wagesBetweenInputAndFirstHiddenLayer = newArray;
        }

        public void AdjustHiddenLayersSize(int newSize)
        {
            throw new NotImplementedException();
        }
        
        public INeuralNet Clone()
        {
            return new MultiLayerNeuralNet(this);
        }
        
        public string Compress()
        {
            StringWriter sw = new StringWriter();

            StreamHelper.Compress(sw, this.wagesBetweenInputAndFirstHiddenLayer);

            sw.WriteLine(this.wagesBetweenHiddenLayers.Length);

            for(int i = 0; i < this.wagesBetweenHiddenLayers.Length; i++)
            {
                StreamHelper.Compress(sw, this.wagesBetweenHiddenLayers[i]);
            }

            StreamHelper.Compress(sw, this.biasesInHiddenLayers);

            StreamHelper.Compress(sw, this.wagesBetweenLastHiddenAndOutputLayer);

            StreamHelper.Compress(sw, this.biasesInOutputLayer);

            return sw.ToString();
        }
    }
}
