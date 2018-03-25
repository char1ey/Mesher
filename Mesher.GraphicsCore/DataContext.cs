using System;
using System.Collections.Generic;
using System.Drawing;
using Mesher.GraphicsCore.Buffers;

namespace Mesher.GraphicsCore
{
    public sealed class DataContext : IDisposable 
    {
        private IntPtr m_hglrc;

        private RenderContext m_defaultRenderContext;

        private List<RenderContext> m_renderWindows;

        private List<IDisposable> m_buffers;

        private List<Texture.Texture> m_textures;

        private List<ShaderProgram.ShaderProgram> m_shaderPrograms;

        public IntPtr GlrcHandle
        {
            get { return m_hglrc; }
        }

        public DataContext(IntPtr defaultRenderWindowHandge)
        {
            m_textures = new List<Texture.Texture>();
            m_buffers = new List<IDisposable>();
            m_renderWindows = new List<RenderContext>();     
            m_shaderPrograms = new List<ShaderProgram.ShaderProgram>();
            m_defaultRenderContext = new RenderContext(defaultRenderWindowHandge);
            m_hglrc = Win32.wglCreateContext(m_defaultRenderContext.RenderWindowHandle);
        }

        public RenderContext CreateRenderWindow(IntPtr handle)
        {
            var renderWindow = m_renderWindows.Find(t => t.Handle == handle);

            if (renderWindow != null)
                return renderWindow;

            if(m_renderWindows.Count == 0)
                m_defaultRenderContext.Dispose();

            renderWindow = new RenderContext(handle);
            
            Win32.wglMakeCurrent(renderWindow.RenderWindowHandle, m_hglrc);

            renderWindow.DataContext = this;

            m_renderWindows.Add(renderWindow);

            m_defaultRenderContext = renderWindow;

            return renderWindow;
        }

        public IndexBuffer CreateIndexBuffer(Int32[] indicies)
        {
            m_defaultRenderContext.Begin();
            var buffer = new IndexBuffer(indicies, this);
            m_defaultRenderContext.End();
            m_buffers.Add(buffer);
            return buffer;
        }

        public VertexBuffer<T> CreateVertexBuffer<T>(T[] vertieces)
        {
            m_defaultRenderContext.Begin();
            var buffer = new VertexBuffer<T>(vertieces, this);
            m_defaultRenderContext.End();
            m_buffers.Add(buffer);
            return buffer;
        }

        internal void Begin()
        {
            m_defaultRenderContext.Begin();
        }

        internal void End()
        {
            m_defaultRenderContext.End();
        }

        public Texture.Texture CreateTexture(Int32 width, Int32 height)
        {
            m_defaultRenderContext.Begin();
            var texture = new Texture.Texture(width, height, this);
            m_defaultRenderContext.End();
            m_textures.Add(texture);
            return texture;
        }

        public Texture.Texture CreateTexture(Bitmap bitmap)
        {
            m_defaultRenderContext.Begin();
            var texture = new Texture.Texture(bitmap, this);
            m_defaultRenderContext.End();
            m_textures.Add(texture);
            return texture;
        }

        public ShaderProgram.ShaderProgram CreateShaderProgram(String vertexShaderSource, String fragmentShaderSource)
        {
            return CreateShaderProgram(vertexShaderSource, null, fragmentShaderSource);
        }

        public ShaderProgram.ShaderProgram CreateShaderProgram(String vertexShaderSource, String geometryShaderSource, String fragmentShaderSource)
        {
            m_defaultRenderContext.Begin();
            var shaderProgram = new ShaderProgram.ShaderProgram(this, vertexShaderSource, geometryShaderSource, fragmentShaderSource);
            m_defaultRenderContext.End();

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
