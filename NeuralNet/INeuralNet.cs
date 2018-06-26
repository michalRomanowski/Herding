namespace NeuralNet
{
    public interface INeuralNet : IRandomize, IGenetic<INeuralNet>, IClone, ICompress
    {
        float[] Think(float[] input);
        void AdjustInputLayerSize(int newSize);
        void AdjustHiddenLayersSize(int newSize);
    }

    public interface IRandomize
    {
        void Randomize();
    }

    public interface IGenetic<T>
    {
        void Mutate(float chanceOfMutation, float maxAddeValue = 1.0f);
        T Crossover(T other);
    }

    public interface IClone
    {
        INeuralNet Clone();
    }

    public interface ICompress
    {
        string Compress();
    }
}
