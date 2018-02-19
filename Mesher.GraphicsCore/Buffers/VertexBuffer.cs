using Mesher.Mathematics;
using System;
using System.Collections.Generic;

namespace Mesher.GraphicsCore.Buffers
{
    public class VertexBuffer<T> : IDisposable where T : VecN, new()
    {
        private readonly RenderManager m_renderManager;

        private UInt32[] m_id;

	    private T[] m_vertices;

        public UInt32 Id { get { return m_id[0]; } }

		public Int32 Count { get { return m_vertices.Length; } }

	    internal VertexBuffer(T[] vertices, RenderManager renderManager)
	    {
	        m_renderManager = renderManager;

		    m_vertices = vertices;

            GenBuffer();
            Bind();
		    SetData(vertices);
            UnBind();
	    }

        private void SetData(T[] vertices)
        {
            m_renderManager.Begin();
            Gl.BufferData(Gl.GL_ARRAY_BUFFER, GetVertexData(vertices), Gl.GL_STATIC_DRAW);
            m_renderManager.End();
        }

        private void GenBuffer()
        {
            m_id = new UInt32[1];
            m_renderManager.Begin();
            Gl.GenBuffers(1, m_id);
            m_renderManager.End();
        }

        public void Bind()
        {
            m_renderManager.Begin();
            Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, Id);
            m_renderManager.End();
        }

        public void UnBind()
        {
            m_renderManager.Begin();
            Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, 0);
            m_renderManager.End();
        }

	    private Single[] GetVertexData(T[] vertices)
        {
            var ret = new List<Single>();

            for (var i = 0; i < vertices.Length; i++)
                ret.AddRange(vertices[i].GetComponentsFloat());

            return ret.ToArray();
        }

        public void Dispose()
        {
            m_renderManager.Begin();
            Gl.DeleteBuffers(1, m_id);
            m_renderManager.End();
        }
    }
}
