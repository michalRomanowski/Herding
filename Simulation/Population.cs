using Agent;
using Auxiliary;
using System.Collections.Generic;
using System.Linq;
using Teams;

namespace Simulations
{
    public class Population
    {
        public int Id { get; set; }

        public List<Team> Units { get; set; }

        public Team Best { get; set; }
        
        public Population() { }

        public Population(PopulationParameters parameters)
        {
            Units = new List<Team>();

            for (int i = 0; i < parameters.PopulationSize; i++)
            {
                Units.Add(TeamFactory.GetTeam(parameters.TeamParameters));
            }

            Best = Units.FirstOrDefault()?.GetClone();
        }

        public void SetPositions(IList<Position> positions)
        {
            foreach(Team t in Units)
                t.SetPositions(positions);
        }

        public void ResizeTeam(int newTeamSize)
        {
            Best.Resize(newTeamSize);

            foreach (Team t in Units)
                t.Resize(newTeamSize);
        }

        public void ResizeNeuralNet(int numberOfSeenShepherds, int numberOfSeenSheep, int numberOfHiddenLayers, int hiddenLayerSize)
        {
            Best.ResizeNeuralNet(numberOfSeenShepherds, numberOfSeenSheep, numberOfHiddenLayers, hiddenLayerSize);

            foreach (var t in Units)
                t.ResizeNeuralNet(numberOfSeenShepherds, numberOfSeenSheep, numberOfHiddenLayers, hiddenLayerSize);
        }

        public IReadOnlyList<IEnumerable<Team>> GetRandomUniqueSubsets(int numberOfSubsets, int subsetSize)
        {
            var randomIndexesSequence = new RandomUniqueSequence(Units.Count);

            var subsets = new List<IEnumerable<Team>>(numberOfSubsets);

            for (int i = 0; i < numberOfSubsets; i++)
            {
                subsets.Add(
                    randomIndexesSequence.GetSubSequence(subsetSize).Select(x => Units[x]).ToList());
            }

            return subsets;
        }

        public void Replace(IEnumerable<Team> newUnits, IEnumerable<Team> oldUnits)
        {
            foreach (var t in oldUnits)
            {
                Units.Remove(t);
            }

            Units.AddRange(newUnits);
        }
    }
}
