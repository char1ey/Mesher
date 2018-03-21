using System;

namespace Mesher.GraphicsCore.Buffers
{
    public abstract class VertexBufferBase : IBindableItem
    {
        public abstract Int32 Count { get; }
        public abstract void Bind();
        public abstract void Unbind();
    }
}
