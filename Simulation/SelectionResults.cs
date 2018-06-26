using System.Collections.Generic;
using Team;

namespace Simulation
{
    public class SelectionResults
    {
        public IList<ITeam> parents = new List<ITeam>();
        public IList<ITeam> toDie = new List<ITeam>();
    }
}
