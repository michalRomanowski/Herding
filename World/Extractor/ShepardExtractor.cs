using Agent;
using Auxiliary;
using System;
using System.Collections;
using System.Linq;

namespace World
{
    public static class ShepardExtractor
    {
        private const float SPEED = 2.0f;

        public static float[] ExtractFeatures(IWorld world, ThinkingAgent agent)
        {
            var centerOfGravity =
                world.Sheep.Select(x => x.Position).CentreOfGravity();
            
            var closestAgents = Finder.FindClosestAgents(world.Shepards.Members.Cast<IMovingAgent>().ToList(), agent, agent.NumberOfSeenShepards);
            var closestSheep = Finder.FindClosestAgents(world.Sheep.Cast<IMovingAgent>().ToList(), agent, agent.NumberOfSeenSheep);

            var closestAgentsInRelativeCoordinationSystem = Position.PositionsInRelativeCoordinationSystem(agent.Position, centerOfGravity, closestAgents.Select(x => x.Position));
            var closestSheepInRelativeCoordinationSystem = Position.PositionsInRelativeCoordinationSystem(agent.Position, centerOfGravity, closestSheep.Select(x => x.Position));

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
                throw new ArgumentException("Net output must be of size == 2.");

            var centerOfGravity =
                world.Sheep.Select(x => x.Position).CentreOfGravity();

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

            if (mX * mX + mY * mY > 1)
                CMath.ToVectorLengthOne(ref mX, ref mY);

            agent.Position.X += mX * SPEED;
            agent.Position.Y += mY * SPEED;
        }
    }
}
