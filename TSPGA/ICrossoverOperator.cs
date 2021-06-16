using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPGA
{
    interface ICrossoverOperator
    {
        Tuple<Chromosome, Chromosome> Crossover(Chromosome parent1, Chromosome parent2, int length);
    }
}
