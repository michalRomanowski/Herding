using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simulations;

namespace UnitTests
{
    static class MyAssert
    {
        public static void AreEqual(OptimizationParameters a, OptimizationParameters b)
        {
            Assert.IsTrue(a.FitnessType == b.FitnessType);
            Assert.IsTrue(a.MutationPower == b.MutationPower);
            Assert.IsTrue(a.NotIdenticalAgents == b.NotIdenticalAgents);
            Assert.IsTrue(a.NumberOfEras == b.NumberOfEras);
            Assert.IsTrue(a.NumberOfHiddenLayers == b.NumberOfHiddenLayers);
            Assert.IsTrue(a.NumberOfNeuronsInHiddenLayer == b.NumberOfNeuronsInHiddenLayer);
            Assert.IsTrue(a.NumberOfParticipants == b.NumberOfParticipants);
            Assert.IsTrue(a.NumberOfRandomSets == b.NumberOfRandomSets);
            Assert.IsTrue(a.NumberOfSeenSheep == b.NumberOfSeenSheep);
            Assert.IsTrue(a.NumberOfSeenShepherds == b.NumberOfSeenShepherds);
            Assert.IsTrue(a.NumberOfSheep == b.NumberOfSheep);
            Assert.IsTrue(a.NumberOfShepherds == b.NumberOfShepherds);
            Assert.IsTrue(a.PopulationSize == b.PopulationSize);
            Assert.IsTrue(a.RandomPositions == b.RandomPositions);
            Assert.IsTrue(a.RandomSetsForBest.Count == b.RandomSetsForBest.Count);
            Assert.IsTrue(a.SeedForRandomSheepForBest == b.SeedForRandomSheepForBest);
            Assert.IsTrue(a.SheepType == b.SheepType);
            Assert.IsTrue(a.TurnsOfHerding == b.TurnsOfHerding);
        }
    }
}
