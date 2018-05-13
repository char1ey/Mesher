using System;
using System.Collections.Generic;
using Mesher.Graphics.Light;
using Mesher.Graphics.Primitives;

namespace Mesher.Graphics.Renderers.OpenGL
{
    public class DefaultGlRLightRenderer : RLightRenderer
    {
        public override void RenderShadowMap(RLight rLight, List<RPrimitive> primitives)
        {
            throw new NotImplementedException();
        }
    }
}
