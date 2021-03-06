﻿using Teams;

namespace Simulations
{
    public interface IFitnessCounter
    {
        double CountFitness(Team team, int seed, bool verbose = false);
    }
}