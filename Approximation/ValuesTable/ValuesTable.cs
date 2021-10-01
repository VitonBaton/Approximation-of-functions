using System;
using System.Collections.Generic;
using System.Text;

namespace Approximation
{
    class ValuesTable
    {
        public List<(double X, double Y)> Points { get; set;}

        public ValuesTable()
        {
            Points = new List<(double X, double Y)>();
        }

        public ValuesTable(double[] X, double[] Y)
        {
            if (X.Length != Y.Length)
            {
                throw new ArgumentException("Length of vectors isn't equal");
            }

            Points = new List<(double X, double Y)>();

            for (int i = 0; i < X.Length; i++)
            {
                Points.Add((X[i], Y[i]));
            }
        }

        public override string ToString()
        {
            var stringValue = new StringBuilder(string.Empty);

            stringValue.AppendLine("   x   |   y   ");
            foreach (var item in Points)
            {
                stringValue.AppendLine(string.Format("{0,7:###.###}|{1,7:###.###}", item.X, item.Y));
            }

            return stringValue.ToString();
        }
    } 
}