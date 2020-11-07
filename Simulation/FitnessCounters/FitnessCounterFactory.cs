using System;
using System.Collections.Generic;
using Auxiliary;
using MathNet.Spatial.Euclidean;
using Simulations.Parameters;

namespace Simulations
{
    public static class FitnessCounterFactory
    {
        public static IFitnessCounter GetFitnessCounterForBest(OptimizationParameters optimizationParameters)
        {
            var countFitnessParameters = new CountFitnessParameters()
            {
                FitnessType = optimizationParameters.FitnessType,
                PositionsOfSheepSet = optimizationParameters.RandomPositions ? 
                    optimizationParameters.RandomSetsForBest.PositionsOfSheepSet : 
                    new List<List<Vector2D>>() { optimizationParameters.PositionsOfSheep },
                PositionsOfShepherdsSet = optimizationParameters.RandomPositions ? 
                    optimizationParameters.RandomSetsForBest.PositionsOfShepherdsSet : 
                    new List<List<Vector2D>>() { optimizationParameters.PositionsOfShepherds },
                SheepType = optimizationParameters.SheepType,
                TurnsOfHerding = optimizationParameters.TurnsOfHerding
            };

            return GetFitnessCounter(countFitnessParameters);
        }

        public static IFitnessCounter GetFitnessCounterForTraining(OptimizationParameters optimizationParameters)
        {
            var countFitnessParameters = new CountFitnessParameters()
            {
                FitnessType = optimizationParameters.FitnessType,
                PositionsOfSheepSet = optimizationParameters.RandomPositions ?
                    VectorFactory.GenerateRandomPositions(optimizationParameters.NumberOfRandomSets, optimizationParameters.NumberOfSheep, 0, 400) :
                    new List<List<Vector2D>>() { optimizationParameters.PositionsOfSheep },
                PositionsOfShepherdsSet = optimizationParameters.RandomPositions ?
                    VectorFactory.GenerateRandomPositions(optimizationParameters.NumberOfRandomSets, optimizationParameters.NumberOfShepherds, 0, 400) :
                    new List<List<Vector2D>>() { optimizationParameters.PositionsOfShepherds },
                SheepType = optimizationParameters.SheepType,
                TurnsOfHerding = optimizationParameters.TurnsOfHerding
            };

            return GetFitnessCounter(countFitnessParameters);
        }

        private static IFitnessCounter GetFitnessCounter(CountFitnessParameters parameters)
        {
            if (parameters.FitnessType == EFitnessType.Final)
            {
                return new FinalFitnessCounter(parameters);
            }
            else if (parameters.FitnessType == EFitnessType.Sum)
            {
                return new SumFitnessCounter(parameters);
            }
            else throw new ArgumentException();
        }
    }
}