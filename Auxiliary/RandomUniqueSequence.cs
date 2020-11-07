using System.Collections.Generic;

namespace Auxiliary
{
    public class RandomUniqueSequence
    {
        private HashSet<int> sequence;
        private readonly int max;

        public RandomUniqueSequence(int max)
        {
            sequence = new HashSet<int>();
            this.max = max;
        }

        public IEnumerable<int> GetSubSequence(int size)
        {
            var subSequence = new List<int>(size);

            for (int i = 0; i < size; i++)
            {
                var element = UniqueElement();
                sequence.Add(element);
                subSequence.Add(element);
            }

            return subSequence;
        }

        private int UniqueElement()
        {
            int element;

            do
            {
                element = StaticRandom.R.Next(max);

            } while (sequence.Contains(element));

            return element;
        }
    }
}
