using System;
using Mesher.GraphicsCore.Collections;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.GraphicsCore.Renderers;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Primitives
{
    public abstract class RPrimitive
    {
        protected IDataContext DataContext { get; private set; }

        public Mat4 Matrix { get; set; }

        public IDataBuffer<Vec3> Positions { get; set; }

        public Boolean IndexedRendering { get; set; }
        public IIndexBuffer Indexes { get; set; }

        internal RPrimitive(IDataContext dataContext)
        {
            DataContext = dataContext;
            Positions = dataContext.CreateDataBuffer<Vec3>();
            Indexes = dataContext.CreateIndexBuffer();
        }

        public abstract void Render(RenderersFactory renderersFactory, RLights rLights, IRenderContext renderContext);
    }
}