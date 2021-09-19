using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuesTable = System.Collections.Generic.List<System.Collections.Generic.List<double>>;

namespace Interpolation
{
    interface IInterpolator
    {
        public double GetValue(ValuesTable table, double x);
    }
}
