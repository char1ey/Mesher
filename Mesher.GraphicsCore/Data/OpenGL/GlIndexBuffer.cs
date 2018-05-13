using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Mesher.GraphicsCore.Imports;

namespace Mesher.GraphicsCore.Data.OpenGL
{
    public class GlIndexBuffer : IIndexBuffer, IBindableItem, IDisposable
    {
        private Int32 m_StructSize;

        private GlDataContext m_dataContext;

        private UInt32[] m_id;

        private List<Int32> m_Data;

        internal GlDataContext DataContext { get { return m_dataContext; } }
        public UInt32 Id { get { return m_id[0]; } }

        public Int32 Count { get { return m_Data.Count; } }

        public Int32 Capacity { get; private set; }


        internal GlIndexBuffer(List<Int32> data, GlDataContext dataContext)
        {
            m_dataContext = dataContext;

            m_Data = data;

            GenBuffer();
            Bind();
            SetData(data.ToArray());
            Unbind();
        }

        internal GlIndexBuffer(GlDataContext dataContext)
        {
            m_dataContext = dataContext;

            m_Data = new List<Int32>();
            Capacity = 1;

            GenBuffer();
            Bind();
            SetData(m_Data.ToArray());
            Unbind();
        }

        public void AddRange(List<Int32> ids)
        {
            m_dataContext.BeginChangeData();
            m_Data = ids;
            Bind();
            SetData(m_Data.ToArray());
            Unbind();
            m_dataContext.EndChangeData();
        }

        public void Add(Int32 id)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(Int32 id)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            m_Data.Clear();
            Resize(1);
        }

        public Int32 this[Int32 id]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }


        private void SetData(Int32[] indicies)
        {
            m_dataContext.BeginChangeData();
            Gl.BufferData(Gl.GL_ELEMENT_ARRAY_BUFFER, indicies, Gl.GL_STATIC_DRAW);
            m_dataContext.EndChangeData();
        }

        private void Resize(Int32 newSize)
        {
            Capacity = newSize;
            Gl.BufferData(Gl.GL_ELEMENT_ARRAY_BUFFER, Capacity * m_StructSize, IntPtr.Zero, Gl.GL_STATIC_DRAW);
        }

        private void SetSubData(Int32 data, Int32 offset)
        {
            var ptr = Marshal.AllocHGlobal(m_StructSize);
            Marshal.StructureToPtr(data, ptr, false);
            Gl.BufferSubData(Gl.GL_ELEMENT_ARRAY_BUFFER, offset * m_StructSize, m_StructSize, ptr);
            Marshal.FreeHGlobal(ptr);
        }

        private void SetSubData(Int32[] data, Int32 offset)
        {
            var ptr = StructArrayToPtr(data);
            Gl.BufferSubData(Gl.GL_ELEMENT_ARRAY_BUFFER, offset * m_StructSize, m_StructSize * data.Length, ptr);
            Marshal.FreeHGlobal(ptr);
        }

        private IntPtr StructArrayToPtr(Int32[] data)
        {
            var ptr = Marshal.AllocHGlobal(m_StructSize * data.Length);

            for (var i = 0; i < data.Length; i++)
            {
                var cur = ptr + i * m_StructSize;
                Marshal.StructureToPtr(data[i], cur, false);
            }

            return ptr;
        }

        private void GenBuffer()
        {
            m_dataContext.BeginChangeData();
            m_id = new UInt32[1];
            Gl.GenBuffers(1, m_id);
            m_dataContext.EndChangeData();
        }

        public void Unbind()
        {
            m_dataContext.BeginChangeData();
            Gl.BindBuffer(Gl.GL_ELEMENT_ARRAY_BUFFER, 0);
            m_dataContext.EndChangeData();
        }

        public void Bind()
        {
            m_dataContext.BeginChangeData();
            Gl.BindBuffer(Gl.GL_ELEMENT_ARRAY_BUFFER, Id);
            m_dataContext.EndChangeData();
        }

        public void Dispose()
        {
            m_dataContext.BeginChangeData();
            Gl.DeleteBuffers(1, m_id);
            m_dataContext.EndChangeData();
        }
    }
}
