using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulations
{
    interface IThinkingAgentParameters
    {
        float MutationPower { get; set; }
        float AbsoluteMutationFactor { get; set; }

        int NumberOfSeenShepherds { get; set; }
        int NumberOfSeenSheep { get; set; }

        int NumberOfHiddenLayers { get; set; }
        int NumberOfNeuronsInHiddenLayer { get; set; }
    }
}
