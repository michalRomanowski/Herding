using System.Collections.Generic;

namespace Simulation
{
    public class Populations
    {
        public Population Shepards;
        public Population Wolfs;

        public Populations(SimulationParameters simulationParameters)
        {
            Shepards = new Population(simulationParameters);
            Wolfs = new Population(simulationParameters);
        }
    }
}
