using System;
using System.Collections;
using System.Collections.Generic;

namespace Mesher.GraphicsCore.Data
{
    public interface IIndexBuffer : IDisposable
    {
        Int32 Count { get; }

        void AddRange(List<Int32> ids);
        void Add(Int32 id);
        void RemoveAt(Int32 id);
        void Clear();
        Int32 this[Int32 id] { get; set; }
    }
}