using Agent;
using Auxiliary;
using System.Collections.Generic;
using System.Linq;

namespace World
{
    public static class Finder
    {
        public static IEnumerable<IHavePosition> FindAgentsAtRange(Position center, float range, IEnumerable<IHavePosition> agents)
        {
            float squaredRange = range * range;

            return agents.Where(x => Position.SquaredDistance(center, x.Position) <= squaredRange);
        }

        public static IEnumerable<IHavePosition> FindClosestAgents(IHavePosition center, int numberOfAgentsToFind, IEnumerable<IHavePosition> agents)
        {
            var closestObjects = agents
                .Where(x => x != center)
                .Select(x => new { Agent = x, SquareDistance = Position.SquaredDistance(center.Position, x.Position)})
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
