using Simulations.Parameters;
using System.Linq;
using Teams;

namespace Simulations.OptimizationStep
{
    class SimplifiedOptimizationStep : OptimizationStep
    {
        public SimplifiedOptimizationStep(
            OptimizationParameters parameters,
            Population population) : base(parameters, population){}

        public override void Step(int stepNumber)
        {
            var selectionResults = new Selection(parameters, population, 1).Select();

            var winner = selectionResults.Winners.First();

            for(int i = 0; i < selectionResults.Losers.Count; i++)
            {
                var child = winner.GetClone();
                
                Mutate(child, stepNumber);
                
                if (population.Units.Remove(selectionResults.Losers[i]))
                    population.Units.Add(child);
            }

            UpdateBestTeam(selectionResults.Winners);
        }
    }
}