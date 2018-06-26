using Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agent
{
    public static class SheepProvider
    {
        public static IList<ISheep> GetSheep(IList<Position> positions, ESheepType sheepType, int seed)
        {
            switch(sheepType)
            {
                case ESheepType.Passive:
                {
                    return positions.Select(x => new PassiveSheep(x.X, x.Y) as ISheep).ToList();
                }
                case ESheepType.Wandering:
                {
                    var r = new Random(seed);
                    return positions.Select(x => new WanderingSheep(x.X, x.Y, r.Next()) as ISheep).ToList();
                }
                default:
                {
                    throw new Exception(
                        "Invalid sheepType: " + sheepType.ToString() + ". Expected: " +
                        ESheepType.Passive.ToString() + " or " + ESheepType.Wandering.ToString() + ".");
                }
            }
        }
    }
}
