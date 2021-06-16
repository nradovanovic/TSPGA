using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPGA
{
    class SwapMutationOperator : IMutationOperator
    {
        Random random = new Random();

        public Chromosome Mutation(Chromosome parent, int length)
        {
            List<City> old = parent.getTour();
            int index1 = random.Next(0, length - 1);
            int index2 = random.Next(index1, length);
            City tmp = old[index1];
            old[index1] = old[index2];
            old[index2] = tmp;
            Chromosome child = new Chromosome();
            child.setTour(old);
            return child;
        }
    }
}
