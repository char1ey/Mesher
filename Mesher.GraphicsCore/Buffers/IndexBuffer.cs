using System;

namespace Mesher.GraphicsCore.Buffers
{
    public class IndexBuffer : IDisposable, IBindableItem
    {
        private RenderContext m_renderContext;

        private UInt32[] m_id;

        private Int32[] m_indicies;

        internal RenderContext RenderContext { get { return m_renderContext; } }
        public UInt32 Id { get { return m_id[0]; } }

        public Int32 Count { get { return m_indicies.Length; } }

        internal IndexBuffer(Int32[] indicies, RenderContext renderContext)
        {
            m_renderContext = renderContext;

            m_indicies = indicies;

            GenBuffer();
            Bind();
            SetData(indicies);
            Unbind();
        }

        private void SetData(Int32[] indicies)
        {
            m_renderContext.Begin();
            Gl.BufferData(Gl.GL_ELEMENT_ARRAY_BUFFER, indicies, Gl.GL_STATIC_DRAW);
            m_renderContext.End();
        }

        private void GenBuffer()
        {
            m_renderContext.Begin();
            m_id = new UInt32[1];
            Gl.GenBuffers(1, m_id);
            m_renderContext.End();
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
            m_renderContext.Begin();
            Gl.BindBuffer(Gl.GL_ELEMENT_ARRAY_BUFFER, 0);
            m_renderContext.End();
        }

        public void Bind()
        {
            m_renderContext.Begin();
            Gl.BindBuffer(Gl.GL_ELEMENT_ARRAY_BUFFER, Id);
            m_renderContext.End();
        }

        public void Dispose()
        {
            m_renderContext.Begin();
            Gl.DeleteBuffers(1, m_id);
            m_renderContext.End();
        }
    }
}
