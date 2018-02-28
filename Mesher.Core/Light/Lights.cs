using System;
using System.Collections.Generic;

namespace Mesher.Core.Light
{
    public class Lights : List<Light>
    {
        public const Int32 MAX_LIGHTS_COUNT = 127;
        public new void Add(Light light)
        {
            if(Count + 1 > MAX_LIGHTS_COUNT)
                throw new OverflowException();

            Add(light);
        }
    }
}
