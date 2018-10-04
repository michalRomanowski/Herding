using Auxiliary;

public interface INeuralNet : ICompress<INeuralNet>, ICloneable<INeuralNet>
{
    float[] Think(float[] input);
    void Mutate(float chanceOfMutation, float maxAddeValue);
    INeuralNet Crossover(INeuralNet other);
    void AdjustInputLayerSize(int newSize);
    void AdjustHiddenLayersSize(int newSize);
}

interface IRandomize
{
    void Randomize();
}