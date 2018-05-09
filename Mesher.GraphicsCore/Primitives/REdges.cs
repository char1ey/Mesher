using System;
using Mesher.GraphicsCore.Collections;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.GraphicsCore.Renderers;

namespace Mesher.GraphicsCore.Primitives
{
    public class REdges : RPrimitive
    {
        public Single Width { get; set; }

        internal REdges(IDataFactory dataFactory) : base(dataFactory)
        {
        }

        public override void Render(RenderersFactory renderersFactory, Lights lights, IRenderContext renderContext)
        {
            renderersFactory.EdgesRenderer.Render(this, lights, renderContext);
        }
    }
}