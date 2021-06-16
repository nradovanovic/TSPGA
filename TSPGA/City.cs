using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPGA
{
    class City
    {
        public int Num { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public double Distance(City other)
        {
            return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }
    }
}
