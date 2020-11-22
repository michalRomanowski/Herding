namespace Agent
{
    public interface IShepherdParameters
    {
        int NumberOfSeenShepherds { get; }
        int NumberOfSeenSheep { get; }
        int NumberOfHiddenLayers { get; }
        int NumberOfNeuronsInHiddenLayer { get; }
        EPerceptionType PerceptionType { get; }
        bool RandomizeNeuralNetOnInit { get; }
    }
}
