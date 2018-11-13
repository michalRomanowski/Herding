using Agent;
using Auxiliary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Teams;

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

        public EFitnessType FitnessType { get; set; }

        public bool NotIdenticalAgents;

        public SimulationParameters()
        {
            PositionsOfShepherds = new List<Position>();
            PositionsOfSheep = new List<Position>();

            RandomSetsForBest = new RandomSetsList();
        }

        public PopulationParameters GetPopulationParameters()
        {
            return new PopulationParameters()
            {
                PopulationSize = this.PopulationSize,
                TeamParameters = GetTeamParameters()
            };
        }

        public TeamParameters GetTeamParameters()
        {
            return new TeamParameters()
            {
                NotIdenticalAgents = this.NotIdenticalAgents,
                NumberOfShepherds = this.NumberOfShepherds,
                ShepherdParameters = GetShepherdParameters()
            };
        }

        public ShepherdParameters GetShepherdParameters()
        {
            return new ShepherdParameters()
            {
                HiddenLayerSize = this.NumberOfNeuronsInHiddenLayer,
                NumberOfHiddenLayers = this.NumberOfHiddenLayers,
                NumberOfSeenSheep = this.NumberOfSeenSheep,
                NumberOfSeenShepherds = this.NumberOfSeenShepherds
            };
        }

        public CountFitnessParameters GetCountFitnessParameters()
        {
            return new CountFitnessParameters()
            {
                PositionsOfSheepSet = this.RandomPositions ? this.RandomSetsForBest.PositionsOfSheepSet : new List<IList<Position>>() { this.PositionsOfSheep },
                PositionsOfShepherdsSet = this.RandomPositions ? this.RandomSetsForBest.PositionsOfShepherdsSet : new List<IList<Position>>() { this.PositionsOfShepherds },
                Seed = this.SeedForRandomSheepForBest,
                SheepType = this.SheepType,
                TurnsOfHerding = this.TurnsOfHerding
            };
        }

        public BestTeamSelectorParameters GetBestTeamSelectorParameters()
        {
            return new BestTeamSelectorParameters()
            {
                RandomPositions = this.RandomPositions,
                FitnessType = this.FitnessType,
                CountFitnessParameters = GetCountFitnessParameters()
            };
        }
    }
}
