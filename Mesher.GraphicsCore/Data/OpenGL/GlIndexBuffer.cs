using System;
using Mesher.GraphicsCore.Buffers;

namespace Mesher.GraphicsCore.Data.OpenGL
{
    public class GlIndexBuffer : IIndexBuffer, IDisposable, IBindableItem
    {
        private GlDataContext m_dataContext;

        private UInt32[] m_id;

        private Int32[] m_indicies;

        internal GlDataContext DataContext { get { return m_dataContext; } }
        public UInt32 Id { get { return m_id[0]; } }

        public Int32 Count { get { return m_indicies.Length; } }
        public void AddRange(Int32[] ids)
        {
            m_dataContext.BeginChangeData();
            m_indicies = ids;
            Bind();
            SetData(m_indicies);
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

        public Int32 this[Int32 id]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        internal GlIndexBuffer(Int32[] indicies, GlDataContext dataContext)
        {
            m_dataContext = dataContext;

            m_indicies = indicies;

            GenBuffer();
            Bind();
            SetData(indicies);
            Unbind();
        }
        internal GlIndexBuffer(GlDataContext dataContext)
        {
            m_dataContext = dataContext;

            m_indicies = new Int32[1];

            GenBuffer();
            Bind();
            SetData(m_indicies);
            Unbind();
        }

        private void SetData(Int32[] indicies)
        {
            m_dataContext.BeginChangeData();
            Gl.BufferData(Gl.GL_ELEMENT_ARRAY_BUFFER, indicies, Gl.GL_STATIC_DRAW);
            m_dataContext.EndChangeData();
        }

        private void GenBuffer()
        {
            m_dataContext.BeginChangeData();
            m_id = new UInt32[1];
            Gl.GenBuffers(1, m_id);
            m_dataContext.EndChangeData();
        }

        void IBindableItem.Bind()
        {
            Bind();
        }

        void IBindableItem.Unbind()
        {
            Unbind();
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
