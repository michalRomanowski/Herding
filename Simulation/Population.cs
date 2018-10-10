using Agent;
using Auxiliary;
using System.Collections.Generic;
using System.Linq;
using Teams;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simulations
{
    public class Population
    {
        public int Id { get; set; }

        public List<Team> Units { get; set; }

        public Team Best { get; set; }

        [NotMapped]
        public readonly object BestLocker = new object();
        
        public Population() { }

        public Population(SimulationParameters simulationParameters)
        {
            Units = new List<Team>();

            for (int i = 0; i < simulationParameters.PopulationSize; i++)
            {
                Units.Add(TeamProvider.GetTeam(simulationParameters));

                Units.Last().Members.Add(
                    AgentFactory.GetShepherd(
                        simulationParameters.NumberOfSeenShepherds,
                        simulationParameters.NumberOfSeenSheep,
                        simulationParameters.NumberOfHiddenLayers,
                        simulationParameters.NumberOfNeuronsInHiddenLayer));

                Units.Last().AdjustSize(simulationParameters.NumberOfShepherds);
            }

            Best = Units[0].GetClone();
        }

        public void SetPositions(IList<Position> positions)
        {
            foreach(Team t in Units)
                t.SetPositions(positions);
        }

        public void RemoveRandom()
        {
            Units.RemoveAt(
                CRandom.r.Next(Units.Count));
        }

        public void AdjustTeamSize(int numberOfMembers)
        {
            foreach (Team t in Units)
                t.AdjustSize(numberOfMembers);
        }

        public void AdjustInputLayerSize(int numberOfSeenShepherds, int numberOfSeenSheep)
        { 
            foreach(var team in Units)
                team.AdjustInputLayerSize(numberOfSeenShepherds, numberOfSeenSheep);
        }
        
        public void AdjustHiddenLayersSize(int newSize)
        {
            foreach (var team in Units)
                team.AdjustHiddenLayersSize(newSize);
        }
    }
}
