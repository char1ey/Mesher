using System;
using Mesher.GraphicsCore.Texture;

namespace Mesher.GraphicsCore.Data.OpenGL
{
    public class GlDataContext : IDataContext
    {
        public IDataBuffer<T> CreateDataBuffer<T>() where T : struct
        {
            throw new NotImplementedException();
        }

        public IIndexBuffer CreateIndexBuffer()
        {
            throw new NotImplementedException();
        }

        public Texture.Texture CreateTexture(Int32 width, Int32 height, PixelFormat pixelFormat)
        {
            throw new NotImplementedException();
        }

        public Texture.Texture LoadTextureFromFile(String fileName)
        {
            throw new NotImplementedException();
        }
    }
}