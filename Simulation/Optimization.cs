using Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Teams;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Simulations
{
    public class Optimization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        private readonly object stopLocker = new object();
        private bool stop { get; set; }

        [NotMapped]
        public int StepCount { get; private set; }
        
        public SimulationParameters Parameters { get; set; }
        public Population Shepherds { get; set; }

        private IBestTeamSelector bestTeamSelector;
        
        public Optimization()
        {
            Parameters = new SimulationParameters();
        }

        public Optimization(SimulationParameters parameters)
        {
            Parameters = parameters;
            Shepherds = new Population(parameters);
        }

        public Optimization(SimulationParameters parameters, Population shepherds)
        {
            Parameters = parameters;
            Shepherds = shepherds;
        }
        
        public void Start()
        {
            Parameters.Progress = float.MinValue;

            lock (stopLocker)
                stop = false;

            bestTeamSelector = BestBestTeamSelectorFactory.GetBestTeamSelector(Parameters);

            Parameters.BestResultAtStep = new List<float>();
            
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
                Step();
            }
        }

        private void Step()
        {
            Logger.AddLine("Era: " + StepCount);
            
            Parameters.Progress = (float)(StepCount + 1) / Parameters.OptimizationSteps;

            var selectionResults = new Selection(Parameters, Shepherds).Select();

            var children = Crossover(selectionResults.Winners.ToList());

            Mutation(children);

            UpdateBestTeam(children);

            Shepherds.Replace(children, selectionResults.Losers);
        }
        
        private IReadOnlyList<Team> Crossover(IReadOnlyList<Team> parents)
        {
            List<Team> children = new List<Team>();

            for (int i = 0; i < parents.Count; i += 2)
                children.AddRange(parents[i].Crossover(parents[i + 1]));

            return children;
        }

        private void Mutation(IEnumerable<Team> teamsToMutate)
        {
            foreach (Team team in teamsToMutate)
                team.Mutate(Parameters.MutationPower, Parameters.AbsoluteMutationFactor);
        }

        private void UpdateBestTeam(IEnumerable<Team> newTeams)
        {
            var bestPretenders = newTeams.Concat(new List<Team>() { Shepherds.Best });
            Shepherds.Best = bestTeamSelector.GetBestTeam(bestPretenders).GetClone();

            foreach(var nt in newTeams)
            {
                Logger.AddLine("New fitness: " + nt.Fitness);
            }

            Parameters.BestResultAtStep.Add(Shepherds.Best.Fitness);
            Logger.AddLine("Best fitness: " + Shepherds.Best.Fitness);
        }
    }
}
