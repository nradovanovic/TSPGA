using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPGA
{
    interface IMutationOperator
    {
        Chromosome Mutation(Chromosome parent, int length);
    }
}
