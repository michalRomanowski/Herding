using Agent;
using MathNet.Spatial.Euclidean;
using System.Collections.Generic;

namespace Simulations.Parameters
{
    interface IHerdingParameters
    {
        int TurnsOfHerding { get; set; }
        ESheepType SheepType { get; set; }
        List<Vector2D> PositionsOfShepherds { get; set; }
        List<Vector2D> PositionsOfSheep { get; set; }
    }
}
