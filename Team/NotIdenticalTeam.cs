using System;
using System.Linq;
using Agent;
using System.IO;

namespace Teams
{
    public class NotIdenticalTeam : Team
    {
        public NotIdenticalTeam() : base(){}

        public override Team GetClone()
        {
            var clone = new NotIdenticalTeam();

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
                Members.Last().Mutate(1.0f, 1.0f);
            }
        }

        public override Team[] Crossover(Team partner)
        {
            var children = new NotIdenticalTeam[2] { new NotIdenticalTeam(), new NotIdenticalTeam() };
            
            for (int i = 0; i < Members.Count; i++)
            {
                var childrenAgents = Members[i].Crossover(partner.Members[i]);

                children[0].Members.Add(childrenAgents[0].GetClone());
                children[1].Members.Add(childrenAgents[1].GetClone());
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
