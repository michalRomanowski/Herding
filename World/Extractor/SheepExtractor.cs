using Agent;
using System;

namespace World
{
    public static class SheepExtractor
    {
        private const float SPEED = 1.0f;

        public static float[] ExtractFeatures(IWorld world, IMovingAgent sheep)
        {
            var features = new float[world.Shepherds.Members.Count * 2];

            for(int i = 0; i < world.Shepherds.Members.Count; i++)
            {
                features[2 * i] = world.Shepherds.Members[i].Position.X - sheep.Position.X;
                features[2 * i + 1] = world.Shepherds.Members[i].Position.Y - sheep.Position.Y;
            }

            return features;
        }

        /// <summary>
        /// Returns how agent should move.
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="world"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static void InterpretOutput(IMovingAgent sheep, IWorld world, float[] output)
        {
            if (output.Length != 2)
                throw new ArgumentException();

            var mX = float.IsNaN(output[0]) ? 0 : output[0];
            var mY = float.IsNaN(output[1]) ? 0 : output[1];

            sheep.Position.X += mX * SPEED;
            sheep.Position.Y += mY * SPEED;
        }
    }
}
