using Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Team;

namespace Simulation
{
    public class Optimization
    {
        public bool Stop;
        public readonly object StopLocker = new object();

        public int StepCount { get; private set; }

        public SimulationParameters Parameters { get; private set; }
        public Population Shepards { get; private set; }

        private const int AUTOSAVE_PERIOD = 100000;
        private const bool AUTOSAVE_ACTIVE = true;

        private readonly BestTeamManager bestTeamManager = new BestTeamManager();
        private readonly Random r = new Random();

        public Optimization(SimulationParameters parameters, Population shepards)
        {
            Parameters = parameters;
            Shepards = shepards;
        }

        public void Optimize()
        {
            Parameters.Progress = float.MinValue;
            
            lock (StopLocker)
            {
                Stop = false;
            }

            if (Shepards.Best != null)
            {
                Shepards.Best.Fitness = float.MaxValue;
                bestTeamManager.UpdateBestTeam(Parameters, Shepards, new List<ITeam>() { Shepards.Best });
            }

            Parameters.BestResultAtStep = new List<float>();

            for (StepCount = 0; StepCount < Parameters.OptimizationSteps && Parameters.Progress < 1; StepCount++)
            {
                Step();

                lock (StopLocker)
                {
                    if (Stop) break;
                }

                if (AUTOSAVE_ACTIVE && StepCount % AUTOSAVE_PERIOD == 0)
                {
                    //storage.Autosave();
                }
            }
        }

        private void Step()
        {
            Logger.AddLine("Era: " + StepCount);
            
            Parameters.Progress = (float)(StepCount + 1) / Parameters.OptimizationSteps;

            var selectionResults = Selection();

            var children = Crossover(selectionResults.parents);

            Mutation(children);

            Shepards.Units.AddRange(children);

            bestTeamManager.UpdateBestTeam(Parameters, Shepards, children);
            
            Parameters.BestResultAtStep.Add(Shepards.Best.Fitness);
            Logger.AddLine("Best fitness: " + Shepards.Best.Fitness);
        }

        private SelectionResults Selection()
        {
            List<Tournament> tournaments = InitTournaments();

            List<Thread> tournamentThreads = new List<Thread>();

            StartTournaments(tournaments, tournamentThreads);
            WaitForTournamentsToEnd(tournamentThreads);

            SelectionResults ret = GetTournamentResults(tournaments);

            foreach (Tournament t in tournaments)
                t.ReturnParticipants();

            return ret;
        }

        private IList<ITeam> Crossover(IList<ITeam> parents)
        {
            List<ITeam> children = new List<ITeam>();

            for (int i = 0; i < parents.Count; i += 2)
                children.AddRange(parents[i].Crossover(parents[i + 1]));

            return children;
        }

        private void Mutation(IList<ITeam> teamsToMutate)
        {
            foreach (ITeam team in teamsToMutate)
                team.Mutate(Parameters.MutationPower, Parameters.AbsoluteMutationFactor);
        }

        #region Tournaments

        private List<Tournament> InitTournaments()
        {
            if (Parameters.RandomPositions)
                return InitTournamentsWithRandomPositions();
            else
                return InitTournamentsWithDefinedPositions();
        }

        private List<Tournament> InitTournamentsWithRandomPositions()
        {
            List<Tournament> ret = new List<Tournament>();

            for (int i = 0; i < 2; i++)
            {
                var randomSets = new RandomSetsList();

                randomSets = new RandomSetsList(
                    Parameters.NumberOfRandomSets, 
                    Parameters.PositionsOfShepards.Count, 
                    Parameters.PositionsOfSheep.Count,
                    CRandom.r.Next());

                ret.Add(new Tournament(
                    Parameters,
                    Shepards,
                    randomSets.PositionsOfShepardsSet, 
                    randomSets.PositionsOfSheepSet));
            }

            return ret;
        }

        private List<Tournament> InitTournamentsWithDefinedPositions()
        {
            List<Tournament> ret = new List<Tournament>();

            for (int i = 0; i < 2; i++)
            {
                ret.Add(new Tournament(
                    Parameters,
                    Shepards,
                    Parameters.PositionsOfShepards, 
                    Parameters.PositionsOfSheep));
            }

            return ret;
        }
        
        private void StartTournaments(List<Tournament> tournaments, List<Thread> tournamentThreads)
        {
            foreach (Tournament t in tournaments)
            {
                tournamentThreads.Add(new Thread(t.Attend));
                tournamentThreads.Last().Start();
            }
        }

        private void WaitForTournamentsToEnd(List<Thread> tournamentThreads)
        {
            foreach (Thread thread in tournamentThreads)
                thread.Join();
        }

        private SelectionResults GetTournamentResults(List<Tournament> tournaments)
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
