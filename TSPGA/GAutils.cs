using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPGA
{
    class GAutils
    {  
        public ICrossoverOperator crossoverOperator { get; set; }
        public IMutationOperator mutationOperator { get; set; }
        public double CrossoverProbability { get; set; }
        public double MutationProbability { get; set; }
        Random generator = new Random();

        private static List<City> Shuffle(List<City> list)
        {
            List<City> cities = list;
            Random rand = new Random();
            int n = cities.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                City tmp = cities[k];
                cities[k] = cities[n];
                cities[n] = tmp;
            }
            return cities;
        }

        public List<Chromosome> Intitialization(List<City> cities, int numPopulation)
        {
            List<Chromosome> population = new List<Chromosome>();
            for (int i = 0; i < numPopulation; i++)
            {
                List<City> randomTour = new List<City>();
                randomTour = Shuffle(cities);
                Chromosome c = new Chromosome();
                c.setTour(randomTour);
                population.Add(c);
            }
            return population;
        }

        public List<Chromosome> Selection(List<Chromosome> oldPopulation, int length)
        {
            
            List<Chromosome> newPop = new List<Chromosome>();
            double maxF = oldPopulation.Max(x => x.fitnessValue);
            double sumF = oldPopulation.Sum(x => x.fitnessValue);
            oldPopulation.ForEach(x => x.goodness = 1 / x.fitnessValue);
            double goodnessSum = oldPopulation.Sum(x => x.goodness);
            List<double> prob = new List<double>();
            foreach (Chromosome c in oldPopulation)
                prob.Add(c.goodness / goodnessSum);
            for (int i = 1; i < prob.Count; i++)
                prob[i] = prob[i - 1] + prob[i];
            List<double> rands = Enumerable.Range(0, length).Select(x => generator.NextDouble()).OrderBy(x => x).ToList();
            
            int fitin = 0;
            int newin = 0;
            while (newin < length)
            {
                if (rands[newin] < prob[fitin])
                {
                    newPop.Add(oldPopulation[fitin]);
                    newin++;
                }
                else
                    fitin++;
            }
            return newPop;
        }

        public Tuple<Chromosome, Chromosome> Crossover(Chromosome parent1, Chromosome parent2, int length)
        {
            if (generator.NextDouble() < CrossoverProbability)
                return crossoverOperator.Crossover(parent1, parent2, length);
            else
                return new Tuple<Chromosome, Chromosome>(parent1, parent2);
        }

        public Chromosome Mutation(Chromosome parent, int length)
        {
            if (generator.NextDouble() < MutationProbability)
                return mutationOperator.Mutation(parent, length);
            else
                return parent;
        }

    }
}
