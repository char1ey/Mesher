using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Mesher.GraphicsCore.Buffers
{
    public class VertexBuffer<T> : VertexBufferBase, IDisposable
    {
        private UInt32[] m_Id;

        private List<T> m_Data;

        private Int32 m_StructSize;

        private DataContext m_dataContext;

        internal DataContext DataContext { get { return m_dataContext; } }

        public UInt32 Id
        {
            get { return m_Id[0]; }
        }

        public override Int32 Count
        {
            get { return m_Data.Count; }
        }

        public Int32 Capacity { get; private set; }

        public VertexBuffer(T[] data, DataContext dataContext) : this(dataContext)
        {
            m_Data = data.ToList();
            Resize(m_Data.Count);
            SetSubData(data, 0);
        }

        public VertexBuffer(DataContext dataContext)
        {
            m_dataContext = dataContext;

            m_StructSize = Marshal.SizeOf(typeof(T));
            Capacity = 1;

            m_Data = new List<T>(Capacity);

            m_Id = new UInt32[1];

            Gl.GenBuffers(1, m_Id);
            Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, Id);
            Gl.BufferData(Gl.GL_ARRAY_BUFFER, m_StructSize, IntPtr.Zero, Gl.GL_DYNAMIC_DRAW);
        }

        public T this[Int32 index]
        {
            get { return m_Data[index]; }
            set { }
        }

        public void Add(T[] data)
        {
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
        }

        public void Add(T data)
        {
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
        }

        public void Clear()
        {
            m_Data.Clear();
            Resize(1);
        }

        public override void Bind()
        {
            Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, Id);
        }

        public override void Unbind()
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

       /* void IBindableItem.Bind()
        {
            Bind();
        }

        void IBindableItem.Unbind()
        {
            Unbind();
        }*/
    }
}
