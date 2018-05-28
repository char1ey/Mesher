using System;
using System.Collections.Generic;
using Mesher.Graphics.Light;
using Mesher.Graphics.Primitives;

namespace Mesher.Graphics.Renderers
{
    public abstract class RLightRenderer : IDisposable
    {
        public abstract void RenderShadowMap(RLight rLight, List<RPrimitive> primitives);
        public abstract void Dispose();
    }
}
