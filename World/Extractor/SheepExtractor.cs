using Agent;
using System;

namespace World
{
    public static class SheepExtractor
    {
        private const float SPEED = 1.0f;

        public static float[] ExtractFeatures(IWorld world, ISheep sheep)
        {
            var features = new float[world.Shepards.Members.Count * 2];

            for(int i = 0; i < world.Shepards.Members.Count; i++)
            {
                features[2 * i] = world.Shepards.Members[i].Position.X - sheep.Position.X;
                features[2 * i + 1] = world.Shepards.Members[i].Position.Y - sheep.Position.Y;
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
        public static void InterpretOutput(ISheep sheep, IWorld world, float[] output)
        {
            if (output.Length != 2)
                throw new ArgumentException();

            var mX = output[0];
            var mY = output[1];

            sheep.Position.X += mX * SPEED;
            sheep.Position.Y += mY * SPEED;
        }
    }
}
