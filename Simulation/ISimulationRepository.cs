using System.Collections.Generic;

namespace Simulations
{
    public interface ISimulationRepository
    {
        void Save(string name, OptimizationParameters parameters, Population population);
        void Save(string name, OptimizationParameters parameters);
        void Save(string name, Population population);

        OptimizationParameters LoadOptimizationParameters(string name);
        Population LoadPopulation(string name, IPopulationParameters parameters);

        IEnumerable<string> GetSimulations();
    }
}
