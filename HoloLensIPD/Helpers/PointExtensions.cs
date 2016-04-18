using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace HoloLensIPD.Helpers
{
    public static class PointExtensions
    {
        public static double DistanceTo(this Point p, Point other)
        {
            double a = p.X - other.X;
            double b = p.Y - other.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return distance;
        }
    }
}
