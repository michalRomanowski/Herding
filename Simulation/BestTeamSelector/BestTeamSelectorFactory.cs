namespace Simulations
{
    static class BestTeamSelectorFactory
    {
        public static IBestTeamSelector GetBestTeamSelector(BestTeamSelectorParameters parameters)
        {
            return new BestTeamSelector(
                FitnessCounterFactory.GetFitnessCounter(parameters.FitnessType, parameters.CountFitnessParameters));
        }
    }
}