using System;
using System.Collections.Generic;
using System.Drawing;
using Mesher.GraphicsCore.Buffers;
using Mesher.GraphicsCore.RenderContexts;

namespace Mesher.GraphicsCore
{
    public sealed class DataContext : IDisposable 
    {
        private IntPtr m_hglrc;

        private WindowsRenderContext m_defaultRenderContext;

        private List<WindowsRenderContext> m_renderWindows;

        private List<IDisposable> m_buffers;

        private List<Texture.GlTexture> m_textures;

        private List<ShaderProgram.GlShaderProgram> m_shaderPrograms;

        public IntPtr GlrcHandle
        {
            get { return m_hglrc; }
        }

        public DataContext(IntPtr defaultRenderWindowHandge)
        {
            m_textures = new List<Texture.GlTexture>();
            m_buffers = new List<IDisposable>();
            m_renderWindows = new List<WindowsRenderContext>();     
            m_shaderPrograms = new List<ShaderProgram.GlShaderProgram>();
            m_defaultRenderContext = new WindowsRenderContext(defaultRenderWindowHandge);
            m_hglrc = Win32.wglCreateContext(m_defaultRenderContext.RenderWindowHandle);
        }

        public WindowsRenderContext CreateRenderWindow(IntPtr handle)
        {
            var renderWindow = m_renderWindows.Find(t => t.Handle == handle);

            if (renderWindow != null)
                return renderWindow;

            if(m_renderWindows.Count == 0)
                m_defaultRenderContext.Dispose();

            renderWindow = new WindowsRenderContext(handle);
            
            Win32.wglMakeCurrent(renderWindow.RenderWindowHandle, m_hglrc);

            renderWindow.DataContext = this;

            m_renderWindows.Add(renderWindow);

            m_defaultRenderContext = renderWindow;

            return renderWindow;
        }

        public IndexBuffer CreateIndexBuffer(Int32[] indicies)
        {
            m_defaultRenderContext.BeginRender();
            var buffer = new IndexBuffer(indicies, this);
            m_defaultRenderContext.EndRender();
            m_buffers.Add(buffer);
            return buffer;
        }

        public GlDataBuffer<T> CreateVertexBuffer<T>(T[] vertieces) where T : struct
        {
            m_defaultRenderContext.BeginRender();
            var buffer = new GlDataBuffer<T>(vertieces, this);
            m_defaultRenderContext.EndRender();
            m_buffers.Add(buffer);
            return buffer;
        }

        internal void Begin()
        {
            m_defaultRenderContext.BeginRender();
        }

        internal void End()
        {
            m_defaultRenderContext.EndRender();
        }

        public Texture.GlTexture CreateTexture(Int32 width, Int32 height)
        {
            m_defaultRenderContext.BeginRender();
            var texture = new Texture.GlTexture(width, height, this);
            m_defaultRenderContext.EndRender();
            m_textures.Add(texture);
            return texture;
        }

        public Texture.GlTexture CreateTexture(Bitmap bitmap)
        {
            m_defaultRenderContext.BeginRender();
            var texture = new Texture.GlTexture(bitmap, this);
            m_defaultRenderContext.EndRender();
            m_textures.Add(texture);
            return texture;
        }

        public ShaderProgram.GlShaderProgram CreateShaderProgram(Byte[] vertexShaderSource, Byte[] fragmentShaderSource)
        {
            m_defaultRenderContext.BeginRender();
            var shaderProgram = new ShaderProgram.GlShaderProgram(this, vertexShaderSource, fragmentShaderSource);
            m_defaultRenderContext.EndRender();

            m_shaderPrograms.Add(shaderProgram);
            return shaderProgram;
        }

        public ShaderProgram.GlShaderProgram CreateShaderProgram(String vertexShaderSource, String fragmentShaderSource)
        {
            m_defaultRenderContext.BeginRender();
            var shaderProgram = new ShaderProgram.GlShaderProgram(this, vertexShaderSource, fragmentShaderSource);
            m_defaultRenderContext.EndRender();

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
