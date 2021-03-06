﻿using System;
using System.Collections.Generic;
using System.Drawing;
using Mesher.Graphics.Imports;
using Mesher.Graphics.RenderContexts;
using Mesher.Graphics.Texture;
using Mesher.Graphics.Texture.OpenGL;

namespace Mesher.Graphics.Data.OpenGL
{
    public class GlDataContext : IDataContext, IDisposable 
    {
        private IntPtr m_hglrc;

        private GlWindowsRenderContext m_defaultRenderContext;

        private List<GlWindowsRenderContext> m_renderWindows;

        private List<IDisposable> m_buffers;

        private List<GlTexture> m_textures;

        private List<ShaderProgram.GlShaderProgram> m_shaderPrograms;

        public IntPtr GlrcHandle
        {
            get { return m_hglrc; }
        }

        public GlDataContext(GlWindowsRenderContext defaultRenderWindow)
        {
            m_textures = new List<GlTexture>();
            m_buffers = new List<IDisposable>();
            m_renderWindows = new List<GlWindowsRenderContext>();     
            m_shaderPrograms = new List<ShaderProgram.GlShaderProgram>();
            m_defaultRenderContext = defaultRenderWindow;
            m_hglrc = Win32.wglCreateContext(m_defaultRenderContext.RenderWindowHandle);
        }

        public GlIndexBuffer CreateIndexBuffer(List<Int32> indicies)
        {
            m_defaultRenderContext.BeginRender();
            var buffer = new GlIndexBuffer(indicies, this);
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

        internal void BeginChangeData()
        {
            m_defaultRenderContext.BeginRender();
        }

        internal void EndChangeData()
        {
            m_defaultRenderContext.EndRender();
        }

        public IDataBuffer<T> CreateDataBuffer<T>() where T : struct
        {
            m_defaultRenderContext.BeginRender();
            var buffer = new GlDataBuffer<T>(this);
            m_defaultRenderContext.EndRender();
            m_buffers.Add(buffer);
            return buffer;
        }

        public IFrameBuffer CreateFrameBuffer()
        {
            return new GlFrameBuffer();
        }

        public IIndexBuffer CreateIndexBuffer()
        {
            m_defaultRenderContext.BeginRender();
            var buffer = new GlIndexBuffer(this);
            m_defaultRenderContext.EndRender();
            m_buffers.Add(buffer);
            return buffer;
        }

        public Texture.Texture CreateTexture(Int32 width, Int32 height, PixelFormat pixelFormat)
        {
            throw new NotImplementedException();
        }

        public Texture.Texture LoadTextureFromFile(String fileName)
        {
            throw new NotImplementedException();
        }

        Texture.Texture IDataContext.CreateTexture(Bitmap bitmap)
        {
            return CreateTexture(bitmap);
        }

        public GlTexture CreateTexture(Bitmap bitmap)
        {
            m_defaultRenderContext.BeginRender();
            var texture = new GlTexture(bitmap, this);
            m_defaultRenderContext.EndRender();
            m_textures.Add(texture);
            return texture;
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
