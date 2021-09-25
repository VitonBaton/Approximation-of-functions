using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpolation.InterpolationMethods
{
    class NewtonInterpolation : IInterpolator
    {
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

            var dividedDifferences = (from point in table.Points
                                     select point.Y)
                                     .ToList();

            double value = table.Points[0].Y;
            int size = table.Points.Count;
            double P = 1;

            for (int k = 1; k < size; k++)
            {
                P *= x - table.Points[k - 1].X;
                for (int i = 0; i < (size - k); i++)
                {
                    dividedDifferences[i] = (dividedDifferences[i + 1] - dividedDifferences[i]) / (table.Points[i + k].X - table.Points[i].X);
                }
                value += P * dividedDifferences[0];
            }
            return value;
        }
    }
}