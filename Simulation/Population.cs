using Agent;
using Auxiliary;
using System.Collections.Generic;
using System.Linq;
using Team;

namespace Simulation
{
    public class Population
    {
        public List<ITeam> Units = new List<ITeam>();

        public readonly object BestLocker = new object();

        public ITeam Best;
        public float AverageFitness;

        private IAverageFitnessCounter averageFitnessCounter;

        public Population(SimulationParameters simulationParameters)
        {
            averageFitnessCounter = new AverageFitnessCounter(simulationParameters);

            Units = new List<ITeam>();

            for (int i = 0; i < simulationParameters.PopulationSize; i++)
            {
                Units.Add(TeamProvider.GetTeam(simulationParameters));

                Units.Last().Members.Add(new Shepard(
                            0,
                            0,
                            simulationParameters.NumberOfSeenShepards,
                            simulationParameters.NumberOfSeenSheep,
                            simulationParameters.NumberOfSeenWolfs,
                            simulationParameters.NumberOfHiddenLayers,
                            simulationParameters.NumberOfNeuronsInHiddenLayer));

                Units.Last().AdjustSize(simulationParameters.NumberOfShepards);
            }

            Best = Units[0];
        }

        public Population(SimulationParameters simulationParameters, IEnumerable<string> compressed) : this(simulationParameters)
        {
            Best = TeamProvider.GetTeam(simulationParameters, compressed.First());

            Units = compressed.Skip(1).Select(x => TeamProvider.GetTeam(simulationParameters, x)).ToList();
        }

        public void SetPositions(IList<Position> positions)
        {
            foreach(ITeam t in Units)
                t.SetPositions(positions);
        }

        public void CountAverageFitness(SimulationParameters simulationParameters)
        {
            AverageFitness = averageFitnessCounter.CountAverageFitness(simulationParameters, this, simulationParameters.SeedForRandomSheepForBest);
            Logger.AddLine("Average fitness: " + AverageFitness);
        }

        public void RemoveRandom()
        {
            Units.RemoveAt(
                CRandom.r.Next(Units.Count));
        }

        public void AdjustTeamSize(int numberOfMembers)
        {
            foreach (ITeam t in Units)
                t.AdjustSize(numberOfMembers);
        }

        public void AdjustInputLayerSize(int numberOfSeenShepards, int numberOfSeenSheep)
        { 
            foreach(var team in Units)
                team.AdjustInputLayerSize(numberOfSeenShepards, numberOfSeenSheep);
        }

        public void AdjustHiddenLayersSize(int newSize)
        {
            foreach (var team in Units)
                team.AdjustHiddenLayersSize(newSize);
        }

        public IEnumerable<string> Compress()
        {
            List<string> compressed = new List<string>()
            {
                Best.Compress()
            };

            compressed.AddRange(Units.Select(x => x.Compress()));

            return compressed;
        }
    }
}
