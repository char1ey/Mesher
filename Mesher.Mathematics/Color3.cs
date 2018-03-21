using System;

namespace Mesher.Mathematics
{
    public struct Color3
    {
        public Single R { get; set; }
        public Single G { get; set; }
        public Single B { get; set; }

        public Color3(Single r, Single g, Single b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}
