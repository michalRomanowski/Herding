using Agent;
using MathNet.Spatial.Euclidean;
using Simulations;
using Simulations.Parameters;
using System;
using System.Collections.Generic;

namespace HerdingSimConsole
{
    static class Samples
    {
        public static readonly IDictionary<string, OptimizationParameters> OptimizationParametersSamples = new Dictionary<string, OptimizationParameters>
        {
            //{
            //    "fixedPositions",
            //    new OptimizationParameters()
            //    {
            //        FitnessType = EFitnessType.Final,
            //        MutationPower = 0.02,
            //        NotIdenticalAgents = false,
            //        NumberOfHiddenLayers = 1,
            //        NumberOfNeuronsInHiddenLayer = 26,
            //        NumberOfParticipants = 10,
            //        NumberOfRandomSets = 0,
            //        NumberOfSeenSheep = 10,
            //        NumberOfSeenShepherds = 2,
            //        NumberOfEras = int.MaxValue,
            //        TargetFitness = 0.01,
            //        PopulationSize = 250,
            //        PositionsOfSheep = new List<Vector2D>()
            //        {
            //            new Vector2D(0, 150),
            //            new Vector2D(0, 200),
            //            new Vector2D(0, 250),
            //            new Vector2D(150, 0),
            //            new Vector2D(150, 400),
            //            new Vector2D(250, 0),
            //            new Vector2D(250, 400),
            //            new Vector2D(400, 150),
            //            new Vector2D(400, 200),
            //            new Vector2D(400, 250)
            //        },
            //        PositionsOfShepherds = new List<Vector2D>()
            //        {
            //            new Vector2D(100, 300),
            //            new Vector2D(200, 100),
            //            new Vector2D(300, 300)
            //        },
            //        RandomPositions = false,
            //        RandomSetsForBest = new RandomSetsList(),
            //        SeedForRandomSheepForBest = (int)DateTime.Now.Ticks,
            //        SheepType = ESheepType.Passive,
            //        TurnsOfHerding = 2000
            //    }
            //},
            //{
            //    "randomPositions",
            //    new OptimizationParameters()
            //    {
            //        FitnessType = EFitnessType.Final,
            //        MutationPower = 0.02,
            //        NotIdenticalAgents = false,
            //        NumberOfHiddenLayers = 1,
            //        NumberOfNeuronsInHiddenLayer = 30,
            //        NumberOfParticipants = 10,
            //        NumberOfRandomSets = 10,
            //        NumberOfSeenSheep = 10,
            //        NumberOfSeenShepherds = 2,
            //        NumberOfEras = int.MaxValue,
            //        TargetFitness = 0.0,
            //        PopulationSize = 250,
            //        PositionsOfSheep = new List<Vector2D>()
            //        {
            //            new Vector2D(0, 150),
            //            new Vector2D(0, 200),
            //            new Vector2D(0, 250),
            //            new Vector2D(150, 0),
            //            new Vector2D(150, 400),
            //            new Vector2D(250, 0),
            //            new Vector2D(250, 400),
            //            new Vector2D(400, 150),
            //            new Vector2D(400, 200),
            //            new Vector2D(400, 250)
            //        },
            //        PositionsOfShepherds = new List<Vector2D>()
            //        {
            //            new Vector2D(100, 300),
            //            new Vector2D(200, 100),
            //            new Vector2D(300, 300)
            //        },
            //        RandomPositions = true,
            //        RandomSetsForBest = new RandomSetsList(10, 3, 10, (int)DateTime.Now.Ticks),
            //        SeedForRandomSheepForBest = (int)DateTime.Now.Ticks,
            //        SheepType = ESheepType.Passive,
            //        TurnsOfHerding = 2000
            //    }
            //},
            //{
            //    "randomPositions3v2",
            //    new OptimizationParameters()
            //    {
            //        FitnessType = EFitnessType.Final,
            //        MutationPower = 0.02,
            //        NotIdenticalAgents = false,
            //        NumberOfHiddenLayers = 1,
            //        NumberOfNeuronsInHiddenLayer = 12,
            //        NumberOfParticipants = 10,
            //        NumberOfRandomSets = 10,
            //        NumberOfSeenSheep = 2,
            //        NumberOfSeenShepherds = 2,
            //        NumberOfEras = int.MaxValue,
            //        TargetFitness = 0.0,
            //        PopulationSize = 250,
            //        PositionsOfSheep = new List<Vector2D>()
            //        {
            //            new Vector2D(0, 150),
            //            new Vector2D(0, 200),
            //        },
            //        PositionsOfShepherds = new List<Vector2D>()
            //        {
            //            new Vector2D(100, 300),
            //            new Vector2D(200, 100),
            //            new Vector2D(300, 300)
            //        },
            //        RandomPositions = true,
            //        RandomSetsForBest = new RandomSetsList(10, 3, 2, (int)DateTime.Now.Ticks),
            //        SeedForRandomSheepForBest = (int)DateTime.Now.Ticks,
            //        SheepType = ESheepType.Passive,
            //        TurnsOfHerding = 2000
            //    }
            //},
            {
                "2v2",
                new OptimizationParameters()
                {
                    NumberOfEras = 40000,
                    TurnsOfHerding = 600,
                    TargetFitness = 0,
                    NumberOfParticipants = 10,
                    MutationPower = 0.02,
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
                    RandomSetsForBest = new RandomSetsList(10, 2, 2, (int)DateTime.Now.Ticks),
                    PositionsOfShepherds = new List<Vector2D>()
                    {
                        new Vector2D(150, 200),
                        new Vector2D(250, 200)
                    },
                    PositionsOfSheep = new List<Vector2D>()
                    {
                        new Vector2D(0, 200),
                        new Vector2D(400, 200)
                    },
                    RandomizeNeuralNetOnInit = true
                }
            },
            //{
            //    "2v4PartialKnowledge",
            //    new OptimizationParameters()
            //    {
            //        NumberOfEras = int.MaxValue,
            //        TurnsOfHerding = 1000,
            //        TargetFitness = 0,
            //        NumberOfParticipants= 10,
            //        MutationPower = 0.01,
            //        SheepType = ESheepType.Passive,
            //        NumberOfSeenShepherds = 1,
            //        NumberOfSeenSheep = 2,
            //        NumberOfHiddenLayers = 1,
            //        NumberOfNeuronsInHiddenLayer = 10,
            //        PopulationSize = 100,
            //        FitnessType = EFitnessType.Final,
            //        NotIdenticalAgents = false,
            //        RandomPositions = true,
            //        NumberOfRandomSets = 10,
            //        SeedForRandomSheepForBest = 0,
            //        RandomSetsForBest = new RandomSetsList(10, 2, 4, (int)DateTime.Now.Ticks),
            //        PositionsOfShepherds = new List<Vector2D>()
            //        {
            //            new Vector2D(200, 0),
            //            new Vector2D(200, 400)
            //        },
            //        PositionsOfSheep = new List<Vector2D>()
            //        {
            //            new Vector2D(150, 200),
            //            new Vector2D(250, 200),
            //            new Vector2D(0, 200),
            //            new Vector2D(400, 200)
            //        }
            //    }
            //},
            //{
            //    "test10000",
            //    new OptimizationParameters()
            //    {
            //        FitnessType = EFitnessType.Final,
            //        MutationPower = 0.02,
            //        NotIdenticalAgents = false,
            //        NumberOfHiddenLayers = 1,
            //        NumberOfNeuronsInHiddenLayer = 30,
            //        NumberOfParticipants = 10,
            //        NumberOfRandomSets = 10,
            //        NumberOfSeenSheep = 10,
            //        NumberOfSeenShepherds = 2,
            //        NumberOfEras = int.MaxValue,
            //        TargetFitness = 0.0,
            //        PopulationSize = 250,
            //        PositionsOfSheep = new List<Vector2D>()
            //        {
            //            new Vector2D(0, 150),
            //            new Vector2D(0, 200),
            //            new Vector2D(0, 250),
            //            new Vector2D(150, 0),
            //            new Vector2D(150, 400),
            //            new Vector2D(250, 0),
            //            new Vector2D(250, 400),
            //            new Vector2D(400, 150),
            //            new Vector2D(400, 200),
            //            new Vector2D(400, 250)
            //        },
            //        PositionsOfShepherds = new List<Vector2D>()
            //        {
            //            new Vector2D(100, 300),
            //            new Vector2D(200, 100),
            //            new Vector2D(300, 300)
            //        },
            //        RandomPositions = true,
            //        RandomSetsForBest = new RandomSetsList(10000, 3, 10, (int)DateTime.Now.Ticks),
            //        SeedForRandomSheepForBest = (int)DateTime.Now.Ticks,
            //        SheepType = ESheepType.Passive,
            //        TurnsOfHerding = 2000
            //    }
            //},
            //{
            //    "freezing",
            //    new OptimizationParameters()
            //    {
            //        FitnessType = EFitnessType.Final,
            //        MutationPower = 0.02,
            //        NotIdenticalAgents = true,
            //        NumberOfHiddenLayers = 1,
            //        NumberOfNeuronsInHiddenLayer = 30,
            //        NumberOfParticipants = 10,
            //        NumberOfRandomSets = 10,
            //        NumberOfSeenSheep = 10,
            //        NumberOfSeenShepherds = 2,
            //        NumberOfEras = int.MaxValue,
            //        TargetFitness = 0.0,
            //        PopulationSize = 250,
            //        PositionsOfSheep = new List<Vector2D>()
            //        {
            //            new Vector2D(0, 150),
            //            new Vector2D(0, 200),
            //            new Vector2D(0, 250),
            //            new Vector2D(150, 0),
            //            new Vector2D(150, 400),
            //            new Vector2D(250, 0),
            //            new Vector2D(250, 400),
            //            new Vector2D(400, 150),
            //            new Vector2D(400, 200),
            //            new Vector2D(400, 250)
            //        },
            //        PositionsOfShepherds = new List<Vector2D>()
            //        {
            //            new Vector2D(100, 300),
            //            new Vector2D(200, 100),
            //            new Vector2D(300, 300)
            //        },
            //        RandomPositions = true,
            //        RandomSetsForBest = new RandomSetsList(10, 3, 10, (int)DateTime.Now.Ticks),
            //        SeedForRandomSheepForBest = (int)DateTime.Now.Ticks,
            //        SheepType = ESheepType.Passive,
            //        TurnsOfHerding = 2000,
            //        Freezing = true,
            //    }
            //},
            //{
            //    "simpleFreezing",
            //    new OptimizationParameters()
            //    {
            //        NumberOfEras = int.MaxValue,
            //        TurnsOfHerding = 250,
            //        TargetFitness = 0,
            //        NumberOfParticipants = 10,
            //        MutationPower = 0.01,
            //        SheepType = ESheepType.Passive,
            //        NumberOfSeenShepherds = 0,
            //        NumberOfSeenSheep = 2,
            //        NumberOfHiddenLayers = 1,
            //        NumberOfNeuronsInHiddenLayer = 10,
            //        PopulationSize = 100,
            //        FitnessType = EFitnessType.Final,
            //        NotIdenticalAgents = true,
            //        RandomPositions = true,
            //        NumberOfRandomSets = 10,
            //        SeedForRandomSheepForBest = 10,
            //        RandomSetsForBest = new RandomSetsList(10, 2, 2, 0),
            //        PositionsOfShepherds = new List<Vector2D>()
            //        {
            //            new Vector2D(150, 200),
            //            new Vector2D(250, 200)
            //        },
            //        PositionsOfSheep = new List<Vector2D>()
            //        {
            //            new Vector2D(0, 200),
            //            new Vector2D(400, 200)
            //        },
            //        Freezing = true, 
            //        FreezingInterval = 100
            //    }
            //}
        };
    }
}
