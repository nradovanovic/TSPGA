using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPGA
{
    class PartiallyMappedCrossoverOperator : ICrossoverOperator
    {
        private Random random = new Random();

        public Tuple<Chromosome, Chromosome> Crossover(Chromosome parent1, Chromosome parent2, int length)
        {
            int index1 = random.Next(0, length-1);
            int index2 = random.Next(index1, length);
            List<City> offspring1 = new List<City>(length);
            List<City> offspring2 = new List<City>(length);
            for (int i = 0; i < length; i++)
            {
                offspring1.Add(new City());
                offspring2.Add(new City());
            }
            List<City> parentTour1 = parent1.getTour();
            List<City> parentTour2 = parent2.getTour();

            for (int i=index1; i<=index2; i++)
            {
                offspring1[i] = parentTour2[i];
                offspring2[i] = parentTour1[i];
            }
            List<int> unfilled1 = new List<int>();
            List<int> unfilled2 = new List<int>();
            List<int> available1 = new List<int>();
            List<int> available2 = new List<int>();
            for (int i=0; i<length; i++)
            {
                if (i < index1 || i > index2)
                {
                    if (!offspring1.Contains(parentTour1[i]))
                        offspring1[i] = parentTour1[i];
                    else
                        unfilled1.Add(i);
                    if (!offspring2.Contains(parentTour2[i]))
                        offspring2[i] = parentTour2[i];
                    else
                        unfilled2.Add(i);
                }
            }
            for (int i=0; i<unfilled1.Count; i++)
            {
                offspring1[unfilled1[i]] = parentTour2[unfilled2[i]];
            }
            for (int i = 0; i < unfilled2.Count; i++)
            {
                offspring2[unfilled2[i]] = parentTour1[unfilled1[i]];
            }
            Chromosome child1 = new Chromosome();
            child1.setTour(offspring1);
            Chromosome child2 = new Chromosome();
            child2.setTour(offspring2);
            return new Tuple<Chromosome, Chromosome>(child1, child2);
        }
    }
}
