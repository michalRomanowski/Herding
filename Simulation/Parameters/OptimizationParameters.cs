using Agent;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using MathNet.Spatial.Euclidean;

namespace Simulations.Parameters
{
    [Serializable]
    public class OptimizationParameters : IPopulationParameters
    {
        public int CurrentEra { get; set; }
        public int NumberOfEras { get; set; }
        public int TurnsOfHerding { get; set; }
        public double TargetFitness { get; set; }
        public int NumberOfParticipants { get; set; }
        public double MutationPower { get; set; }
        public ESheepType SheepType { get; set; }
        public EPerceptionType PerceptionType { get; set; }
        public int NumberOfSeenShepherds { get; set; }
        public int NumberOfSeenSheep { get; set; }
        public int NumberOfHiddenLayers { get; set; }
        public int NumberOfNeuronsInHiddenLayer { get; set; }
        public int PopulationSize { get; set; }
        public EFitnessType FitnessType { get; set; }
        public bool NotIdenticalAgents { get; set; }
        public bool RandomPositions { get; set; }
        public int NumberOfRandomSets { get; set; }
        public int SeedForRandomSheepForBest { get; set; }
        public bool RandomizeNeuralNetOnInit { get; set; }
        [XmlElement]
        public RandomSetsList RandomSetsForBest { get; set; }

        [XmlIgnore]
        public int NumberOfShepherds
        {
            get
            {
                return PositionsOfShepherds.Count;
            }
        }
        [XmlIgnore]
        public int NumberOfSheep
        {
            get
            {
                return PositionsOfSheep.Count;
            }
        }

        [XmlArray]
        public List<Vector2D> PositionsOfShepherds { get; set; }
        [XmlArray]
        public List<Vector2D> PositionsOfSheep { get; set; }

        public OptimizationParameters()
        {
            PositionsOfShepherds = new List<Vector2D>();
            PositionsOfSheep = new List<Vector2D>();
            RandomSetsForBest = new RandomSetsList();
        }
    }
}