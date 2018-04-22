using System;
using Mesher.GraphicsCore.Texture;

namespace Mesher.GraphicsCore.Data
{
    public interface IDataContext
    {
        IDataBuffer<T> CreateDataBuffer<T>() where T : struct;
        IIndexBuffer CreateIndexBuffer();
        Texture.Texture CreateTexture(Int32 width, Int32 height, PixelFormat pixelFormat);

        Texture.Texture LoadTextureFromFile(String fileName);
    }
}