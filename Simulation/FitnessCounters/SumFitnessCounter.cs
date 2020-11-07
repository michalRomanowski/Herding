using Agent;
using Auxiliary;
using System.Collections.Generic;
using System.Linq;
using Teams;
using World;

namespace Simulations
{
    public class SumFitnessCounter : FitnessCounter
    {
        public SumFitnessCounter(CountFitnessParameters parameters) : base(parameters) { }

        protected override double CountFitness(Team team, IList<IMovingAgent> sheep)
        {   
            var world = new SimulationWorld(team, sheep, true);
            world.Work(parameters.TurnsOfHerding);
            var fitness = world.Sheep.Select(x => x.Path).Sum(x => x.SumOfDistancesFromCenter());
            return fitness;
        }
    }
}