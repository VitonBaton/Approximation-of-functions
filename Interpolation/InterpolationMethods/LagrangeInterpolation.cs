using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuesTable = System.Collections.Generic.List<System.Collections.Generic.List<double>>;

namespace Interpolation
{
    class LagrangeInterpolation : IInterpolator
    {

        private static double GetLagrangeCoefficient (List<double> valuesX, double x, int k)
        {
            double coefficient = 1;

            for (int i = 0; i < valuesX.Count; i++)
            {
                if (i == k)
                {
                    continue;
                }
                coefficient *= (x - valuesX[i])/ (valuesX[k] - valuesX[i]);
            }

            return coefficient;
        }

        public double GetValue(ValuesTable table, double x)
        {
            if (table is null)
            {
                throw new ArgumentNullException(nameof(table), "Table is null");
            }

            if (x > table[0][^1])
            {
                throw new ArgumentOutOfRangeException(nameof(x), "Finding value is out of range of table.");
            }

            double value = 0;

            for (int i = 0; i < table[0].Count; i++)
            {
                value += GetLagrangeCoefficient(table[0], x, i) * table[1][i];
            }

            return value;
        }
    }
}
