﻿using Agent;
using Auxiliary;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simulations
{
    [Serializable]
    public class SimulationParameters
    {
        public int Id { get; set; }

        [NotMapped]
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
        [NotMapped]
        private float _progress;

        [NonSerialized]
        [NotMapped]
        private object progressLocker = new object();

        public int OptimizationSteps { get; set; }

        public int TurnsOfHerding { get; set; }

        public int NumberOfParticipants { get; set; }

        public float MutationPower { get; set; }

        public float AbsoluteMutationFactor { get; set; }

        [NotMapped]
        public int NumberOfShepherds
        {
            get
            {
                return PositionsOfShepherds.Count;
            }
        }

        [NotMapped]
        public int NumberOfSheep
        {
            get
            {
                return PositionsOfSheep.Count;
            }
        }

        public ESheepType SheepType { get; set; }
        
        public string CompressedPositionsOfShepherds
        {
            get
            {
                return Compressor.Compress(PositionsOfShepherds);
            }
            set
            {
                PositionsOfShepherds = Compressor.DecompressList<Position>(value);
            }
        }

        public string CompressedPositionsOfSheep
        {
            get
            {
                return Compressor.Compress(PositionsOfSheep);
            }
            set
            {
                PositionsOfSheep = Compressor.DecompressList<Position>(value);
            }
        }

        [NotMapped]
        public IList<Position> PositionsOfShepherds { get; set; }

        [NotMapped]
        public IList<Position> PositionsOfSheep { get; set; }

        public int NumberOfSeenShepherds { get; set; }

        public int NumberOfSeenSheep { get; set; }

        public int NumberOfHiddenLayers { get; set; }

        public int NumberOfNeuronsInHiddenLayer { get; set; }

        public int PopulationSize { get; set; }

        public bool RandomPositions { get; set; }

        public int NumberOfRandomSets { get; set; }

        public int SeedForRandomSheepForBest { get; set; }
        
        public RandomSetsList RandomSetsForBest { get; set; }

        public List<float> BestResultAtStep { get; set; }

        public EFitnessType FitnessType { get; set; }

        [NotMapped]
        public IFitnessCounter FitnessCounter
        {
            get { return FitnessCounterProvider.GetFitnessCounter(this); }
        }

        public bool NotIdenticalAgents;

        public SimulationParameters()
        {
            PositionsOfShepherds = new List<Position>();
            PositionsOfSheep = new List<Position>();

            RandomSetsForBest = new RandomSetsList();
        }
    }
}
