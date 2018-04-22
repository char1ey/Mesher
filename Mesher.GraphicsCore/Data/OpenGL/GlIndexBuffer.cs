using System;

namespace Mesher.GraphicsCore.Data.OpenGL
{
    public class GlIndexBuffer : IIndexBuffer
    {
        public Int32 Count { get; }
        public void Add(Int32 id)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(Int32 id)
        {
            throw new NotImplementedException();
        }

        public Int32 this[Int32 id]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}