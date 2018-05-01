using System;
using System.Drawing;
using Mesher.GraphicsCore.Texture;

namespace Mesher.GraphicsCore.Data
{
    public interface IDataFactory
    {
        IDataBuffer<T> CreateDataBuffer<T>() where T : struct;
        IIndexBuffer CreateIndexBuffer();

        IFrameBuffer CreateFrameBuffer();

        Texture.Texture CreateTexture(Int32 width, Int32 height, PixelFormat pixelFormat);

        Texture.Texture LoadTextureFromFile(String fileName);

        Texture.Texture CreateTexture(Bitmap bitmap);
    }
}