using System;

namespace Auxiliary
{
    public static class StaticRandom
    {
        public static Random R { get; }

        static StaticRandom()
        {
            R = new Random();
        }
    }
}