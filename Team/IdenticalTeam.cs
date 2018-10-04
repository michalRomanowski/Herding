using System.Linq;
using Agent;
using System.IO;

namespace Teams
{
    public class IdenticalTeam : Team
    {
        public IdenticalTeam() : base() { }

        public override Team GetClone()
        {
            var clone = new IdenticalTeam();

            foreach (var a in Members)
            {
                clone.Members.Add(a.GetClone());
            }

            return clone;
        }

        public override void AdjustSize(int newSize)
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

        public override Team[] Crossover(Team partner)
        {
            Team[] children = new Team[2] { new IdenticalTeam(), new IdenticalTeam() };

            var childrenAgents = Members[0].Crossover(partner.Members[0]).Select(x => x as ThinkingAgent).ToArray();

            children[0].Members.Add(childrenAgents[0]);
            children[1].Members.Add(childrenAgents[1]);

            for (int i = 1; i < Members.Count; i++)
            {
                children[0].Members.Add(childrenAgents[0].GetClone());
                children[1].Members.Add(childrenAgents[1].GetClone());
            }

            return children;
        }

        public override void Mutate(float mutationPower, float absoluteMutationFactor)
        {
            Members[0].Mutate(mutationPower, absoluteMutationFactor);

            for (int i = 1; i < Members.Count; i++)
                Members[i] = Members[0].GetClone();
        }
    }
}