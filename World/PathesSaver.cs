﻿using Agent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace World
{
    public static class PathesSaver
    {
        public static void SavePathes(string path, IList<IThinkingAgent> shepards, IList<ISheep> sheep)
        {
            Directory.CreateDirectory(path);

            for (int i = 0; i < shepards.Count; i++)
            {
                PathesSaver.Save(path, "Agent_" + i.ToString() + "_X.txt", shepards[i].Path.Select(x => x.X).ToList());
                PathesSaver.Save(path, "Agent_" + i.ToString() + "_Y.txt", shepards[i].Path.Select(x => x.Y).ToList());
            }

            for (int i = 0; i < shepards.Count; i++)
            {
                PathesSaver.Save(path, "Sheep_" + i.ToString() + "_X.txt", sheep[i].Path.Select(x => x.X).ToList());
                PathesSaver.Save(path, "Sheep_" + i.ToString() + "_Y.txt", sheep[i].Path.Select(x => x.Y).ToList());
            }
        }

        private static void Save(string path, string filename, IList<float> positions)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(path, filename)))
            {
                foreach (float pos in positions)
                {
                    sw.WriteLine(pos.ToString());
                }
            }
        }
    }
}
