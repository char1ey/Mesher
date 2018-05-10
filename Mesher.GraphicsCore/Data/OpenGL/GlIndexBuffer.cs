using System;
using Mesher.GraphicsCore.Buffers;

namespace Mesher.GraphicsCore.Data.OpenGL
{
    public class GlIndexBuffer : IIndexBuffer, IBindableItem, IDisposable
    {
        private GlDataFactory m_dataFactory;

        private UInt32[] m_id;

        private Int32[] m_indicies;

        internal GlDataFactory DataFactory { get { return m_dataFactory; } }
        public UInt32 Id { get { return m_id[0]; } }

        public Int32 Count { get { return m_indicies.Length; } }
        public void AddRange(Int32[] ids)
        {
            m_dataFactory.BeginChangeData();
            m_indicies = ids;
            Bind();
            SetData(m_indicies);
            Unbind();
            m_dataFactory.EndChangeData();
        }

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

        internal GlIndexBuffer(Int32[] indicies, GlDataFactory dataFactory)
        {
            m_dataFactory = dataFactory;

            m_indicies = indicies;

            GenBuffer();
            Bind();
            SetData(indicies);
            Unbind();
        }
        internal GlIndexBuffer(GlDataFactory dataFactory)
        {
            m_dataFactory = dataFactory;

            m_indicies = new Int32[1];

            GenBuffer();
            Bind();
            SetData(m_indicies);
            Unbind();
        }

        private void SetData(Int32[] indicies)
        {
            m_dataFactory.BeginChangeData();
            Gl.BufferData(Gl.GL_ELEMENT_ARRAY_BUFFER, indicies, Gl.GL_STATIC_DRAW);
            m_dataFactory.EndChangeData();
        }

        private void GenBuffer()
        {
            m_dataFactory.BeginChangeData();
            m_id = new UInt32[1];
            Gl.GenBuffers(1, m_id);
            m_dataFactory.EndChangeData();
        }

        public void Unbind()
        {
            m_dataFactory.BeginChangeData();
            Gl.BindBuffer(Gl.GL_ELEMENT_ARRAY_BUFFER, 0);
            m_dataFactory.EndChangeData();
        }

        public void Bind()
        {
            m_dataFactory.BeginChangeData();
            Gl.BindBuffer(Gl.GL_ELEMENT_ARRAY_BUFFER, Id);
            m_dataFactory.EndChangeData();
        }

        public void Dispose()
        {
            m_dataFactory.BeginChangeData();
            Gl.DeleteBuffers(1, m_id);
            m_dataFactory.EndChangeData();
        }
    }
}
