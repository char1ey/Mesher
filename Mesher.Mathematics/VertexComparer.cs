using System;
using System.Collections.Generic;

namespace Mesher.Mathematics
{
    public class VertexComparer : EqualityComparer<Vertex>
    {
        private const double Eps = 1e-9;

        public override bool Equals(Vertex a, Vertex b)
        {
            return Math.Abs(a.X - b.X) < Eps && Math.Abs(a.Y - b.Y) < Eps && Math.Abs(a.Z - b.Z) < Eps;
        }

        public override int GetHashCode(Vertex v)
        {
            return v.X.GetHashCode() ^ v.Y.GetHashCode() ^ v.Z.GetHashCode();
        }
    }
}