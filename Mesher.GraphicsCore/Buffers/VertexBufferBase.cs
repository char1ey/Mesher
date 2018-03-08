using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesher.GraphicsCore.Buffers
{
    public abstract class VertexBufferBase : IBindableItem
    {
        public abstract Int32 Count { get; }
        public abstract void Bind();
        public abstract void Unbind();
    }
}
