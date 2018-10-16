namespace Teams
{
    public static class TeamFactory
    {
        public static Team GetTeam(bool notIdenticalAgents)
        {
            if (notIdenticalAgents)
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
