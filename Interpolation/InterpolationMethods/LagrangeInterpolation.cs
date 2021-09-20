using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpolation
{
    class LagrangeInterpolation : IInterpolator
    {

        private static double GetLagrangeCoefficient (ValuesTable table, double x, int k)
        {
            if (table is null)
            {
                throw new ArgumentNullException(nameof(table), "Table is null");
            }

            if (x > table.Points[^1].X)
            {
                throw new ArgumentOutOfRangeException(nameof(x), "Finding value is out of range of table.");
            }

            double coefficient = 1;

            for (int i = 0; i < table.Points.Count; i++)
            {
                if (i == k)
                {
                    continue;
                }
                coefficient *= (x - table.Points[i].X)/ (table.Points[k].X - table.Points[i].X);
            }

            return coefficient;
        }

        public double GetValue(ValuesTable table, double x)
        {
            if (table is null)
            {
                throw new ArgumentNullException(nameof(table), "Table is null");
            }

            if (x > table.Points[^1].X)
            {
                throw new ArgumentOutOfRangeException(nameof(x), "Finding value is out of range of table.");
            }

            double value = 0;

            for (int i = 0; i < table.Points.Count; i++)
            {
                value += GetLagrangeCoefficient(table, x, i) * table.Points[i].Y;
            }

            return value;
        }
    }
}
