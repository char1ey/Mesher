using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesher.Mathematics
{
    public class Triangle
    {
        public Vec3 A;
        public Vec3 B;
        public Vec3 C;

        public Triangle(Vec3 a, Vec3 b, Vec3 c)
        {
            A = a;
            B = b;
            C = c;
        }
    }
}
