using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Approximation.InterpolationMethods
{
    class InterpolationSpline : IInterpolator
    {
        private List<double> A;

        private List<double> B;

        private List<double> C;

        private List<double> D;

        public InterpolationSpline(List<double> A, List<double> B, List<double> C, List<double> D)
        {
            this.A = A;
            this.B = B;
            this.C = C;
            this.D = D;
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

            int index = 1;

            for (; x > table.Points[index].X; index++)
            {
            }

            var xDifference = (x - table.Points[index].X);

            return A[index]
                + B[index - 1] * xDifference
                + C[index] * xDifference * xDifference / 2
                + D[index - 1] * xDifference * xDifference * xDifference / 6;
        }

        public override string ToString()
        {
            var stringValue = new StringBuilder(string.Empty);

            for (int i = 0; i < B.Count; i++)
            {
                stringValue.AppendLine(string.Format($"a[{i + 1}] = {A[i + 1],7:###.###}, b[{i + 1}] = {B[i],7:###.###}, c[{i + 1}] = {C[i + 1],7:###.###}, d[{i + 1}] = {D[i],7:###.###}"));
            }

            return stringValue.ToString();
        }
    }
}
