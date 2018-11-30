using System;
using Auxiliary;
using System.Collections.Generic;
using System.Linq;
using Teams;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simulations
{
    public class Optimization
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private readonly object stopLocker = new object();
        private bool stop;

        [NotMapped]
        public int StepCount { get; private set; }

        public SimulationParameters Parameters { get; set; }
        public Population Shepherds { get; set; }

        private IBestTeamSelector bestTeamSelector;
        private IFitnessCounter controlFitnessCounter;
        private CountFitnessParameters controlFitnessParameters;

        private Selection selection;

        private IAutosaver _autosaver;
        [NotMapped]
        public IAutosaver Autosaver { set { _autosaver = value; } }

        [NotMapped]
        public float BestFitness { get; private set; }
        [NotMapped]
        private float controlFitness;

        [NotMapped]
        private const float TEAM_MUTATION_CHANCE = 0.02f;
        [NotMapped]
        private const int CHILDREN_ON_EACH_STEP = 4; // Must be Even 
        [NotMapped]
        private const int NUMBER_OF_TOURNAMENTS = 2;
        [NotMapped]
        private const int WINNERS_PER_TOURNAMENT = 1;
        [NotMapped]
        private const int LOSERS_PER_TOURNAMENT = CHILDREN_ON_EACH_STEP / NUMBER_OF_TOURNAMENTS;

        public Optimization()
        {
            Parameters = new SimulationParameters();
        }

        public Optimization(IAutosaver autosaver) : this(new SimulationParameters(), autosaver) { }

        public Optimization(SimulationParameters parameters, IAutosaver autosaver) : this(parameters, new Population(parameters.GetPopulationParameters()), autosaver) { }

        public Optimization(SimulationParameters parameters, Population shepherds, IAutosaver autosaver)
        {
            Parameters = parameters;
            Shepherds = shepherds;

            this._autosaver = autosaver;
        }
        
        public void SetControlFitnessCounter(IFitnessCounter controlFitnessCounter, CountFitnessParameters controlFitnessParameters)
        {
            this.controlFitnessCounter = controlFitnessCounter;
            this.controlFitnessParameters = controlFitnessParameters;
        }

        public void Start()
        {
            Parameters.Progress = float.MinValue;

            lock (stopLocker)
                stop = false;
            
            bestTeamSelector = BestTeamSelectorFactory.GetBestTeamSelector(Parameters.GetBestTeamSelectorParameters());
            
            selection = new Selection(
                new SelectionParameters(){
                    SimulationParameters = Parameters,
                    Population = Shepherds,
                    NumberOfTournaments = NUMBER_OF_TOURNAMENTS,
                    WinnersPerTournament = WINNERS_PER_TOURNAMENT,
                    LosersPerTournament = LOSERS_PER_TOURNAMENT});

            Optimize();
        }

        public void Stop()
        {
            lock (stopLocker)
                stop = true;
        }

        private void Optimize()
        {
            for (StepCount = 0; StepCount < Parameters.OptimizationSteps && Parameters.Progress < 1 && stop == false; StepCount++)
            {
                Logger.AddLine("Era: " + StepCount);
                Logger.AddLine("Time: " + DateTime.Now.ToString());
                Parameters.Progress = (float)(StepCount + 1) / Parameters.OptimizationSteps;

                Step();

                _autosaver.Autosave(this, StepCount);
            }
        }

        private void Step()
        {
            Mutation();
            
            var selectionResults = selection.Select();

            var children = Crossover(selectionResults.Winners.First(), selectionResults.Winners.Last());

            Shepherds.Replace(children, selectionResults.Losers);

            var newBestTeam = UpdateBestTeam(ComposeBestPretenders(selectionResults.Winners));

            if (newBestTeam)
                UpdateControlFitness();

            LogControlFitness();
        }

        private IEnumerable<Team> Crossover(Team parent1, Team parent2)
        {
            List<Team> children = new List<Team>();

            for (int i = 0; i < CHILDREN_ON_EACH_STEP; i += 2)
                children.AddRange(parent1.Crossover(parent2));

            return children;
        }

        private IEnumerable<Team> Mutation()
        {
            var mutated = new List<Team>();

            foreach(var t in Shepherds.Units)
            {
                if(CRandom.Instance.NextFloat(0, 1.0f) < TEAM_MUTATION_CHANCE)
                {
                    t.Mutate(Parameters.MutationPower, Parameters.AbsoluteMutationFactor);
                    mutated.Add(t);
                }
            }

            return mutated;
        }

        private bool UpdateBestTeam(IEnumerable<Team> pretenders)
        {            
            var best = bestTeamSelector.GetBestTeam(pretenders);
            var bestClone = best.GetClone();
            
            bool bestChanged = false;

            if (best.Fitness != Shepherds.Best.Fitness)
            {
                Shepherds.Best = bestClone; 
                bestChanged = true;
            }

            BestFitness = Shepherds.Best.Fitness;
            
            Logger.AddLine("Best fitness: " + Shepherds.Best.Fitness);
            return bestChanged;
        }

        private IEnumerable<Team> ComposeBestPretenders(IEnumerable<Team> newTeams)
        {
            var pretenders = newTeams.Select(x => x).ToList();
            pretenders.Add(Shepherds.Best);

            return pretenders;
        }

        private void UpdateControlFitness()
        {
            if (controlFitnessCounter == null)
                return;

            var controlTeam = Shepherds.Best.GetClone();
            controlTeam.Resize(controlFitnessParameters.PositionsOfShepherdsSet.First().Count);

            controlFitness = controlFitnessCounter.CountFitness(controlTeam);
        }

        private void LogControlFitness()
        {
            if (controlFitnessCounter == null)
                return;

            Logger.AddLine($"Control Fitness: {controlFitness}");
        }
    }
}
