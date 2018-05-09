using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.GraphicsCore.Light;
using Mesher.GraphicsCore.Primitives;

namespace Mesher.GraphicsCore.Renderers
{
    public abstract class RLightRenderer
    {
        public abstract void RenderShadowMap(RLight rLight, List<RPrimitive> primitives);
    }
}
