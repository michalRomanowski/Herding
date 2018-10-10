using System.Collections.Generic;
using Teams;

namespace Simulations
{
    interface IBestTeamSelector
    {
        Team GetBestTeam(IEnumerable<Team> teams);
    }
}
