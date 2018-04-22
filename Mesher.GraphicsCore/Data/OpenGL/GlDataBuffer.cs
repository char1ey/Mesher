using System;

namespace Mesher.GraphicsCore.Data.OpenGL
{
    public class GlDataBuffer<T> : IDataBuffer<T> where T : struct 
    {
        public Int32 Count { get; }
        public void Add(T value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(Int32 id)
        {
            throw new NotImplementedException();
        }

        public T this[Int32 index]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}