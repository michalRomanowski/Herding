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
                return GetTournamentWithDefinedPositions(simulationParameters, participants);
            }
        }

        private static ITournament GetTournamentWithRandomPositions(SimulationParameters simulationParameters, IEnumerable<Team> participants)
        {
            var randomSets = new RandomSetsList(
                simulationParameters.NumberOfRandomSets,
                simulationParameters.PositionsOfShepherds.Count,
                simulationParameters.PositionsOfSheep.Count,
                CRandom.Instance.Next());

            return new Tournament(
                simulationParameters,
                randomSets.PositionsOfShepherdsSet,
                randomSets.PositionsOfSheepSet,
                participants);
        }

        private static ITournament GetTournamentWithDefinedPositions(SimulationParameters simulationParameters, IEnumerable<Team> participants)
        {
            return new Tournament(
                simulationParameters,
                new List<IList<Position>> { simulationParameters.PositionsOfShepherds },
                new List<IList<Position>> { simulationParameters.PositionsOfSheep },
                participants);
        }
    }
}