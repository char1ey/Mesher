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
        protected IDataContext DataContext { get; private set; }

        public Mat4 Matrix { get; set; }

        public IDataBuffer<Vec3> Positions { get; private set; }

        public Boolean IndexedRendering { get; set; }
        public IIndexBuffer Indexes { get; private set; }

        internal RPrimitive(IDataContext dataContext, RScene scene)
        {
            DataContext = dataContext;
            RScene = scene;
            Positions = dataContext.CreateDataBuffer<Vec3>();
            Indexes = dataContext.CreateIndexBuffer();
        }

        public abstract void Render(RSceneRenderer sceneRenderer, IRenderContext renderContext);
    }
}