using System;
using System.Collections.Generic;
using ValuesTable = System.Collections.Generic.List<System.Collections.Generic.List<double>>;

namespace Interpolation
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
            
            interpolator = new LagrangeInterpolation();
            foreach (var point in pointsToFind)
            {
                Console.WriteLine("x: {0}, y: {1}", point, interpolator.GetValue(builder.BuildTableForInterpolation(point,degree), point));
            }
            Console.WriteLine();

            interpolator = new NewtonInterpolation();
            degree = 2;
            foreach (var point in pointsToFind)
            {
                Console.WriteLine("x: {0}, y: {1}", point, interpolator.GetValue(builder.BuildTableForInterpolation(point, degree), point));
            }
            Console.WriteLine();
        }

    }
}
