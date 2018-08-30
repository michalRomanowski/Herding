using Teams;

namespace Simulations
{
    public static class TeamProvider
    {
        public static Team GetTeam(SimulationParameters simulationParameters)
        {
            if(simulationParameters.NotIdenticalAgents)
            {
                return new NotIdenticalTeam();
            }
            else
            {
                return new IdenticalTeam();
            }
        }
    }
}
