using Agent;
using System.Linq;

namespace Teams
{
    public class IdenticalTeam : Team
    {
        public IdenticalTeam() : base() { }

        public IdenticalTeam(ITeamParameters parameters) : base()
        {
            if (parameters.NumberOfShepherds == 0)
                return;

            Members.Add(AgentFactory.GetShepherd(parameters));
            Resize(parameters.NumberOfShepherds);
        }

        public override void Init(Team other)
        {
            Members = other.Members.Select(x => other.Members.First().GetClone()).ToList();
        }

        public override Team GetClone()
        {
            return new IdenticalTeam()
            {
                Members = Members.Select(x => x.GetClone()).ToList()
            };
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

        public override void Mutate(double mutationPower)
        {
            Members[0].Mutate(mutationPower);

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