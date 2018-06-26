using System;
using System.Linq;
using Agent;
using System.IO;

namespace Team
{
    public class NotIdenticalTeam : Team
    {
        public NotIdenticalTeam() : base(){}

        public NotIdenticalTeam(string compressed) : base()
        {
            var sr = new StringReader(compressed);

            while(sr.Peek() > 0)
            {
                Members.Add(new Shepard(sr));
            }
        }

        public override ITeam Clone()
        {
            var clone = new NotIdenticalTeam();

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
                Members.Last().Mutate(1.0f, 1.0f);
            }
        }

        public override ITeam[] Crossover(ITeam partner)
        {
            var children = new NotIdenticalTeam[2] { new NotIdenticalTeam(), new NotIdenticalTeam() };
            
            for (int i = 0; i < Members.Count; i++)
            {
                var childrenAgents = Members[i].Crossover(partner.Members[i]);

                children[0].Members.Add(childrenAgents[0].Clone());
                children[1].Members.Add(childrenAgents[1].Clone());
            }

            return children;
        }

        public override void Mutate(float mutationPower, float absoluteMutationFactor)
        {
            foreach (var agent in Members)
            {
                agent.Mutate(mutationPower, absoluteMutationFactor);
            }
        }
    }
}
