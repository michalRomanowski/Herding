using Team;

namespace Simulation
{
    public static class TeamProvider
    {
        public static ITeam GetTeam(SimulationParameters simulationParameters, string compressed)
        {
            if (simulationParameters.NotIdenticalAgents)
            {
                return new NotIdenticalTeam(compressed);
            }
            else
            {
                return new IdenticalTeam(simulationParameters.NumberOfShepards, compressed);
            }
        }

        public static ITeam GetTeam(SimulationParameters simulationParameters)
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
