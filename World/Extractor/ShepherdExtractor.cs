using Agent;
using Auxiliary;
using System;
using System.Linq;
using System.Numerics;

namespace World
{
    public static class ShepherdExtractor
    {
        private const float SPEED = 2.0f;

        public static float[] ExtractFeatures(IWorld world, ThinkingAgent agent)
        {
            var centerOfGravity =
                world.Sheep.Select(x => x.Position).Center();
            
            var closestAgents = Finder.FindClosestAgents(agent, agent.NumberOfSeenShepherds, world.Shepherds.Members.Cast<IMovingAgent>());
            var closestSheep = Finder.FindClosestAgents(agent, agent.NumberOfSeenSheep, world.Sheep.Cast<IMovingAgent>());

            var closestAgentsInRelativeCoordinationSystem = closestAgents.Select(x => x.Position).PositionsInRelativeCoordinationSystem(agent.Position, centerOfGravity);
            var closestSheepInRelativeCoordinationSystem = closestSheep.Select(x => x.Position).PositionsInRelativeCoordinationSystem(agent.Position, centerOfGravity);

            closestAgentsInRelativeCoordinationSystem.AddRange(closestSheepInRelativeCoordinationSystem);

            float[] features = new float[closestAgentsInRelativeCoordinationSystem.Count * 2 + 2];

            features[0] = 0.0f;
            features[1] = Position.Distance(agent.Position, centerOfGravity);

            if (float.IsNaN(features[1]))
                features[1] = 0.0f;

            for (int i = 0; i < closestAgentsInRelativeCoordinationSystem.Count; i++)
            {
                features[2 + 2 * i] = closestAgentsInRelativeCoordinationSystem[i].X;
                features[2 + 2 * i + 1] = closestAgentsInRelativeCoordinationSystem[i].Y;
            }
            
            return features;
        }

        public static void InterpretOutput(ThinkingAgent agent, IWorld world, float[] output)
        {
            if (output.Count() != 2)
                throw new ArgumentException();

            var centerOfGravity =
                world.Sheep.Select(x => x.Position).Center();

            var vCentered = new Vector2(
                centerOfGravity.X - agent.Position.X, 
                centerOfGravity.Y - agent.Position.Y);

            var vCenteredLength = vCentered.Length();
            
            float sin = vCentered.Y / vCenteredLength;
            float cos = vCentered.X / vCenteredLength;

            if (float.IsNaN(sin))
                sin = 0.0f;

            if (float.IsNaN(cos))
                cos = 1.0f;

            var vRotated = new Vector2(
                output[0] * cos - output[1] * sin,
                output[0] * sin + output[1] * cos);

            float vRotatedLength = vRotated.Length();

            if (vRotatedLength > 1)
            {
                vRotated.X /= vRotatedLength;
                vRotated.Y /= vRotatedLength;
            }

            agent.Position.X += vRotated.X * SPEED;
            agent.Position.Y += vRotated.Y * SPEED;
        }
    }
}
