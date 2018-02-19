using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesher.Mathematics
{
    public class Color3
    {
        public Single R { get; set; }
        public Single G { get; set; }
        public Single B { get; set; }

        public Color3 () {  }

        public Color3(Single r, Single g, Single b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}
