namespace Teams
{
    public static class TeamFactory
    {
        public static Team GetTeam(TeamParameters parameters)
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
