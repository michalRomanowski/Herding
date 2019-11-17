namespace Teams
{
    public static class TeamFactory
    {
        public static Team GetTeam(ITeamParameters parameters)
        {
            if (parameters.NotIdenticalAgents)
            {
                return new NotIdenticalTeam(parameters);
            }
            else
            {
                return new IdenticalTeam(parameters);
            }
        }
    }
}
