using System.Linq;
using Agent;
using System.IO;

namespace Team
{
    public class IdenticalTeam : Team
    {
        public IdenticalTeam() : base() { }

        public IdenticalTeam(int membersInTeam, string compressed) : base()
        {
            var sr = new StringReader(compressed);

            Members.Add(new Shepard(sr));

            for(int i = 1; i < membersInTeam; i++)
                Members.Add(Members.First().Clone());
        }

        public override ITeam Clone()
        {
            var clone = new IdenticalTeam();

            foreach (var a in Members)
            {
                clone.Members.Add(a.Clone());
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
                Members.Add(Members.First().Clone());
            }
        }

        public override ITeam[] Crossover(ITeam partner)
        {
            ITeam[] children = new ITeam[2] { new IdenticalTeam(), new IdenticalTeam() };

            var childrenAgents = Members[0].Crossover(partner.Members[0]).Select(x => x as IThinkingAgent).ToArray();

            children[0].Members.Add(childrenAgents[0]);
            children[1].Members.Add(childrenAgents[1]);

            for (int i = 1; i < Members.Count; i++)
            {
                children[0].Members.Add(childrenAgents[0].Clone());
                children[1].Members.Add(childrenAgents[1].Clone());
            }

            return children;
        }

        public override void Mutate(float mutationPower, float absoluteMutationFactor)
        {
            Members[0].Mutate(mutationPower, absoluteMutationFactor);

            for (int i = 1; i < Members.Count; i++)
                Members[i] = Members[0].Clone();
        }
    }
}