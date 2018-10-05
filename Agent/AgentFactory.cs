using System;
using System.Collections.Generic;
using System.Linq;
using Auxiliary;

namespace Agent
{
    public static class AgentFactory
    {
        public static ThinkingAgent GetShepherd(
            int numberOfSeenShepherds,
            int numberOfSeenSheep,
            int numberOfHiddenLayers,
            int sizeOfHiddenLayer)
        {
            return new Shepherd(
                numberOfSeenShepherds,
                numberOfSeenSheep,
                numberOfHiddenLayers,
                sizeOfHiddenLayer);
        }

        public static IList<IMovingAgent> GetSheep(IList<Position> positions, ESheepType sheepType, int seed)
        {
            switch (sheepType)
            {
                case ESheepType.Passive:
                    {
                        return positions.Select(x => new PassiveSheep(x.X, x.Y) as IMovingAgent).ToList();
                    }
                case ESheepType.Wandering:
                    {
                        var r = new Random(seed);
                        return positions.Select(x => new WanderingSheep(x.X, x.Y, r.Next()) as IMovingAgent).ToList();
                    }
                default:
                    {
                        throw new ArgumentException();
                    }
            }
        }
    }
}
