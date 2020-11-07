using Agent;

namespace Teams
{
    public interface ITeamParameters : IShepherdParameters
    {
        bool NotIdenticalAgents { get; }
        int NumberOfShepherds { get; }
    }
}