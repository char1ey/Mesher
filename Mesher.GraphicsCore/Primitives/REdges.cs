using System;
using System.Drawing;
using Mesher.GraphicsCore.Camera;
using Mesher.GraphicsCore.Collections;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.GraphicsCore.Renderers;

namespace Mesher.GraphicsCore.Primitives
{
    public class REdges : RPrimitive
    {
        public Single Width { get; set; }
        public Color Color { get; set; }

        internal REdges(IDataContext dataContext) : base(dataContext)
        {
        }

        public override void Render(RenderersFactory renderersFactory, RenderArgs renderArgs)
        {
            renderersFactory.EdgesRenderer.Render(this, renderArgs);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}