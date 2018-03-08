using System;
using System.Collections.Generic;
using System.Drawing;
using Mesher.GraphicsCore.Buffers;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore
{
    public sealed class RenderContext : IDisposable 
    {
        private ShaderProgram.ShaderProgram m_shaderProgram;

        private IntPtr m_hglrc;

        private RenderWindow m_defaultRenderWindow;

        private List<RenderWindow> m_renderWindows;

        private List<IDisposable> m_buffers;

        private List<Texture.Texture> m_textures;

        public IntPtr GlrcHandle
        {
            get { return m_hglrc; }
        }
        
        public ShaderProgram.ShaderProgram ShaderProgram
        {
            get
            {
                if (m_shaderProgram == null)
                    CreateShaderProgram();

                return m_shaderProgram;
            }
        }

        public RenderContext(IntPtr defaultRenderWindowHandge)
        {
            m_textures = new List<Texture.Texture>();
            m_buffers = new List<IDisposable>();
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

            renderWindow.RenderContext = this;

            m_renderWindows.Add(renderWindow);

            m_defaultRenderWindow = renderWindow;

            return renderWindow;
        }

        public IndexBuffer CreateIndexBuffer(Int32[] indicies)
        {
            m_defaultRenderWindow.Begin();
            var buffer = new IndexBuffer(indicies, this);
            m_defaultRenderWindow.End();
            m_buffers.Add(buffer);
            return buffer;
        }

        public VertexBuffer<T> CreateVertexBuffer<T>(T[] vertieces)
        {
            m_defaultRenderWindow.Begin();
            var buffer = new VertexBuffer<T>(vertieces, this);
            m_defaultRenderWindow.End();
            m_buffers.Add(buffer);
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

        public Texture.Texture CreateTexture(Int32 width, Int32 height)
        {
            m_defaultRenderWindow.Begin();
            var texture = new Texture.Texture(width, height, this);
            m_defaultRenderWindow.End();
            m_textures.Add(texture);
            return texture;
        }

        public Texture.Texture CreateTexture(Bitmap bitmap)
        {
            m_defaultRenderWindow.Begin();
            var texture = new Texture.Texture(bitmap, this);
            m_defaultRenderWindow.End();
            m_textures.Add(texture);
            return texture;
        }

        private void CreateShaderProgram()
        {
            m_defaultRenderWindow.Begin();
            m_shaderProgram = new ShaderProgram.ShaderProgram(this);
            m_defaultRenderWindow.End();
        }        

        public void Dispose()
        {
            foreach(var buffers in m_buffers)
                buffers.Dispose();
            foreach(var window in m_renderWindows)
                window.Dispose();
            foreach(var texture in m_textures)
                texture.Dispose();
            m_shaderProgram?.Dispose();

            Win32.wglDeleteContext(m_hglrc);
        }
    }
}
