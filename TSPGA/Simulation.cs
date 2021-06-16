using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPGA
{
    class Simulation
    {
        public int numSim { get; set; }
        public string fileName { get; set; }
        public string optimalFileName { get; set; }
        List<IMutationOperator> mutations;
        List<ICrossoverOperator> crossovers;
        List<double> mp;
        List<double> cp;
        public int maxGen { get; set; }
        public int populationSize { get; set; }

        public Simulation(int ns, string f, string of,  List<IMutationOperator> mut, List<ICrossoverOperator> crs, List<double> m, List<double> c, int g, int ps)
        {
            numSim = ns;
            fileName = f;
            optimalFileName = of;
            mutations = mut;
            crossovers = crs;
            mp = m;
            cp = c;
            maxGen = g;
            populationSize = ps;
        }

        public void runSimulation()
        {
            List<City> cities = new List<City>();
            using (var sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] lineSplit = line.Split();
                    City city = new City();
                    city.Num = int.Parse(lineSplit[0]);
                    city.X = double.Parse(lineSplit[1]);
                    city.Y = double.Parse(lineSplit[2]);
                    cities.Add(city);
                }
            }

            List<City> optimal = new List<City>();
            using (var sr = new StreamReader(optimalFileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    int index = int.Parse(line);
                    optimal.Add(cities[index - 1]);
                }
            }
            Chromosome best = new Chromosome();
            best.setTour(optimal);
            Console.WriteLine(best.fitnessValue);

            GAutils utils = new GAutils();

            List<Tuple<string, double>> bestDist = new List<Tuple<string, double>>();
            List<Tuple<string, double>> avgDist = new List<Tuple<string, double>>();
            List<Tuple<string, double>> worstDist = new List<Tuple<string, double>>();

            Random rand = new Random();
            foreach (ICrossoverOperator co in crossovers)
            {
                foreach (IMutationOperator mo in mutations)
                {
                    foreach (double crossProb in cp)
                    {
                        foreach (double mutProb in mp)
                        {
                            string s = "Crossover operator: " + co.GetType().ToString().Split('.')[1] + ", Mutation operator: " + mo.GetType().ToString().Split('.')[1] + ", Crossover probability: " + crossProb + ", mutation Probability: " + mutProb + " , Population size: " + populationSize;
                            utils.crossoverOperator = co;
                            utils.mutationOperator = mo;
                            utils.CrossoverProbability = crossProb;
                            utils.MutationProbability = mutProb;
                            List<double> results = new List<double>();
                            for (int sim = 0; sim < numSim; sim++)
                            {
                                int gen = 0;
                                List<Chromosome> pop = utils.Intitialization(cities, populationSize);
                                while (gen < maxGen)
                                {

                                    List<Chromosome> pool = utils.Selection(pop, populationSize);
                                    List<Chromosome> newPop = new List<Chromosome>();
                                    newPop.AddRange(pop.OrderBy(x => x.fitnessValue).ToList().Take(20));
                                    pop = new List<Chromosome>();
                                    for (int i = 0; i < populationSize/2 ; i++)
                                    {
                                        int ind1 = rand.Next(0, populationSize/2);
                                        int ind2 = rand.Next(0, populationSize/2);
                                        Tuple<Chromosome, Chromosome> children = utils.Crossover(pool[ind1], pool[ind2], cities.Count);
                                        newPop.Add(children.Item1);
                                        newPop.Add(children.Item2);
                                    }
                                    for (int i = 0; i < populationSize/2 ; i++)
                                    {
                                        int ind1 = rand.Next(10, 10 + populationSize);
                                        Chromosome child = utils.Mutation(newPop[ind1], cities.Count);
                                        newPop.RemoveAt(ind1);
                                        newPop.Add(child);
                                    }
                                    gen++;
                                    pop = newPop;
                                    if (gen == maxGen-1)
                                    {
                                        Console.WriteLine("done");
                                        results.Add(newPop.Min(x => x.fitnessValue));
                                    }
                                }
                            }
                            bestDist.Add(new Tuple<string, double>(s, results.Min()));
                            avgDist.Add(new Tuple<string, double>(s, results.Sum() / results.Count));
                            worstDist.Add(new Tuple<string, double>(s, results.Max()));
                        }
                    }
                }

            }
            Console.WriteLine("Simulation done, writing results to file");
            using (StreamWriter sw = new StreamWriter("results.txt"))
            {
                sw.WriteLine("Simulation ran " + numSim + " times with maximum generations " + maxGen);
                sw.WriteLine("Optimal length is: " + best.fitnessValue);
                sw.WriteLine("----------------------------------------------------");
                for (int i = 0; i < bestDist.Count; i++)
                {
                    sw.WriteLine(bestDist[i].Item1);
                    sw.WriteLine("Best: " + bestDist[i].Item2 + " Average: " + avgDist[i].Item2 + " Worst: " + worstDist[i].Item2);
                    sw.WriteLine("----------------------------------------------------");
                }

            }
        }
    }
}
