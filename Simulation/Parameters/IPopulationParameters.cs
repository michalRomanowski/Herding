using Teams;

namespace Simulations.Parameters
{
    public interface IPopulationParameters : ITeamParameters
    {
        int PopulationSize { get; }
    }
}