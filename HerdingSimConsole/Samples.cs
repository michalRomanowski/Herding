using Agent;
using MathNet.Spatial.Euclidean;
using Simulations;
using System;
using System.Collections.Generic;

namespace HerdingSimConsole
{
    static class Samples
    {
        public static readonly IDictionary<string, OptimizationParameters> OptimizationParametersSamples = new Dictionary<string, OptimizationParameters>
        {
            {
                "fixedPositions",
                new OptimizationParameters()
                {
                    FitnessType = EFitnessType.Final,
                    MutationPower = 0.02,
                    NotIdenticalAgents = false,
                    NumberOfHiddenLayers = 1,
                    NumberOfNeuronsInHiddenLayer = 10,
                    NumberOfParticipants = 10,
                    NumberOfRandomSets = 0,
                    NumberOfSeenSheep = 2,
                    NumberOfSeenShepherds = 1,
                    NumberOfEras = int.MaxValue,
                    TargetFitness = 1.0,
                    PopulationSize = 100,
                    PositionsOfSheep = new List<Vector2D>()
                    {
                        new Vector2D(0, 200),
                        new Vector2D(400, 200)
                    },
                    PositionsOfShepherds = new List<Vector2D>()
                    {
                        new Vector2D(150, 200),
                        new Vector2D(250, 200)
                    },
                    RandomPositions = false,
                    RandomSetsForBest = new RandomSetsList(),
                    SeedForRandomSheepForBest = 0,
                    SheepType = ESheepType.Passive,
                    TurnsOfHerding = 400
                }
            },
            {
                "randomPositions",
                new OptimizationParameters()
                {
                    FitnessType = EFitnessType.Final,
                    MutationPower = 0.02,
                    NotIdenticalAgents = false,
                    NumberOfHiddenLayers = 1,
                    NumberOfNeuronsInHiddenLayer = 10,
                    NumberOfParticipants = 10,
                    NumberOfRandomSets = 5,
                    NumberOfSeenSheep = 2,
                    NumberOfSeenShepherds = 1,
                    NumberOfEras = int.MaxValue,
                    TargetFitness = 1.0,
                    PopulationSize = 100,
                    PositionsOfSheep = new List<Vector2D>()
                    {
                        new Vector2D(0, 200),
                        new Vector2D(400, 200)
                    },
                    PositionsOfShepherds = new List<Vector2D>()
                    {
                        new Vector2D(150, 200),
                        new Vector2D(250, 200)
                    },
                    RandomPositions = true,
                    RandomSetsForBest = new RandomSetsList(5, 2, 2, (int)DateTime.Now.Ticks),
                    SeedForRandomSheepForBest = (int)DateTime.Now.Ticks,
                    SheepType = ESheepType.Passive,
                    TurnsOfHerding = 400
                }
            },
            {
                "symmetric",
                new OptimizationParameters()
                {
                    NumberOfEras = int.MaxValue,
                    TurnsOfHerding = 1000,
                    TargetFitness = 0,
                    NumberOfParticipants = 10,
                    MutationPower = 0.01,
                    SheepType = ESheepType.Passive,
                    NumberOfSeenShepherds = 1,
                    NumberOfSeenSheep = 2,
                    NumberOfHiddenLayers = 1,
                    NumberOfNeuronsInHiddenLayer = 10,
                    PopulationSize = 100,
                    FitnessType = EFitnessType.Final,
                    NotIdenticalAgents = false,
                    RandomPositions = false,
                    NumberOfRandomSets = 0,
                    SeedForRandomSheepForBest = 0,
                    RandomSetsForBest = new RandomSetsList(),
                    PositionsOfShepherds = new List<Vector2D>()
                    {
                        new Vector2D(200, 150),
                        new Vector2D(200, 250)
                    },
                    PositionsOfSheep = new List<Vector2D>()
                    {
                        new Vector2D(150, 200),
                        new Vector2D(250, 200),
                        new Vector2D(0, 200),
                        new Vector2D(400, 200)
                    }
                }
            },
            {
                "2v4PartialKnowledge",
                new OptimizationParameters()
                {
                    NumberOfEras = int.MaxValue,
                    TurnsOfHerding = 1000,
                    TargetFitness = 0,
                    NumberOfParticipants= 10,
                    MutationPower = 0.01,
                    SheepType = ESheepType.Passive,
                    NumberOfSeenShepherds = 1,
                    NumberOfSeenSheep = 2,
                    NumberOfHiddenLayers = 1,
                    NumberOfNeuronsInHiddenLayer = 10,
                    PopulationSize = 100,
                    FitnessType = EFitnessType.Final,
                    NotIdenticalAgents = false,
                    RandomPositions = true,
                    NumberOfRandomSets = 10,
                    SeedForRandomSheepForBest = 0,
                    RandomSetsForBest = new RandomSetsList(10, 2, 4, (int)DateTime.Now.Ticks),
                    PositionsOfShepherds = new List<Vector2D>()
                    {
                        new Vector2D(200, 0),
                        new Vector2D(200, 400)
                    },
                    PositionsOfSheep = new List<Vector2D>()
                    {
                        new Vector2D(150, 200),
                        new Vector2D(250, 200),
                        new Vector2D(0, 200),
                        new Vector2D(400, 200)
                    }
                }
            }
        };
    }
}
