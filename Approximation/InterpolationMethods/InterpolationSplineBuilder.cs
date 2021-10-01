using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Approximation.InterpolationMethods
{
    class InterpolationSplineBuilder
    {
        public ValuesTable Table { get; set; }

        public InterpolationSpline BuildSpline()
        {
            if (Table is null)
            {
                throw new ArgumentNullException(nameof(Table), "Table is null");
            }

            var H = FindCoefficientsH(Table);

            var A = (from point in Table.Points
                     select point.Y).ToList();

            var C = FindCoefficientsC(H, A);
            var D = FindCoefficientsD(H, C);
            var B = FindCoefficientsB(H, A, C, D);

            return new InterpolationSpline(A.ToList(), B, C.ToList(), D);
        }

        private static List<double> FindCoefficientsB(List<double> H, List<double> A, List<double> C, List<double> D)
        {
            var B = new List<double>();

            for (int i = 0; i < H.Count; i++)
            {
                B.Add(C[i + 1] * H[i] / 2 - D[i] * H[i] * H[i] / 6 + (A[i + 1] - A[i]) / H[i]);
            }

            return B;
        }

        private static List<double> FindCoefficientsD(List<double> H, List<double> C)
        {
            var D = new List<double>();

            for (int i = 0; i < H.Count; i++)
            {
                D.Add((C[i + 1] - C[i]) / H[i]);
            }

            return D;
        }

        private List<double> FindCoefficientsC(List<double> H, List<double> A)
        {
            var C = new List<double>();
            C.Add(0);
            C.AddRange(
                RunThrough(H.ToArray(),
                           FindCoefficientsBForRunThrough(H),
                           H.Skip(1).ToArray(),
                           FindCoefficientsYForRunThrough(H.ToList(), A.ToList())
                           )
                );
            C.Add(0);
            return C;
        }

        private List<double> FindCoefficientsH(ValuesTable table)
        {
            List<double> result = new List<double>();

            for (int i = 0; i < table.Points.Count - 1; i++)
            {
                result.Add(table.Points[i + 1].X - table.Points[i].X);
            }

            return result;
        }

        private double[] FindCoefficientsBForRunThrough(List<double> H)
        {
            int size = H.Count - 1;
            double[] result = new double[size];

            for (int i = 1; i <= size; i++)
            {
                result[i - 1] = 2 * (H[i] + H[i - 1]);
            }

            return result;
        }

        private double[] FindCoefficientsYForRunThrough(List<double> H, List<double> Y)
        {
            int size = H.Count - 1;
            double[] result = new double[size];

            for (int i = 1; i <= size; i++)
            {
                result[i - 1] = 6 * ((Y[i + 1] - Y[i]) / H[i] - (Y[i] - Y[i - 1]) / H[i - 1]);
            }

            return result;
        }

        private double[] RunThrough(double[] a, double[] b, double[] c, double[] y)
        {
            int size = b.Length;
            double[] A = new double[size];
            double[] B = new double[size];

            A[0] = -c[0] / b[0];
            B[0] = y[0] / b[0];


            for (int i = 1; i < size - 1; i++)
            {
                A[i] = -c[i] / (b[i] + a[i] * A[i - 1]);
                B[i] = (y[i] - a[i] * B[i - 1]) / (b[i] + a[i] * A[i - 1]);
            }

            double[] x = new double[size];
            x[^1] = (y[^1] - a[^1] * B[^2]) / (b[^1] + a[^1] * A[^2]);
            for (int i = size - 2; i >= 0; i--)
            {
                x[i] = A[i] * x[i + 1] + B[i];
            }
            return x;
        }
    }
}
