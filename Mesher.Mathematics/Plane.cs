using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesher.Mathematics
{
    public class Plane
    {
        public static Plane XYPlane = new Plane(0, 0, 1, 0);
        public static Plane XZPlane = new Plane(0, 1, 0, 0);
        public static Plane YZPlane = new Plane(1, 0, 0, 0);

        private const double Eps = 1e-9;

        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double D { get; set; }

        public Vec3 Normal
        {
            get
            {
                return new Vec3(A, B, C);
            }
        }

        public Plane(double a, double b, double c, double d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }

        public Vec3 Cross(Line l)
        {
            var nDotA = Normal.Dot(l.Point0);
            var nDotBa = Normal.Dot(l.Direction);

            return l.Point0 + (D - nDotA) / nDotBa * l.Direction;
        }
    }
}
