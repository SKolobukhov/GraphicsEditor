using DrawablesUI;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawablesUI
{
    public class Vector
    {
        public readonly PointF value;

        public Vector(PointF point)
        {
            value = point;
        }
        public static Vector ToVector(PointF point)
        {
            return new Vector(point);
        }
    }
}
