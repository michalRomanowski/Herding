using System;
using System.Collections.Generic;

namespace Agent
{
    static class PerceptionFactory
    {
        private readonly static Dictionary<EPerceptionType, Func<IPerception>> perceptionMap = 
            new Dictionary<EPerceptionType, Func<IPerception>>
        {
            { EPerceptionType.Simple, () => new SimplePerception() },
            { EPerceptionType.Reciprocal, () => new ReciprocalPerception() }
        };

        public static IPerception GetPerception(EPerceptionType perceptionType)
        {
            return perceptionMap[perceptionType].Invoke();
        }
    }
}
