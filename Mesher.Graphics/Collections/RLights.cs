using System;
using System.Collections.Generic;

namespace Mesher.Graphics.Collections
{
    public class RLights : List<Light.RLight>, IDisposable
    {
        public void Dispose()
        {
            foreach(var rLight in this)
                rLight.Dispose();
        }
    }
}