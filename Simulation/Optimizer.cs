using Auxiliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Team;

namespace Simulation
{
    public class Optimizer
    {
        public bool Stop;
        public readonly object StopLocker = new object();
        
        public int StepCount { get; private set; }

        private const int AUTOSAVE_PERIOD = 100000;
        private const bool AUTOSAVE_ACTIVE = true;

        private readonly BestTeamManager bestTeamManager = new BestTeamManager();
        private readonly Random r = new Random();

        public void Optimize(SimulationParameters simulationParameters, Populations populations)
        {
            simulationParameters.Progress = float.MinValue;

            lock (StopLocker)
            {
                Stop = false;
            }

            if (populations.Shepards.Best != null)
            {
                populations.Shepards.Best.Fitness = float.MaxValue;
                bestTeamManager.UpdateBestTeam(simulationParameters, populations.Shepards, new List<ITeam>() { populations.Shepards.Best });
            }

            simulationParameters.BestResultAtStep = new List<float>();

            for (StepCount = 0; StepCount < simulationParameters.OptimizationSteps && simulationParameters.Progress < 1; StepCount++)
            {
                Step(simulationParameters, populations);

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

        private void Step(SimulationParameters simulationParameters, Populations populations)
        {
            Logger.AddLine("Era: " + StepCount);
            
            simulationParameters.Progress = (float)(StepCount + 1) / simulationParameters.OptimizationSteps;

            var selectionResults = Selection(simulationParameters, populations);

            var children = Crossover(selectionResults.parents);

            Mutation(children, simulationParameters);

            populations.Shepards.Units.AddRange(children);

            bestTeamManager.UpdateBestTeam(simulationParameters, populations.Shepards, children);
            
            simulationParameters.BestResultAtStep.Add(populations.Shepards.Best.Fitness);
            Logger.AddLine("Best fitness: " + populations.Shepards.Best.Fitness);
        }

        private SelectionResults Selection(SimulationParameters simulationParameters, Populations populations)
        {
            List<Tournament> tournaments = InitTournaments(simulationParameters, populations);

            List<Thread> tournamentThreads = new List<Thread>();

            StartTournaments(tournaments, tournamentThreads);
            WaitForTournamentsToEnd(tournamentThreads);

            SelectionResults ret = GetTournamentResults(tournaments);

            foreach (Tournament t in tournaments)
                t.ReturnParticipants();

            return ret;
        }

        #region Tournaments Initialization

        private List<Tournament> InitTournaments(SimulationParameters simulationParameters, Populations populations)
        {
            List<Tournament> ret = new List<Tournament>();

            if (simulationParameters.RandomPositions == true)
            {
                ret = InitTournamentsWithRandomPositions(simulationParameters, populations);
            }
            else
            {
                ret = InitTournamentsWithDefinedPositions(simulationParameters, populations);
            }

            return ret;
        }

        private List<Tournament> InitTournamentsWithRandomPositions(SimulationParameters simulationParameters, Populations populations)
        {
            List<Tournament> ret = new List<Tournament>();

            for (int i = 0; i < 2; i++)
            {
                var randomSets = new RandomSetsList();

                randomSets = new RandomSetsList(
                    simulationParameters.NumberOfRandomSets, 
                    simulationParameters.PositionsOfShepards.Count, 
                    simulationParameters.PositionsOfSheep.Count, 
                    simulationParameters.PositionsOfWolfs.Count, 
                    CRandom.r.Next());

                ret.Add(new Tournament(
                    simulationParameters,
                    populations.Shepards,
                    randomSets.PositionsOfShepardsSet, 
                    randomSets.PositionsOfSheepSet,
                    randomSets.PositionsOfWolfsSet));
            }

            return ret;
        }

        private List<Tournament> InitTournamentsWithDefinedPositions(SimulationParameters simulationParameters, Populations populations)
        {
            List<Tournament> ret = new List<Tournament>();

            for (int i = 0; i < 2; i++)
            {
                ret.Add(new Tournament(
                    simulationParameters,
                    populations.Shepards,
                    simulationParameters.PositionsOfShepards, 
                    simulationParameters.PositionsOfSheep, 
                    simulationParameters.PositionsOfWolfs));
            }

            return ret;
        }

        #endregion

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

        private IList<ITeam> Crossover(IList<ITeam> parents)
        {
            List<ITeam> children = new List<ITeam>();

            for (int i = 0; i < parents.Count; i += 2)
                children.AddRange(parents[i].Crossover(parents[i + 1]));

            return children;
        }

        private void Mutation(IList<ITeam> teamsToMutate, SimulationParameters simulationParameters)
        {
            foreach (ITeam team in teamsToMutate)
                team.Mutate(simulationParameters.MutationPower, simulationParameters.AbsoluteMutationFactor);
        }
    }
}
