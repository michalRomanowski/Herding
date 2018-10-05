using Agent;
using Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace World
{
    public static class Finder
    {
        public static IEnumerable<IHasPosition> FindAgentsAtRange(IEnumerable<IHasPosition> agents, Position center, float range)
        {
            float squaredRange = range * range;

            return agents.Where(x => Position.SquaredDistance(center, x.Position) <= squaredRange);
        }

        public static IEnumerable<IHasPosition> FindClosestAgents(IEnumerable<IHasPosition> agents, IHasPosition center, int numberOfObjectsToFind)
        {
            var distanceComparer = new AgentAndDistanceComparer();

            var closestObjects =
                agents
                    .Where(x => x != center)
                    .Select(x => new AgentAndDistance(x, Position.SquaredDistance(center.Position, x.Position)))
                    .ToList();

            closestObjects.Sort(distanceComparer);

            var timesToCopyFirstAgent = numberOfObjectsToFind - closestObjects.Count();

            List<IHasPosition> closestAgents = new List<IHasPosition>();

            for (int i = 0; i < timesToCopyFirstAgent; i++)
            {
                closestAgents.Add(closestObjects.First().movingAgent);
            }

            closestAgents.AddRange(closestObjects.Take(numberOfObjectsToFind).Select(x => x.movingAgent));

            return closestAgents;
        }
    }
}

public class AgentAndDistance
{
    public IHasPosition movingAgent;
    public float squaredDistance;

    public AgentAndDistance(IHasPosition movingAgent, float squaredDistance)
    {
        this.movingAgent = movingAgent;
        this.squaredDistance = squaredDistance;
    }
}

public class AgentAndDistanceComparer : IComparer<AgentAndDistance>
{
    public int Compare(AgentAndDistance a,AgentAndDistance b)
    {
        if (a.squaredDistance > b.squaredDistance) return 1;
        else if (a.squaredDistance < b.squaredDistance) return -1;
        else return 0;
    }
}
