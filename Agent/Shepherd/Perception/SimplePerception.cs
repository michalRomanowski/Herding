namespace Agent
{
    class SimplePerception : IPerception
    {
        public EPerceptionType PerceptionType => EPerceptionType.Simple;

        public double TransformPerception(double input)
        {
            return input;
        }
    }
}