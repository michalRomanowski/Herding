using Agent;
using Auxiliary;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace Simulation
{
    [Serializable]
    public class SimulationParameters
    {
        public float Progress
        {
            get
            {
                lock (progressLocker)
                {
                    return _progress;
                }
            }
            set 
            {
                lock (progressLocker)
                {
                    _progress = value;
                }


                lock (progressLocker)
                {
                    if (value < 0)
                        _progress = 0;
                    else if (value > 1)
                        _progress = 1;
                    else _progress = value;
                }
            }
        }

        [NonSerialized]
        private float _progress;

        [NonSerialized]
        private object progressLocker = new object();
        
        public int OptimizationSteps;

        public int TurnsOfHerding;

        public int NumberOfParticipants;

        public float MutationPower;

        public float AbsoluteMutationFactor;

        public int NumberOfChildren;

        public int NumberOfShepards
        {
            get
            {
                return PositionsOfShepards.Count;
            }
        }

        public int NumberOfSheep
        {
            get
            {
                return PositionsOfSheep.Count;
            }
        }

        public ESheepType SheepType;

        public bool RandomNeuralNets;

        public List<Position> PositionsOfShepards = new List<Position>();

        public List<Position> PositionsOfSheep = new List<Position>();

        public int NumberOfSeenShepards;

        public int NumberOfSeenSheep;

        public int NumberOfHiddenLayers;

        public int NumberOfNeuronsInHiddenLayer;

        public int PopulationSize;

        public bool RandomPositions;

        public int NumberOfRandomSets;

        public int SeedForRandomSheepForBest;

        public RandomSetsList RandomSetsForBest = new RandomSetsList();

        public List<float> BestResultAtStep;

        public EFitnessType FitnessType;

        public IFitnessCounter FitnessCounter
        {
            get { return FitnessCounterProvider.GetFitnessCounter(this); }
        }

        public bool NotIdenticalAgents;

        public SimulationParameters() { }

        public SimulationParameters(bool initWithDefaultValues)
        {
            if (!initWithDefaultValues)
                return;

            this.AbsoluteMutationFactor = 0.05f;
            this.BestResultAtStep = new List<float>();
            this.FitnessType = EFitnessType.Final;
            this.MutationPower = 1.0f;
            this.NotIdenticalAgents = false;
            this.NumberOfChildren = 2;
            this.NumberOfHiddenLayers = 1;
            this.NumberOfNeuronsInHiddenLayer = 10;
            this.NumberOfParticipants = 10;
            this.NumberOfRandomSets = 0;
            this.NumberOfSeenSheep = 2;
            this.NumberOfSeenShepards = 1;
            this.OptimizationSteps = 100;
            this.PopulationSize = 50;
            this.PositionsOfSheep = new List<Position> {
                new Position(0, 200),
                new Position(400, 200)};
            this.PositionsOfShepards = new List<Position> {
                new Position(150, 200),
                new Position(250, 200)};
            this.Progress = float.MinValue;
            this.RandomNeuralNets = false;
            this.RandomPositions = false;
            this.SeedForRandomSheepForBest = 0;
            this.RandomSetsForBest = new RandomSetsList();
            this.SheepType = ESheepType.Passive;
            this.TurnsOfHerding = 300;
        }

        public static SimulationParameters Decompress(string compressed)
        {
            return new XmlSerializer(typeof(SimulationParameters)).Deserialize(new StringReader(compressed)) as SimulationParameters;
        }

        public string Compress()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
        }
    }
}
