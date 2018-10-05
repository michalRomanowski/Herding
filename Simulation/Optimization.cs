using Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Teams;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simulations
{
    public class Optimization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [NotMapped]
        public bool Stop
        {
            get
            {
                lock (_stopLocker)
                {
                    return _stop;
                }
            }
            set
            {
                lock (_stopLocker)
                {
                    _stop = value;
                }
            }
        }
        private readonly object _stopLocker = new object();
        private bool _stop { get; set; }

        [NotMapped]
        public int StepCount { get; private set; }
        
        public SimulationParameters Parameters { get; set; }
        public Population Shepherds { get; set; }

        private readonly BestTeamManager bestTeamManager = new BestTeamManager();
        private readonly Random r = new Random();

        private Tournament[] tournaments = new Tournament[2];

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

        public void Optimize()
        {
            Parameters.Progress = float.MinValue;
            
            Stop = false;

            if (Shepherds.Best != null)
            {
                Shepherds.Best.Fitness = float.MaxValue;
                bestTeamManager.UpdateBestTeam(Parameters, Shepherds, new List<Team>() { Shepherds.Best });
            }

            Parameters.BestResultAtStep = new List<float>();

            for (StepCount = 0; StepCount < Parameters.OptimizationSteps && Parameters.Progress < 1; StepCount++)
            {
                Step();

                if (Stop) break;
            }
        }

        private void Step()
        {
            Logger.AddLine("Era: " + StepCount);
            
            Parameters.Progress = (float)(StepCount + 1) / Parameters.OptimizationSteps;

            var selectionResults = Selection();

            var children = Crossover(selectionResults.parents);

            Mutation(children);

            Shepherds.Units.AddRange(children);

            bestTeamManager.UpdateBestTeam(Parameters, Shepherds, children);
            
            Parameters.BestResultAtStep.Add(Shepherds.Best.Fitness);
            Logger.AddLine("Best fitness: " + Shepherds.Best.Fitness);
        }

        private SelectionResults Selection()
        {
            List<Thread> tournamentThreads = new List<Thread>();
            
            StartTournaments(tournamentThreads);
            WaitForTournamentsToEnd(tournamentThreads);

            SelectionResults ret = GetTournamentResults();

            foreach (Tournament t in tournaments)
                t.ReturnParticipants();

            return ret;
        }

        private IList<Team> Crossover(IList<Team> parents)
        {
            List<Team> children = new List<Team>();

            for (int i = 0; i < parents.Count; i += 2)
                children.AddRange(parents[i].Crossover(parents[i + 1]));

            return children;
        }

        private void Mutation(IList<Team> teamsToMutate)
        {
            foreach (Team team in teamsToMutate)
                team.Mutate(Parameters.MutationPower, Parameters.AbsoluteMutationFactor);
        }

        #region Tournaments

        private void StartTournaments(List<Thread> tournamentThreads)
        {
            InitTournaments();
            
            foreach(var t in tournaments)
            {
                tournamentThreads.Add(new Thread(t.Attend));
                tournamentThreads.Last().Start();
            }
        }
        
        private void InitTournaments()
        {
            if (Parameters.RandomPositions) InitTournamentsWithRandomPositions();
            else InitTournamentsWithDefinedPositions();
        }

        private void InitTournamentsWithRandomPositions()
        {
            for (int i = 0; i < 2; i++)
            {
                var randomSets = new RandomSetsList();

                randomSets = new RandomSetsList(
                    Parameters.NumberOfRandomSets,
                    Parameters.PositionsOfShepherds.Count,
                    Parameters.PositionsOfSheep.Count,
                    CRandom.r.Next());

                tournaments[i] = new Tournament(
                    Parameters,
                    Shepherds,
                    randomSets.PositionsOfShepherdsSet,
                    randomSets.PositionsOfSheepSet);
            }
        }

        private void InitTournamentsWithDefinedPositions()
        {
            for (int i = 0; i < 2; i++)
            {
                tournaments[i] = new Tournament(
                    Parameters,
                    Shepherds,
                    Parameters.PositionsOfShepherds,
                    Parameters.PositionsOfSheep);
            }
        }
        private void WaitForTournamentsToEnd(List<Thread> tournamentThreads)
        {
            foreach (Thread thread in tournamentThreads)
                thread.Join();
        }

        private SelectionResults GetTournamentResults()
        {
            var ret = new SelectionResults();

            foreach (Tournament t in tournaments)
            {
                ret.parents.Add(t.Winner);
                ret.toDie.Add(t.Looser);
            }

            return ret;
        }

        #endregion
    }
}
