using System.Collections.Generic;
using Teams;

namespace Simulations
{
    public class SelectionResults
    {
        public IList<Team> parents = new List<Team>();
        public IList<Team> toDie = new List<Team>();
    }
}
