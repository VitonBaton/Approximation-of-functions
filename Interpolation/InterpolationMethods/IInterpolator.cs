using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpolation.InterpolationMethods
{
    interface IInterpolator
    {
        public double GetValue(ValuesTable table, double x);
    }
}
