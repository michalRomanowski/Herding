using System;
using Auxiliary;
using System.Collections.Generic;
using System.Linq;
using Teams;

namespace Simulations
{
    public class Optimization
    {
        public OptimizationParameters Parameters { get; set; }
        public Population Shepherds { get; set; }
        
        private double BestFitness;
        private double ControlFitness;
        
        private BestTeamSelector bestTeamSelector;
        private IFitnessCounter controlFitnessCounter;
        private CountFitnessParameters controlFitnessParameters;

        private Selection selection;

        private ISimulationRepository repository;

        private const double ABSOLUTE_MUTATION_FACTOR = 1.0;
        private const int AUTOSAVE_FREQUENCY = 1000;

        public Optimization(OptimizationParameters parameters, Population shepherds, ISimulationRepository repository)
            : this(parameters, shepherds, repository, null){ }

        public Optimization(OptimizationParameters parameters, 
            Population shepherds, 
            ISimulationRepository repository, 
            CountFitnessParameters controlFitnessParameters)
        {
            Parameters = parameters;
            Shepherds = shepherds;
            this.repository = repository;

            if(controlFitnessParameters != null)
            {
                this.controlFitnessCounter = FitnessCounterFactory.GetFitnessCounter(controlFitnessParameters);
                this.controlFitnessParameters = controlFitnessParameters;
            }
        }
        
        public void Start()
        {
            BestFitness = double.MaxValue;

            bestTeamSelector = new BestTeamSelector(
                FitnessCounterFactory.GetFitnessCounter(Parameters.GetBestTeamSelectorParameters().CountFitnessParameters));
            
            selection = new Selection(
                new SelectionParameters(){
                    OptimizationParameters = Parameters,
                    Population = Shepherds});

            Optimize();
        }

        private void Optimize()
        {
            repository.Save($"START_{DateTime.Now.ToString("yyyyMMddHHmmss")}", Parameters, Shepherds);

            for (int era = 0; era < Parameters.NumberOfEras && BestFitness > Parameters.TargetFitness; era++)
            {
                Step();

                Log(era);

                if (era % AUTOSAVE_FREQUENCY == 0)
                    repository.Save(DateTime.Now.ToString("yyyyMMddHHmmss"), Parameters, Shepherds);
            }

            repository.Save($"END_{DateTime.Now.ToString("yyyyMMddHHmmss")}", Parameters, Shepherds);
        }

        private void Step()
        {
            var selectionResults = selection.Select();

            Mutation(selectionResults);

            Replace(Crossover(selectionResults.Winners), selectionResults.Losers);

            if (UpdateBestTeam(selectionResults.Winners))
            {
                UpdateControlFitness();
            }
        }

        private IEnumerable<Team> Crossover(IList<Team> parents)
        {
            return new List<Team>() {
                parents[0].Crossover(parents[1]),
                parents[0].Crossover(parents[1]),
                parents[1].Crossover(parents[0]),
                parents[1].Crossover(parents[0])};
        }

        private void Mutation(SelectionResult selectionResults)
        {
            foreach (var l in selectionResults.Losers)
                l.Mutate(Parameters.MutationPower, ABSOLUTE_MUTATION_FACTOR);
        }

        private void Replace(IEnumerable<Team> newUnits, IList<Team> oldUnits)
        {
            foreach(var nu in newUnits)
            {
                if (Shepherds.Units.Remove(oldUnits.ElementAt(CRandom.Instance.Next(oldUnits.Count()))))
                    Shepherds.Units.Add(nu);
            }
        }

        private bool UpdateBestTeam(IList<Team> pretenders)
        {
            pretenders.Add(Shepherds.Best);
            
            var bestPretenderWithFitness = bestTeamSelector.GetBestTeam(pretenders);
            
            if(Shepherds.Best != bestPretenderWithFitness.Team)
            {
                Shepherds.Best = bestPretenderWithFitness.Team.GetClone();
                BestFitness = bestPretenderWithFitness.Fitness;
                return true;
            }

            return false;
        }

        private void UpdateControlFitness()
        {
            if (controlFitnessCounter == null)
                return;

            var controlTeam = Shepherds.Best.GetClone();
            controlTeam.Resize(controlFitnessParameters.PositionsOfShepherdsSet.First().Count);

            ControlFitness = controlFitnessCounter.CountFitness(controlTeam);
        }

        private void Log(int era)
        {
            Logger.Instance.AddLine("Era: " + era);
            Logger.Instance.AddLine("Time: " + DateTime.Now.ToString());            
            if (controlFitnessCounter != null)
                Logger.Instance.AddLine($"Control Fitness: {ControlFitness}");
        }
    }
}
