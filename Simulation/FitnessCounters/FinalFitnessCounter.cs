using Agent;
using Auxiliary;
using System.Collections.Generic;
using System.Linq;
using Teams;
using World;

namespace Simulations
{
    public class FinalFitnessCounter : FitnessCounter
    {
        public FinalFitnessCounter(CountFitnessParameters parameters) : base(parameters) { }

        protected override double CountFitness(Team team, IList<IMovingAgent> sheep)
        {
            var world = new SimulationWorld(team, sheep, false);
            world.Work(parameters.TurnsOfHerding);
            var fitness = world.Sheep.Select(x => x.Position).SumOfDistancesFromCenter();
            return fitness;
        }
    }
}