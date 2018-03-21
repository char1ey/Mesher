using System;
using System.Collections.Generic;
using System.Drawing;
using Mesher.GraphicsCore.Buffers;

namespace Mesher.GraphicsCore
{
    public sealed class RenderContext : IDisposable 
    {
        private IntPtr m_hglrc;

        private RenderWindow m_defaultRenderWindow;

        private List<RenderWindow> m_renderWindows;

        private List<IDisposable> m_buffers;

        private List<Texture.Texture> m_textures;

        private List<ShaderProgram.ShaderProgram> m_shaderPrograms;

        public IntPtr GlrcHandle
        {
            get { return m_hglrc; }
        }

        public RenderContext(IntPtr defaultRenderWindowHandge)
        {
            m_textures = new List<Texture.Texture>();
            m_buffers = new List<IDisposable>();
            m_renderWindows = new List<RenderWindow>();     
            m_shaderPrograms = new List<ShaderProgram.ShaderProgram>();
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

        public ShaderProgram.ShaderProgram CreateShaderProgram(String vertexShaderSource, String fragmentShaderSource)
        {
            return CreateShaderProgram(vertexShaderSource, null, fragmentShaderSource);
        }

        public ShaderProgram.ShaderProgram CreateShaderProgram(String vertexShaderSource, String geometryShaderSource, String fragmentShaderSource)
        {
            m_defaultRenderWindow.Begin();
            var shaderProgram = new ShaderProgram.ShaderProgram(this, vertexShaderSource, geometryShaderSource, fragmentShaderSource);
            m_defaultRenderWindow.End();

            m_shaderPrograms.Add(shaderProgram);
            return shaderProgram;
        }        

        public void Dispose()
        {
            foreach(var buffers in m_buffers)
                buffers.Dispose();
            foreach(var window in m_renderWindows)
                window.Dispose();
            foreach(var texture in m_textures)
                texture.Dispose();
            foreach(var shaderProgram in m_shaderPrograms)
                shaderProgram.Dispose();

            Win32.wglDeleteContext(m_hglrc);
        }
    }
}
