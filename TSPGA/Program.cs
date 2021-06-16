using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPGA
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ICrossoverOperator> crossovers = new List<ICrossoverOperator>() { new PartiallyMappedCrossoverOperator(), new OrderCrossoverOperator() };
            List<IMutationOperator> mutations = new List<IMutationOperator> { new CentreInverseMutationOperator(), new SwapMutationOperator(), new ReverseSequenceMutationOperator() };
            List<double> cp = new List<double> { 0.7, 0.8, 0.9 };
            List<double> mp = new List<double> { 0.1, 0.2, 0.3 };
            int gens = 200;
            int sims = 10;
            int popsize = 100;
            Simulation simulation = new Simulation(sims, "berlin52.txt", "berlin52.opt.txt", mutations, crossovers, mp, cp, gens, popsize);
            simulation.runSimulation();
        }
    }
}
