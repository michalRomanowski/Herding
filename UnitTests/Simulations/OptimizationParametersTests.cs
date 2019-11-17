using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Agent;
using Auxiliary;
using Simulations;

namespace UnitTests.Simulations
{
    [TestClass]
    public class OptimizationParametersTests
    {
        [TestMethod]
        public void CreateOptimizationParametersTest()
        {
            OptimizationParameters sp = new OptimizationParameters()
            {
                FitnessType = EFitnessType.Final,
                MutationPower = 0.05f,
                NotIdenticalAgents = false,
                NumberOfHiddenLayers = 1,
                NumberOfNeuronsInHiddenLayer = 30,
                NumberOfParticipants = 10,
                NumberOfRandomSets = 5,
                NumberOfSeenSheep = 10,
                NumberOfSeenShepherds = 2,
                NumberOfEras = 5000,
                PopulationSize = 100,
                PositionsOfSheep = new List<Position>()
                {
                    new Position(0, 0),
                    new Position(1, 1),
                    new Position(2, 2),
                    new Position(3, 3),
                    new Position(4, 4),
                    new Position(5, 5),
                    new Position(6, 6),
                    new Position(7, 7),
                    new Position(8, 8),
                    new Position(9, 9)
                },
                PositionsOfShepherds = new List<Position>()
                {
                    new Position(10, 10),
                    new Position(20, 20),
                    new Position(30, 30)
                },
                RandomPositions = false,
                RandomSetsForBest = null,
                SeedForRandomSheepForBest = 0,
                SheepType = ESheepType.Passive,
                TurnsOfHerding = 3000
            };
        }
    }
}
