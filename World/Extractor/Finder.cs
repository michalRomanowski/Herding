using Agent;
using Auxiliary;
using System.Collections.Generic;
using System.Linq;
using MathNet.Spatial.Euclidean;

namespace World
{
    public static class Finder
    {
        public static IEnumerable<IHavePosition> FindAgentsAtRange(Vector2D center, double range, IEnumerable<IHavePosition> agents)
        {
            var squaredRange = range * range;

            return agents.Where(x => center.SquaredDistance(x.Position) <= squaredRange);
        }

        public static IEnumerable<IHavePosition> FindClosestAgents(IHavePosition center, int numberOfAgentsToFind, IEnumerable<IHavePosition> agents)
        {
            var closestObjects = agents
                .Where(x => x != center)
                .Select(x => new { Agent = x, SquareDistance = center.Position.SquaredDistance(x.Position)})
                .ToList();
            
            closestObjects.Sort((a, b) => {
                return a.SquareDistance > b.SquareDistance ? 1 : -1;});
            
            List<IHavePosition> closestAgents = new List<IHavePosition>();
            
            var timesToCopyFirstAgent = numberOfAgentsToFind - closestObjects.Count();
            
            for (int i = 0; i < timesToCopyFirstAgent; i++)
                closestAgents.Add(closestObjects.First().Agent);

            closestAgents.AddRange(closestObjects.Take(numberOfAgentsToFind).Select(x => x.Agent));

            return closestAgents;
        }
    }
}
