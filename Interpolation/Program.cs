﻿using System;
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

        static List<List<double>> BuildTable(double startValue, double endValue, double shift, Func<double, double> func)
        {
            if (func is null)
            {
                throw new ArgumentNullException(nameof(func),"Function is null");
            }

            if (endValue < startValue)
            {
                throw new ArgumentException("End value is less than start.");
            }

            if (shift <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(shift),"Shift value must be positive.");
            }

            var table = new List<List<double>>();
            for (int i = 0; i < 2; i++)
            {
                table.Add(new List<double>());
            }
            
            for(var i = 0; startValue <= endValue; i++)
            {
                table[0].Add(startValue);
                table[1].Add(func(startValue));
                startValue += shift;
            }
            
            return table;
        }

        static void PrintTable(List<List<double>> table)
        {
            int size = table[0].Count;
            Console.Write("x: ");
            for (int i = 0; i < size; i++)
            {
                Console.Write("{0,7:###.###} ", table[0][i]);
            }
            Console.WriteLine();

            Console.Write("y: ");
            for (int i = 0; i < size; i++)
            {
                Console.Write("{0,7:###.###} ", table[1][i]);
            }
            Console.WriteLine();
        }

        static List<List<double>> GetInterpolationalRange(List<List<double>> table, double x, int degree)
        {
            if (table is null)
            {
                throw new ArgumentNullException(nameof(table),"Table is null");
            }

            if (x > table[0][^1])
            {
                throw new ArgumentOutOfRangeException(nameof(x), "Finding value is out of range of table.");
            }

            if (degree > table[0].Count)
            {
                throw new ArgumentOutOfRangeException(nameof(x), "Degree of polynomial is bigger than count of points of table.");
            }

            int index = 0;

            int tableSize = table[0].Count;
            for (; index < tableSize && x > table[0][index]; index++)
            {
            }

            var returningRange = new List<List<double>>();


            if (index < degree)
            {
                returningRange.Add(table[0].Take(degree + 1).ToList());
                returningRange.Add(table[1].Take(degree + 1).ToList());
            }
            else
            {
                returningRange.Add(table[0].Skip(index - degree).Take(degree + 1).ToList());
                returningRange.Add(table[1].Skip(index - degree).Take(degree + 1).ToList());
            }

            return returningRange;
        }

    }
}