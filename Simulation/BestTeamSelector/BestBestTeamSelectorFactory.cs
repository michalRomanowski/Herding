namespace Simulations
{
    static class BestBestTeamSelectorFactory
    {
        public static IBestTeamSelector GetBestTeamSelector(SimulationParameters simulationParamaters)
        {
            if (simulationParamaters.RandomPositions)
            {
                return new RandomPositionsBestTeamSelector(simulationParamaters);
            }
            else
            {
                return new FixedPositionsBestTeamSelector(simulationParamaters);
            }
        }
    }
}
