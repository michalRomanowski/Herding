using System.Collections.Generic;
using MathNet.Spatial.Euclidean;
using Agent;

namespace Simulations
{
    public class CountFitnessParameters
    {
        public EFitnessType FitnessType { get; set; }
        public int TurnsOfHerding { get; set; }
        public List<List<Vector2D>> PositionsOfShepherdsSet { get; set; }
        public List<List<Vector2D>> PositionsOfSheepSet { get; set; }
        public ESheepType SheepType { get; set; }
    }
}
