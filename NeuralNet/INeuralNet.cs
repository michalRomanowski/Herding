using Auxiliary;

public interface INeuralNet : ICompress<INeuralNet>, ICloneable<INeuralNet>
{
    float[] Think(float[] input);
    void Mutate(float chanceOfMutation, float maxAddeValue);
    INeuralNet Crossover(INeuralNet other);
    void Resize(int inputLayerSize, int numberOfHiddenLayers, int hiddenLayerSize);
}

interface IRandomize
{
    void Randomize();
}