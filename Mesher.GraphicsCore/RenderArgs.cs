using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.GraphicsCore.Camera;
using Mesher.GraphicsCore.Collections;

namespace Mesher.GraphicsCore
{
    public class RenderArgs
    {
        public RLights RLights { get; set; }
        public RCamera RCamera { get; set; }
    }
}
