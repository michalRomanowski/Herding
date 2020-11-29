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
        [XmlAttribute]
        public int CurrentEra { get; set; }
        [XmlAttribute]
        public int NumberOfEras { get; set; }
        [XmlAttribute]
        public int TurnsOfHerding { get; set; }
        [XmlAttribute]
        public double TargetFitness { get; set; }
        [XmlAttribute]
        public int NumberOfParticipants { get; set; }
        [XmlAttribute]
        public double MutationPower { get; set; }
        [XmlAttribute]
        public ESheepType SheepType { get; set; }
        [XmlAttribute]
        public EPerceptionType PerceptionType { get; set; }
        [XmlAttribute]
        public int NumberOfSeenShepherds { get; set; }
        [XmlAttribute]
        public int NumberOfSeenSheep { get; set; }
        [XmlAttribute]
        public int NumberOfHiddenLayers { get; set; }
        [XmlAttribute]
        public int NumberOfNeuronsInHiddenLayer { get; set; }
        [XmlAttribute]
        public int PopulationSize { get; set; }
        [XmlAttribute]
        public EFitnessType FitnessType { get; set; }
        [XmlAttribute]
        public bool NotIdenticalAgents { get; set; }

        [XmlAttribute]
        public bool RandomPositions { get; set; }
        [XmlAttribute]
        public int NumberOfRandomSets { get; set; }
        [XmlAttribute]
        public int SeedForRandomSheepForBest { get; set; }
        [XmlAttribute]
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