using System;
using System.Collections.Generic;
using ValuesTable = System.Collections.Generic.List<System.Collections.Generic.List<double>>;
using MathNet.Numerics.Differentiation;
using Approximation.InterpolationMethods;
using Approximation.ApproximationMethods;

namespace Approximation
{
    class Program
    {

        static void Main(string[] args)
        {
            var pointsToFind = new[] { 1.17, 1.34, 1.74 };
            var degree = 3;
            IInterpolator interpolator;

            var builder = new TableBuilder();
            builder.StartOfRange = 1;
            builder.EndOfRange = 2;
            builder.Increment = 0.2;
            builder.Function = (x) => x * Math.Pow(2, x) - 1;

            var table = builder.BuildTable();
            Console.WriteLine(table.ToString());

            Console.WriteLine("Lagrange Interpolation:");
            interpolator = new LagrangeInterpolation();
            foreach (var point in pointsToFind)
            {
                Console.WriteLine("x: {0,7:###.###}, y: {1,7:###.###}", point, interpolator.GetValue(builder.BuildTableForInterpolation(point, degree), point));
            }
            Console.WriteLine();

            Console.WriteLine("Newton Interpolation:");
            interpolator = new NewtonInterpolation();
            degree = 2;
            foreach (var point in pointsToFind)
            {
                Console.WriteLine("x: {0,7:###.###}, y: {1,7:###.###}", point, interpolator.GetValue(builder.BuildTableForInterpolation(point, degree), point));
            }
            Console.WriteLine();

            Console.WriteLine("Spline Interpolation:");
            var splineBuilder = new InterpolationSplineBuilder();
            splineBuilder.Table = table;
            interpolator = splineBuilder.BuildSpline();
            foreach (var point in pointsToFind)
            {
                Console.WriteLine("x: {0,7:###.###}, y: {1,7:###.###}", point, interpolator.GetValue(table, point));
            }
            Console.WriteLine();
            Console.WriteLine("Coefficients of spline:");
            Console.WriteLine(interpolator.ToString());


            Console.WriteLine("Least squares method:");

            table = new ValuesTable(new[] {0.0, 2, 4, 6, 8, 10},
                                    new[] {5, 1, 0.5, 1.5, 4.5, 8.5});

            for (int i = 1; i < 4; i++)
            {
                var coefficients = LeastSquaresMethod.ApproximateTableWithPolynomial(table, i);
                Console.Write($"Cofficients of {i+1}th degree polynomial: ");
                foreach (var coefficient in coefficients)
                {
                    Console.Write("{0:###.###}; ", coefficient);
                }
                Console.WriteLine();
            }
        }

    }
}
