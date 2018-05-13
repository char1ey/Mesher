using System;
using Mesher.Graphics.Data;
using Mesher.Graphics.Renderers;
using Mesher.Mathematics;

namespace Mesher.Graphics.Primitives
{
    public abstract class RPrimitive : IDisposable
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

        public abstract void Render(RenderersFactory renderersFactory, RenderArgs renderArgs);
        public virtual void Dispose()
        {
            Positions?.Dispose();
            Indexes?.Dispose();
        }
    }
}