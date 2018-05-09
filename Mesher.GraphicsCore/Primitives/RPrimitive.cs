using System;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.GraphicsCore.Renderers;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Primitives
{
    public abstract class RPrimitive
    {
        public RScene RScene { get; private set; }
        protected IDataFactory DataFactory { get; private set; }

        public Mat4 Matrix { get; set; }

        public IDataBuffer<Vec3> Positions { get; private set; }

        public Boolean IndexedRendering { get; set; }
        public IIndexBuffer Indexes { get; private set; }

        internal RPrimitive(IDataFactory dataFactory, RScene scene)
        {
            DataFactory = dataFactory;
            RScene = scene;
            Positions = dataFactory.CreateDataBuffer<Vec3>();
            Indexes = dataFactory.CreateIndexBuffer();
        }

        public abstract void Render(RenderersFactory sceneRenderer, IRenderContext renderContext);
    }
}