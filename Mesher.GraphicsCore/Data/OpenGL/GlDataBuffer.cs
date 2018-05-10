using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.Data.OpenGL;

namespace Mesher.GraphicsCore.Buffers
{
    public class GlDataBuffer<T> : IDataBuffer<T>, IBindableItem, IDisposable where T : struct
    {
        private UInt32[] m_Id;

        private List<T> m_Data;

        private Int32 m_StructSize;

        private GlDataFactory m_dataFactory;

        internal GlDataFactory DataFactory { get { return m_dataFactory; } }

        public UInt32 Id
        {
            get { return m_Id[0]; }
        }

        public Int32 Count
        {
            get { return m_Data.Count; }
        }

        public Int32 Capacity { get; private set; }

        public GlDataBuffer(T[] data, GlDataFactory dataFactory) : this(dataFactory)
        {
            m_Data = data.ToList();
            Resize(m_Data.Count);
            SetSubData(data, 0);
        }

        public GlDataBuffer(GlDataFactory dataFactory)
        {
            m_dataFactory = dataFactory;

            m_StructSize = Marshal.SizeOf(typeof(T));
            Capacity = 1;

            m_Data = new List<T>(Capacity);

            m_Id = new UInt32[1];

            Gl.GenBuffers(1, m_Id);
            Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, Id);
            Gl.BufferData(Gl.GL_ARRAY_BUFFER, m_StructSize, IntPtr.Zero, Gl.GL_DYNAMIC_DRAW);
        }

        public void AddRange(T[] data)
        {
            DataFactory.BeginChangeData();

            Bind();

            m_Data.AddRange(data);

            if (Capacity < m_Data.Count)
            {
                while (Capacity < m_Data.Count)
                    Capacity *= 2;

                Resize(Capacity);
                SetSubData(m_Data.ToArray(), 0);
            }
            else SetSubData(data, Count - 1);

            Unbind();

            DataFactory.EndChangeData();
        }

        public void RemoveAt(Int32 id)
        {
            throw new NotImplementedException();
        }

        public T this[Int32 index]
        {
            get { return m_Data[index]; }
            set { }
        }

        public void Add(T data)
        {
            m_dataFactory.BeginChangeData();

            Bind();

            m_Data.Add(data);

            if (Capacity < m_Data.Count)
            {
                while (Capacity < m_Data.Count)
                    Capacity *= 2;

                Resize(Capacity);
                SetSubData(m_Data.ToArray(), 0);
            }
            else SetSubData(data, Count - 1);

            Unbind();

            m_dataFactory.EndChangeData();
        }

        public void Clear()
        {
            m_Data.Clear();
            Resize(1);
        }

        public void Bind()
        {
            Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, Id);
        }

        public void Unbind()
        {
            Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, 0);
        }

        private void Resize(Int32 newSize)
        {
            Capacity = newSize;
            Gl.BufferData(Gl.GL_ARRAY_BUFFER, Capacity * m_StructSize, IntPtr.Zero, Gl.GL_DYNAMIC_DRAW);
        }

        private void SetSubData(T data, Int32 offset)
        {
            var ptr = Marshal.AllocHGlobal(m_StructSize);
            Marshal.StructureToPtr(data, ptr, false);
            Gl.BufferSubData(Gl.GL_ARRAY_BUFFER, offset * m_StructSize, m_StructSize, ptr);
            Marshal.FreeHGlobal(ptr);
        }

        private void SetSubData(T[] data, Int32 offset)
        {
            var ptr = StructArrayToPtr(data);
            Gl.BufferSubData(Gl.GL_ARRAY_BUFFER, offset * m_StructSize, m_StructSize * data.Length, ptr);
            Marshal.FreeHGlobal(ptr);
        }

        private IntPtr StructArrayToPtr(T[] data)
        {
            var ptr = Marshal.AllocHGlobal(m_StructSize * data.Length);

            for (var i = 0; i < data.Length; i++)
            {
                var cur = ptr + i * m_StructSize;
                Marshal.StructureToPtr(data[i], cur, false);
            }

            return ptr;
        }

        public void Dispose()
        {
            Bind();
            Gl.DeleteBuffers(1, m_Id);
        }
    }
}
