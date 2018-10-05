using Agent;
using Auxiliary;
using System.Collections.Generic;
using System.Linq;

namespace World
{
    public static class Finder
    {
        public static IEnumerable<IHasPosition> FindAgentsAtRange(Position center, float range, IEnumerable<IHasPosition> agents)
        {
            float squaredRange = range * range;

            return agents.Where(x => Position.SquaredDistance(center, x.Position) <= squaredRange);
        }

        public static IEnumerable<IHasPosition> FindClosestAgents(IHasPosition center, int numberOfAgentsToFind, IEnumerable<IHasPosition> agents)
        {
            var closestObjects = agents
                .Where(x => x != center)
                .Select(x => new { Agent = x, SquareDistance = Position.SquaredDistance(center.Position, x.Position)})
                .ToList();
            
            closestObjects.Sort((a, b) => {
                return a.SquareDistance > b.SquareDistance ? 1 : -1;});
            
            List<IHasPosition> closestAgents = new List<IHasPosition>();
            
            var timesToCopyFirstAgent = numberOfAgentsToFind - closestObjects.Count();
            
            for (int i = 0; i < timesToCopyFirstAgent; i++)
                closestAgents.Add(closestObjects.First().Agent);

            closestAgents.AddRange(closestObjects.Take(numberOfAgentsToFind).Select(x => x.Agent));

            return closestAgents;
        }
    }
}
