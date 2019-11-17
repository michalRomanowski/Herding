using Teams;

namespace Simulations
{
    public interface IPopulationParameters : ITeamParameters
    {
        int PopulationSize { get; }
    }
}
