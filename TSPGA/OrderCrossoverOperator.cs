using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPGA
{
    class OrderCrossoverOperator : ICrossoverOperator
    {
        Random random = new Random();

        public Tuple<Chromosome, Chromosome> Crossover(Chromosome parent1, Chromosome parent2, int length)
        {
            //[1, L-2]
            int index1 = random.Next(0, length - 1);
            //[index1, L-1]
            int index2 = random.Next(index1, length);
            //int index1 = 3;
            //int index2 = 5;
            List<City> offspring1 = new List<City>(length);
            List<City> offspring2 = new List<City>(length);
            for (int i = 0; i < length; i++)
            {
                offspring1.Add(new City());
                offspring2.Add(new City());
            }
            List<City> parentTour1 = parent1.getTour();
            List<City> parentTour2 = parent2.getTour();
  
            for (int i = index1; i <= index2; i++)
            {
                offspring1[i] = parentTour1[i];
                offspring2[i] = parentTour2[i];
            }
            int c1 = index2 + 1;
            int c2 = index2 + 1;
            for (int i = index2 + 1; i < length; i++)
            {
                if (!offspring1.Contains(parentTour2[i]))
                    offspring1[c1++] = parentTour2[i];
                if (!offspring2.Contains(parentTour1[i]))
                    offspring2[c2++] = parentTour1[i];
            }
            if (c1 >= length) c1 %= length;
            if (c2 >= length) c2 %= length;
            for (int i = 0; i <= index2; i++)
            {
                if (!offspring1.Contains(parentTour2[i]))
                    offspring1[c1++] = parentTour2[i];
                if (!offspring2.Contains(parentTour1[i]))
                    offspring2[c2++] = parentTour1[i];
                if (c1 >= length) c1 %= length;
                if (c2 >= length) c2 %= length;
            }
       
            Chromosome child1 = new Chromosome();
            child1.setTour(offspring1);
            Chromosome child2 = new Chromosome();
            child2.setTour(offspring2);
            return new Tuple<Chromosome, Chromosome>(child1, child2);
        }
    }
}
