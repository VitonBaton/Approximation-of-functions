using System;

namespace Interpolation.InterpolationMethods
{
    class TableBuilder
    {
        private ValuesTable table;

        public double StartOfRange {get; set;}

        public double EndOfRange {get; set;}   

        public double Increment {get; set;} 

        public Func<double,double> Function { get; set; }

        public ValuesTable BuildTable()
        {
            if (Function is null)
            {
                throw new ArgumentNullException(nameof(Function), "Function is null");
            }

            if (EndOfRange < StartOfRange)
            {
                throw new ArgumentException("End value is less than start.");
            }

            if (Increment <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Increment), "Shift value must be positive.");
            }

            table = new ValuesTable();

            var currentX = StartOfRange;

            for (; currentX <= EndOfRange; currentX += Increment)
            {
                table.Points.Add((currentX, Function(currentX)));
            }

            return table;
        }

        public ValuesTable BuildTableForInterpolation(double x, int interpolationDegree)
        {
            if (table is null)
            {
                throw new ArgumentNullException(nameof(table), "Table is null");
            }

            if (x > table.Points[^1].X)
            {
                throw new ArgumentOutOfRangeException(nameof(x), "Finding value is out of range of table.");
            }

            if (interpolationDegree > table.Points.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(x), "Degree of polynomial is bigger than count of points of table.");
            }

            int index = 0;

            int tableSize = table.Points.Count;
            for (; index < tableSize && x > table.Points[index].X; index++)
            {
            }

            var returningRange = new ValuesTable();

            if (index < interpolationDegree)
            {
                returningRange.Points = table.Points.Take(interpolationDegree + 1).ToList();
            }
            else
            {
                returningRange.Points = table.Points.Skip(index - interpolationDegree).Take(interpolationDegree + 1).ToList();
            }

            return returningRange;
        }
    }
}