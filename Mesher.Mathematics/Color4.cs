using System;
using System.Drawing;

namespace Mesher.Mathematics
{
    public struct Color4
    {
        public Single R { get; set; }
        public Single G { get; set; }
        public Single B { get; set; }
        public Single A { get; set; }

        public Color4(Color color) : this(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f) { }

        public Color4(Single r, Single g, Single b, Single a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }
}
