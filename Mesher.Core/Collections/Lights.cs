using System.Collections.Generic;
using Mesher.Core.Objects;
using Mesher.GraphicsCore.Collections;

namespace Mesher.Core.Collections
{
    public class Lights : List<Light>
    {
        public RLights RLights { get; private set; }

        public Lights()
        {
            RLights = new RLights();
        }
    }
}