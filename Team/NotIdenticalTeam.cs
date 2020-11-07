using Agent;
using Auxiliary;
using System.Linq;

namespace Teams
{
    public class NotIdenticalTeam : Team
    {
        public NotIdenticalTeam() : base(){ }

        public NotIdenticalTeam(ITeamParameters parameters) : base()
        {
            for(int i = 0; i < parameters.NumberOfSeenShepherds; i++)
                Members.Add(AgentFactory.GetShepherd(parameters));
        }

        public override void Init(Team other)
        {
            Members = other.Members.Select(x => x.GetClone()).ToList();
        }

        public override Team GetClone()
        {
            return new NotIdenticalTeam
            {
                Members = Members.Select(x => x.GetClone()).ToList()
            };
        }
        
        public override Team Crossover(Team partner)
        {
            var children = new NotIdenticalTeam();

            for (int i = 0; i < Members.Count; i++)
            {
                children.Members.Add(Members[i].Crossover(partner.Members[i]));
            }

            return children;
        }

        public override void Mutate(double mutationPower)
        {
            Members[StaticRandom.R.Next(Members.Count)].Mutate(mutationPower);
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
            foreach (var agent in Members)
            {
                agent.ResizeNeuralNet(numberOfSeenShepherds, numberOfSeenSheep, numberOfHiddenLayers, hiddenLayerSize);
            }
        }
    }
}