using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesher.Mathematics
{
    public class Line
    {
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

        public Line(double a, double b, double c, double d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }

        public Vec3 Cross(Plane p)
        {
            throw new NotImplementedException();
        }
    }
}
