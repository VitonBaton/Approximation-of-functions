using System;
using System.Collections.Generic;
using System.Text;

namespace Interpolation.InterpolationMethods
{
    class ValuesTable
    {
        public List<(double X, double Y)> Points { get; set;}

        public ValuesTable()
        {
            Points = new List<(double X, double Y)>();
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