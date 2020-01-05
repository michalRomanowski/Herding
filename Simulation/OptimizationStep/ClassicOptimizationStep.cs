using System.Collections.Generic;
using System.Linq;
using Auxiliary;
using Teams;

namespace Simulations.OptimizationStep
{
    class ClassicOptimizationStep : OptimizationStep
    {
        private readonly Selection selection;
        
        public ClassicOptimizationStep(
            OptimizationParameters parameters, 
            Population population) : base(parameters, population)
        {
            selection = new Selection(parameters, population, 2);
        }

        public override void Step()
        {
            var selectionResults = selection.Select();

            Mutation(selectionResults);

            Replace(Crossover(selectionResults.Winners), selectionResults.Losers);

            UpdateBestTeam(selectionResults.Winners);
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
                l.Mutate(parameters.MutationPower, ABSOLUTE_MUTATION_FACTOR);
        }

        private void Replace(IEnumerable<Team> newUnits, IList<Team> oldUnits)
        {
            foreach (var nu in newUnits)
            {
                if (population.Units.Remove(oldUnits.ElementAt(CRandom.Instance.Next(oldUnits.Count()))))
                    population.Units.Add(nu);
            }
        }
    }
}
