using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auxiliary;
using Teams;

namespace Simulations
{
    static class TournamentFactory
    {
        public static ITournament GetTournament(SimulationParameters simulationParameters, IEnumerable<Team> participants)
        {
            if (simulationParameters.RandomPositions)
            {
                return GetTournamentWithRandomPositions(simulationParameters, participants);
            }
            else
            {
                return GetTournamentWithFixedPositions(simulationParameters, participants);
            }
        }

        private static ITournament GetTournamentWithRandomPositions(SimulationParameters simulationParameters, IEnumerable<Team> participants)
        {
            var randomSets = new RandomSetsList(
                simulationParameters.NumberOfRandomSets,
                simulationParameters.PositionsOfShepherds.Count,
                simulationParameters.PositionsOfSheep.Count,
                CRandom.Instance.Next());

            var countFitnessParameters = new CountFitnessParameters()
            {
                PositionsOfSheepSet = randomSets.PositionsOfSheepSet,
                PositionsOfShepherdsSet = randomSets.PositionsOfShepherdsSet,
                TurnsOfHerding = simulationParameters.TurnsOfHerding,
                SheepType = simulationParameters.SheepType,
                Seed = CRandom.Instance.Next()
            };

            return new Tournament(
                simulationParameters.FitnessType,
                countFitnessParameters,
                participants);
        }

        private static ITournament GetTournamentWithFixedPositions(SimulationParameters simulationParameters, IEnumerable<Team> participants)
        {
            var countFitnessParameters = new CountFitnessParameters()
            {
                PositionsOfSheepSet = new List<IList<Position>> { simulationParameters.PositionsOfSheep },
                PositionsOfShepherdsSet = new List<IList<Position>> { simulationParameters.PositionsOfShepherds },
                TurnsOfHerding = simulationParameters.TurnsOfHerding,
                SheepType = simulationParameters.SheepType,
                Seed = CRandom.Instance.Next()
            };

            return new Tournament(
                simulationParameters.FitnessType,
                countFitnessParameters,
                participants);
        }
    }
}