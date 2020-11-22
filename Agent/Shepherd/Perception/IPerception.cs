namespace Agent
{
    public interface IPerception
    {
        EPerceptionType PerceptionType { get; }
        double TransformPerception(double input);
    }
}
