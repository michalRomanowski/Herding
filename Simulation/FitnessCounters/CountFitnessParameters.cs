using System.Collections.Generic;
using Agent;
using Auxiliary;
using Teams;

namespace Simulations
{
    public class CountFitnessParameters
    {
        public int TurnsOfHerding;
        public IList<IList<Position>> PositionsOfShepherdsSet;
        public IList<IList<Position>> PositionsOfSheepSet;
        public ESheepType SheepType;
        public int Seed;
    }
}
