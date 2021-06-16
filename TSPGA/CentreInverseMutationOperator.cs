using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPGA
{
    class CentreInverseMutationOperator : IMutationOperator
    {
        Random random = new Random();

        public Chromosome Mutation(Chromosome parent, int length)
        {
            List<City> old = parent.getTour();
            int index = random.Next(0, length);
            List<City> newTour = new List<City>();
            for (int i = index; i >= 0; i--)
            {
                newTour.Add(old[i]);
            }

            for (int i = length - 1; i > index; i--)
            {
                newTour.Add(old[i]);
            }


            Chromosome child = new Chromosome();
            child.setTour(newTour);
            return child;
        }
    }
}
