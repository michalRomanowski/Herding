using Agent;
using Auxiliary;
using Simulations;
using System.Collections.Generic;

namespace UnitTests
{
    static class TestObjects
    {
        public static OptimizationParameters GetOptimizationParameters1()
        {
            return new OptimizationParameters()
            {
                FitnessType = EFitnessType.Final,
                MutationPower = 0.05,
                NotIdenticalAgents = false,
                NumberOfHiddenLayers = 1,
                NumberOfNeuronsInHiddenLayer = 8,
                NumberOfParticipants = 10,
                NumberOfRandomSets = 0,
                NumberOfSeenSheep = 2,
                NumberOfSeenShepherds = 1,
                NumberOfEras = 10,
                PopulationSize = 10,
                PositionsOfSheep = new List<Position>()
                {
                    new Position(0, 200),
                    new Position(400, 200)
                },
                PositionsOfShepherds = new List<Position>()
                {
                    new Position(150, 200),
                    new Position(250, 200)
                },
                RandomPositions = false,
                RandomSetsForBest = new RandomSetsList(),
                SeedForRandomSheepForBest = 0,
                SheepType = ESheepType.Passive,
                TurnsOfHerding = 400
            };
        }

        public static OptimizationParameters GetOptimizationParameters2()
        {
            return new OptimizationParameters()
            {
                FitnessType = EFitnessType.Sum,
                MutationPower = 0.1,
                NotIdenticalAgents = true,
                NumberOfHiddenLayers = 2,
                NumberOfNeuronsInHiddenLayer = 7,
                NumberOfParticipants = 8,
                NumberOfRandomSets = 0,
                NumberOfSeenSheep = 1,
                NumberOfSeenShepherds = 2,
                NumberOfEras = 50,
                PopulationSize = 50,
                PositionsOfSheep = new List<Position>()
                {
                    new Position(1, 2),
                    new Position(3, 4)
                },
                PositionsOfShepherds = new List<Position>()
                {
                    new Position(5, 6),
                    new Position(7, 8)
                },
                RandomPositions = false,
                RandomSetsForBest = new RandomSetsList(),
                SeedForRandomSheepForBest = 909,
                SheepType = ESheepType.Wandering,
                TurnsOfHerding = 100
            };
        }

        public static OptimizationParameters GetOptimizationParametersWithRandomSets()
        {
            return new OptimizationParameters()
            {
                FitnessType = EFitnessType.Final,
                MutationPower = 0.05,
                NotIdenticalAgents = false,
                NumberOfHiddenLayers = 1,
                NumberOfNeuronsInHiddenLayer = 8,
                NumberOfParticipants = 10,
                NumberOfRandomSets = 2,
                NumberOfSeenSheep = 2,
                NumberOfSeenShepherds = 1,
                NumberOfEras = 10,
                PopulationSize = 100,
                PositionsOfSheep = new List<Position>()
                {
                    new Position(0, 200),
                    new Position(400, 200)
                },
                PositionsOfShepherds = new List<Position>()
                {
                    new Position(150, 200),
                    new Position(250, 200)
                },
                RandomPositions = true,
                RandomSetsForBest = new RandomSetsList(3, 4, 5, 123),
                SeedForRandomSheepForBest = 123,
                SheepType = ESheepType.Passive,
                TurnsOfHerding = 400
            };
        }
    }
}
