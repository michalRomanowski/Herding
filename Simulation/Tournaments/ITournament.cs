using System.Collections.Generic;
using Teams;

namespace Simulations
{
    interface ITournament
    {        
        IEnumerable<Team> Attend();
    }
}
