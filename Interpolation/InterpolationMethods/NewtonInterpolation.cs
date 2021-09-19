using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuesTable = System.Collections.Generic.List<System.Collections.Generic.List<double>>;

namespace Interpolation
{
    class NewtonInterpolation : IInterpolator
    {
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

            var dividedDifferences = table[1].ToArray();

            double value = table[1][0];
            int size = table[0].Count;
            double P = 1;

            for (int k = 1; k < size; k++)
            {
                P *= x - table[0][k - 1];
                for (int i = 0; i < (size - k); i++)
                {
                    dividedDifferences[i] = (dividedDifferences[i + 1] - dividedDifferences[i]) / (table[0][i + k] - table[0][i]);
                }
                value += P * dividedDifferences[0];
            }
            return value;
        }
    }
}
