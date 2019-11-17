using System.Linq;
using Agent;
using System.IO;

namespace Teams
{
    public class IdenticalTeam : Team
    {
        public IdenticalTeam() : base() { }

        public IdenticalTeam(ITeamParameters parameters) : base(parameters) { }

        public override Team GetClone()
        {
            var clone = new IdenticalTeam();

            foreach (var a in Members)
            {
                clone.Members.Add(a.GetClone());
            }

            return clone;
        }
        
        public override Team Crossover(Team partner)
        {
            var children = new IdenticalTeam();

            children.Members.Add(Members[0].Crossover(partner.Members[0]));

            for (int i = 1; i < Members.Count; i++)
            {
                children.Members.Add(children.Members.First().GetClone());
            }

            return children;
        }

        public override void Mutate(double mutationPower, double absoluteMutationFactor)
        {
            Members[0].Mutate(mutationPower, absoluteMutationFactor);

            FillWithClonesOfFirst();
        }

        public override void Resize(int newSize)
        {
            while (Members.Count > newSize)
            {
                Members.Remove(Members.Last());
            }

            while (Members.Count < newSize)
            {
                Members.Add(Members.First().GetClone());
            }
        }
        
        public override void ResizeNeuralNet(int numberOfSeenShepherds, int numberOfSeenSheep, int numberOfHiddenLayers, int hiddenLayerSize)
        {
            Members[0].ResizeNeuralNet(numberOfSeenShepherds, numberOfSeenSheep, numberOfHiddenLayers, hiddenLayerSize);

            FillWithClonesOfFirst();
        }

        private void FillWithClonesOfFirst()
        {
            for (int i = 1; i < Members.Count; i++)
                Members[i] = Members[0].GetClone();
        }
    }
}