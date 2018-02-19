using System;

namespace Mesher.Mathematics
{
    public abstract class VecN
    {
        public abstract Int32 ComponentsCount { get; }
        public abstract Single[] GetComponentsFloat();
        public abstract Double[] GetComponentsDouble();
    }
}
