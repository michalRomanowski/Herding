using Agent;

namespace World
{
    public static class SheepExtractor
    {
        public static double[] ExtractFeatures(IWorld world, IMovingAgent sheep)
        {
            var features = new double[world.Shepherds.Members.Count * 2];

            for(int i = 0; i < world.Shepherds.Members.Count; i++)
            {
                features[2 * i] = world.Shepherds.Members[i].Position.X - sheep.Position.X;
                features[2 * i + 1] = world.Shepherds.Members[i].Position.Y - sheep.Position.Y;
            }

            return features;
        }
    }
}
