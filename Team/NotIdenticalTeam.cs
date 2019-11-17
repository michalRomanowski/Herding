using Auxiliary;
using System.Linq;

namespace Teams
{
    public class NotIdenticalTeam : Team
    {
        public NotIdenticalTeam() : base(){ }

        public NotIdenticalTeam(ITeamParameters parameters) : base(parameters){ }

        private const double MIX_MEMBERS_CHANCE = 0.5;

        public override Team GetClone()
        {
            var clone = new NotIdenticalTeam();

            foreach (var a in Members)
            {
                clone.Members.Add(a.GetClone());
            }

            return clone;
        }
        
        public override Team Crossover(Team partner)
        {
            return CRandom.Instance.NextDouble() < MIX_MEMBERS_CHANCE ? MixMembers(partner) : CrossoverMembers(partner);
        }

        private Team MixMembers(Team partner)
        {
            var children = new NotIdenticalTeam();

            for (int i = 0; i < Members.Count; i++)
            {
                if(CRandom.Instance.Next(2) == 0)
                {
                    children.Members.Add(Members[CRandom.Instance.Next(Members.Count)].GetClone());
                }
                else
                {
                    children.Members.Add(partner.Members[CRandom.Instance.Next(partner.Members.Count)].GetClone());
                }
            }

            return children;
        }

        private Team CrossoverMembers(Team partner)
        {
            var children = new NotIdenticalTeam();
            
            for (int i = 0; i < Members.Count; i++)
            {
                children.Members.Add(Members[i].Crossover(partner.Members[i]));
            }

            return children;
        }

        public override void Mutate(double mutationPower, double absoluteMutationFactor)
        {
            Members[CRandom.Instance.Next(Members.Count)].Mutate(mutationPower, absoluteMutationFactor);
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
                Members.Last().Mutate(1.0f, 1.0f);
            }
        }
        
        public override void ResizeNeuralNet(int numberOfSeenShepherds, int numberOfSeenSheep, int numberOfHiddenLayers, int hiddenLayerSize)
        {
            foreach (var agent in Members)
            {
                agent.ResizeNeuralNet(numberOfSeenShepherds, numberOfSeenSheep, numberOfHiddenLayers, hiddenLayerSize);
            }
        }
    }
}
