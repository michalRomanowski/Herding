using Agent;

namespace Teams
{
    public class TeamParameters
    {
        public bool NotIdenticalAgents { get; set; }
        public int NumberOfShepherds { get; set; }

        public ShepherdParameters ShepherdParameters;
    }
}
