using System;

namespace Mesher.GraphicsCore.Data
{
    public interface IDataBuffer<T> : IDisposable where T : struct
    {
        Int32 Count { get; }
        void Add(T value);

        void AddRange(T[] values);
        void RemoveAt(Int32 id);
        void Clear();
        T this[Int32 index] { get; set; }
    }
}