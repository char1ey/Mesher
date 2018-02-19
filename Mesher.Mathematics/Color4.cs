using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesher.Mathematics
{
    public class Color4
    {
        public Single R { get; set; }
        public Single G { get; set; }
        public Single B { get; set; }
        public Single A { get; set; }

        public Color4() { }

        public Color4(Single r, Single g, Single b, Single a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }
}
