using System;

namespace Mesher.GraphicsCore.Buffers
{
    public class IndexBuffer : IDisposable
    {
        private RenderManager m_renderManager;

        private UInt32[] m_id;

        private Int32[] m_indicies;

        public UInt32 Id { get { return m_id[0]; } }

        public Int32 Count { get { return m_indicies.Length; } }

        internal IndexBuffer(Int32[] indicies, RenderManager renderManager)
        {
            m_renderManager = renderManager;

            m_indicies = indicies;

            GenBuffer();
            Bind();
            SetData(indicies);
            UnBind();
        }

        private void SetData(Int32[] indicies)
        {
            m_renderManager.Begin();
            Gl.BufferData(Gl.GL_ELEMENT_ARRAY_BUFFER, indicies, Gl.GL_STATIC_DRAW);
            m_renderManager.End();
        }

        private void GenBuffer()
        {
            m_renderManager.Begin();
            m_id = new UInt32[1];
            Gl.GenBuffers(1, m_id);
            m_renderManager.End();
        }

        public void Bind()
        {
            m_renderManager.Begin();
            Gl.BindBuffer(Gl.GL_ELEMENT_ARRAY_BUFFER, Id);
            m_renderManager.End();
        }

        public void UnBind()
        {
            m_renderManager.Begin();
            Gl.BindBuffer(Gl.GL_ELEMENT_ARRAY_BUFFER, 0);
            m_renderManager.End();
        }

        public void Dispose()
        {
            m_renderManager.Begin();
            Gl.DeleteBuffers(1, m_id);
            m_renderManager.End();
        }
    }
}
