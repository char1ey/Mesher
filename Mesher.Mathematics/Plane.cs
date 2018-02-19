using System;

namespace Mesher.Mathematics
{
    public class Plane
    {
        public static Plane XYPlane = new Plane(0, 0, 1, 0);
        public static Plane XZPlane = new Plane(0, 1, 0, 0);
        public static Plane YZPlane = new Plane(1, 0, 0, 0);

        private const Double Eps = 1e-9;

        public Double A { get; set; }
        public Double B { get; set; }
        public Double C { get; set; }
        public Double D { get; set; }

        public Vec3 Normal
        {
            get
            {
                return new Vec3(A, B, C);
            }
        }

        public Plane(Double a, Double b, Double c, Double d)
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
