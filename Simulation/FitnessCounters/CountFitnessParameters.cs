using System.Collections.Generic;
using Agent;
using MathNet.Spatial.Euclidean;

namespace Simulations
{
    public class CountFitnessParameters
    {
        public EFitnessType FitnessType;
        public int TurnsOfHerding;
        public List<List<Vector2D>> PositionsOfShepherdsSet;
        public List<List<Vector2D>> PositionsOfSheepSet;
        public ESheepType SheepType;
        public int Seed;
    }
}
