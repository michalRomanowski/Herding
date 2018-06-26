using System.Collections.Generic;
using Agent;
using Auxiliary;
using System.IO;

namespace Team
{
    public interface ITeam
    {
        IList<IThinkingAgent> Members { get; }
        float Fitness { get; set; }
        void ResetFitness();
        ITeam Clone();
        void AdjustSize(int newSize);
        void SetPositions(IList<Position> positions);
        void ClearPath();
        ITeam[] Crossover(ITeam partner);
        void Mutate(float mutationPower, float absoluteMutationFactor);

        void AdjustInputLayerSize(int numberOfSeenShepards, int numberOfSeenSheep);
        void AdjustHiddenLayersSize(int newSize);

        string Compress();
    }
}
