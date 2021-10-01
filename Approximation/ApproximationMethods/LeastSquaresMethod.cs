using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace Approximation.ApproximationMethods
{
    static class LeastSquaresMethod
    {
        static public double[] ApproximateTableWithPolynomial(ValuesTable table, int polynomialDegree)
        {
            if (table is null)
            {
                throw new ArgumentNullException(nameof(table), "Table is null");
            }

            if (polynomialDegree <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(polynomialDegree), "Degree of polynomial must be positive.");
            }

            var (matrixStorage, vectorStorage) = EvaluateLinearSystemData(table, polynomialDegree);

            var A = Matrix<double>.Build.DenseOfArray(matrixStorage);
            var b = Vector<double>.Build.Dense(vectorStorage);
            var x = A.Solve(b);

            return x.ToArray();
        }

        private static (double[,] matrixStorage, double[] vectorStorage) EvaluateLinearSystemData(ValuesTable table, int polynomialDegree)
        {
            var matrixDegree = polynomialDegree + 1;
            var matrixStorage = new double[matrixDegree, matrixDegree];
            var vectorStorage = new double[matrixDegree];

            for (int i = 0; i < matrixDegree; i++)
            {
                for (int j = 0; j < matrixDegree; j++)
                {
                    for (int k = 0; k < table.Points.Count; k++)
                    {
                        matrixStorage[i, j] += Math.Pow(table.Points[k].X, polynomialDegree * 2 - i - j);
                    }
                }

                for (int k = 0; k < table.Points.Count; k++)
                {
                    vectorStorage[i] += Math.Pow(table.Points[k].X, polynomialDegree - i) * table.Points[k].Y;
                }
            }

            return (matrixStorage, vectorStorage);
        }
    }
}
