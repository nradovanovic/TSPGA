using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPGA
{
    class Chromosome
    {
        List<City> tourOrder;
        public double fitnessValue { get; set; }
        public double goodness { get; set; }

        public List<City> getTour()
        {
            return tourOrder;
        }

        public void setTour(List<City> tour)
        {
            tourOrder = new List<City>();
            foreach (City c in tour)
                tourOrder.Add(c);
            calculateFitness();
        }

        private void calculateFitness()
        {
            double distance = 0;
            for (int i = 0; i < tourOrder.Count - 1; i++)
            {
                distance += tourOrder[i].Distance(tourOrder[i + 1]);
            }
            distance += tourOrder[0].Distance(tourOrder[tourOrder.Count - 1]);
            fitnessValue = distance;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            foreach (City c in tourOrder)
                s.Append(c.Num + " ");
            s.Append(fitnessValue);
            return s.ToString();
        }
    }
}
