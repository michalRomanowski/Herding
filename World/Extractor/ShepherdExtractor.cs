using Agent;
using Auxiliary;
using System.Linq;
using MathNet.Spatial.Euclidean;
using System;

namespace World
{
    public static class ShepherdExtractor
    {
        public static double[] ExtractFeatures(IWorld world, ThinkingAgent agent)
        {
            var centerOfGravity =
                world.Sheep.Select(x => x.Position).Center();
            
            var closestAgents = 
                Finder.FindClosestAgents(agent, agent.NumberOfSeenShepherds, world.Shepherds.Members)
                .Concat(Finder.FindClosestAgents(agent, agent.NumberOfSeenSheep, world.Sheep));

            var closestAgentsInRelativeCoordinationSystem = closestAgents
                .Select(x => x.Position.PositionInRelativeCoordinationSystem(agent.Position, centerOfGravity)).ToList();
            
            var features = new double[closestAgentsInRelativeCoordinationSystem.Count * 2 + 2];

            features[0] = TransformPerception(0.0);
            features[1] = TransformPerception(agent.Position.Distance(centerOfGravity));

            if (double.IsNaN(features[1]))
                features[1] = 0.0f;

            for (int i = 0; i < closestAgentsInRelativeCoordinationSystem.Count; i++)
            {
                features[2 + 2 * i] = TransformPerception(closestAgentsInRelativeCoordinationSystem[i].X);
                features[2 + 2 * i + 1] = TransformPerception(closestAgentsInRelativeCoordinationSystem[i].Y);
            }

            return features;
        }

        private static double TransformPerception(double input)
        {
            const double FRACTION = 4000.0;

            if (input == 0.0)
                return FRACTION;

            if (input > 0.0)
                return Math.Min(FRACTION / input, FRACTION);
            else return Math.Max(FRACTION / input, -FRACTION);
        }

        public static void InterpretOutput(ThinkingAgent agent, IWorld world, Vector2D output)
        {
            var centerOfGravity =
                world.Sheep.Select(x => x.Position).Center();

            var vCentered = new Vector2D(
                centerOfGravity.X - agent.Position.X, 
                centerOfGravity.Y - agent.Position.Y);

            var vCenteredLength = vCentered.Length;
            
            var sin = vCentered.Y / vCenteredLength;
            var cos = vCentered.X / vCenteredLength;

            if (double.IsNaN(sin))
                sin = 0.0;

            if (double.IsNaN(cos))
                cos = 1.0;

            var vRotated = new Vector2D (
                output.X * cos - output.Y * sin, 
                output.X * sin + output.Y * cos);

            agent.decision = vRotated;
            agent.Move();
        }
    }
}
