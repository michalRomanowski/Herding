using Agent;
using Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace World
{
    public static class Finder
    {
        public static IEnumerable<IMovingAgent> FindAgentsAtRange(IList<IMovingAgent> agents, Position center, float range)
        {
            float squaredRange = range * range;

            return agents.Where(x => Position.SquaredDistance(center, x.Position) <= squaredRange);
        }

        public static IEnumerable<IMovingAgent> FindClosestAgents(IList<IMovingAgent> agents, IMovingAgent center, int numberOfObjectsToFind)
        {
            var distanceComparer = new MovingAgentAndDistanceComparer();

            var closestObjects =
                agents
                    .Where(x => x != center)
                    .Select(x => new MovingAgentAndDistance(x, Position.SquaredDistance(center.Position, x.Position)))
                    .ToList();

            closestObjects.Sort(distanceComparer);

            var timesToCopyFirstAgent = numberOfObjectsToFind - closestObjects.Count;

            List<IMovingAgent> closestAgents = new List<IMovingAgent>();

            for (int i = 0; i < timesToCopyFirstAgent; i++)
            {
                closestAgents.Add(closestObjects.First().movingAgent);
            }

            closestAgents.AddRange(closestObjects.Take(numberOfObjectsToFind).Select(x => x.movingAgent));

            return closestAgents;
        }
    }
}

public class MovingAgentAndDistance
{
    public IMovingAgent movingAgent;
    public float squaredDistance;

    public MovingAgentAndDistance(IMovingAgent movingAgent, float squaredDistance)
    {
        this.movingAgent = movingAgent;
        this.squaredDistance = squaredDistance;
    }
}

public class MovingAgentAndDistanceComparer : IComparer<MovingAgentAndDistance>
{
    public int Compare(MovingAgentAndDistance a, MovingAgentAndDistance b)
    {
        if (a.squaredDistance > b.squaredDistance) return 1;
        else if (a.squaredDistance < b.squaredDistance) return -1;
        else return 0;
    }
}
