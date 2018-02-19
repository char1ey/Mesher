using System;
using System.Collections.Generic;
using Mesher.GraphicsCore.Buffers;
using Mesher.GraphicsCore.Objects;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore
{
    public sealed class RenderManager : IDisposable 
    {
        private ShaderProgram.ShaderProgram m_shaderProgram;

        private IntPtr m_hglrc;

        private List<RenderWindow> m_renderWindows;

        private RenderWindow m_defaultRenderWindow;

        public IntPtr GlrcHandle
        {
            get { return m_hglrc; }
        }
        
        internal ShaderProgram.ShaderProgram ShaderProgram
        {
            get
            {
                if (m_shaderProgram == null)
                    CreateShaderProgram();

                return m_shaderProgram;
            }
        }

        public RenderManager(IntPtr defaultRenderWindowHandge)
        {
            m_renderWindows = new List<RenderWindow>();     
            m_defaultRenderWindow = new RenderWindow(defaultRenderWindowHandge);
            m_hglrc = Win32.wglCreateContext(m_defaultRenderWindow.RenderWindowHandle);
        }

        public RenderWindow CreateRenderWindow(IntPtr handle)
        {
            var renderWindow = m_renderWindows.Find(t => t.Handle == handle);

            if (renderWindow != null)
                return renderWindow;

            if(m_renderWindows.Count == 0)
                m_defaultRenderWindow.Dispose();

            renderWindow = new RenderWindow(handle);
            
            Win32.wglMakeCurrent(renderWindow.RenderWindowHandle, m_hglrc);

            renderWindow.RenderManager = this;

            m_renderWindows.Add(renderWindow);

            m_defaultRenderWindow = renderWindow;

            return renderWindow;
        }

        public IndexBuffer CreateIndexBuffer(Int32[] indicies)
        {
            m_defaultRenderWindow.Begin();
            var buffer = new IndexBuffer(indicies, this);
            m_defaultRenderWindow.End();
            return buffer;
        }

        public VertexBuffer<T> CreateVertexBuffer<T>(T[] vertieces) where T : VecN, new()
        {
            m_defaultRenderWindow.Begin();
            var buffer = new VertexBuffer<T>(vertieces, this);
            m_defaultRenderWindow.End();
            return buffer;
        }

        internal void Begin()
        {
            m_defaultRenderWindow.Begin();
        }

        internal void End()
        {
            m_defaultRenderWindow.End();
        }

        public Scene CreateScene()
        {
            return new Scene();
        }

        private void CreateShaderProgram()
        {
            m_defaultRenderWindow.Begin();
            m_shaderProgram = new ShaderProgram.ShaderProgram();
            m_defaultRenderWindow.End();
        }        

        public void Dispose()
        {
            Win32.wglDeleteContext(m_hglrc);
        }
    }
}
