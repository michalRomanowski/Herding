using System;
using System.Collections.Generic;
using System.Linq;
using Teams;
using MathNet.Spatial.Euclidean;

namespace Simulations
{
    public class Population
    {
        public List<Team> Units { get; set; }
        
        public Team Best { get; set; }
        
        public Population()
        {
            Units = new List<Team>();
            Best = Units.FirstOrDefault()?.GetClone();
        }

        public Population(IPopulationParameters parameters)
        {
            Randomize(parameters);
        }

        public void Randomize(IPopulationParameters parameters)
        {
            Units = new List<Team>();

            for (int i = 0; i < parameters.PopulationSize; i++)
            {
                Units.Add(TeamFactory.GetTeam(parameters));
            }

            Best = Units.FirstOrDefault()?.GetClone();
        }

        public void SetPositions(IList<Vector2D> positions)
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
    }
}
