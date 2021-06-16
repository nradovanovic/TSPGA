using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPGA
{
    class ReverseSequenceMutationOperator : IMutationOperator
    {
        Random random = new Random();

        public Chromosome Mutation(Chromosome parent, int length)
        {
            int index1 = random.Next(0, length - 1);
            int index2 = random.Next(index1, length);
            List<City> old = parent.getTour();
            List<City> newTour = new List<City>();
            for (int i = 0; i < index1; i++)
            {
                newTour.Add(old[i]);
            }
            for (int i = index2; i >= index1; i--)
            {
                newTour.Add(old[i]);
            }
            for (int i = index2 + 1; i < length; i++)
            {
                newTour.Add(old[i]);
            }

            Chromosome child = new Chromosome();
            child.setTour(newTour);
            return child;
        }
    }
}
