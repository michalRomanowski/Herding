using System.Linq;

namespace Simulations.OptimizationStep
{
    class SimplifiedOptimizationStep : OptimizationStep
    {
        private readonly Selection selection;

        public SimplifiedOptimizationStep(
            OptimizationParameters parameters,
            Population population) : base(parameters, population)
        {
            selection = new Selection(parameters, population, 1);  
        }

        public override void Step()
        {
            var selectionResults = selection.Select();

            var winner = selectionResults.Winners.First();

            for(int i = 0; i < selectionResults.Losers.Count; i++)
            {
                var child = winner.GetClone();
                child.Mutate(parameters.MutationPower, ABSOLUTE_MUTATION_FACTOR);

                if (population.Units.Remove(selectionResults.Losers[i]))
                    population.Units.Add(child);
            }

            UpdateBestTeam(selectionResults.Winners);
        }
    }
}
