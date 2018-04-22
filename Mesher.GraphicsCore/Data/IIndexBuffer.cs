using System;

namespace Mesher.GraphicsCore.Data
{
    public interface IIndexBuffer
    {
        Int32 Count { get; }

        void Add(Int32 id);
        void RemoveAt(Int32 id);

        Int32 this[Int32 id] { get; set; }
    }
}