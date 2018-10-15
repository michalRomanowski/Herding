using Agent;
using Auxiliary;
using System;
using System.Linq;

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

            float dX = centerOfGravity.X - agent.Position.X;
            float dY = centerOfGravity.Y - agent.Position.Y;
            float d = (float)Math.Sqrt(dX * dX + dY * dY);

            float sin = dY / d;
            float cos = dX / d;

            if (float.IsNaN(sin))
                sin = 0.0f;

            if (float.IsNaN(cos))
                cos = 1.0f;

            float mX = output[0] * cos - output[1] * sin;
            float mY = output[0] * sin + output[1] * cos;

            float lengthM = (float)Math.Sqrt(mX * mX + mY * mY);

            if (lengthM > 1)
            {
                mX /= lengthM;
                mY /= lengthM;
            }
            
            agent.Position.X += mX * SPEED;
            agent.Position.Y += mY * SPEED;
        }
    }
}
