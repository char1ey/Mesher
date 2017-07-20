using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesher.Mathematics
{
    public struct Triangle
    {
        public Vertex A;
        public Vertex B;
        public Vertex C;

        public Triangle(Vertex a, Vertex b, Vertex c)
        {
            A = a;
            B = b;
            C = c;
        }
    }
}
