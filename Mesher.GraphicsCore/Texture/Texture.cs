using System;

namespace Mesher.GraphicsCore.Texture
{
    public abstract class Texture : IDisposable
    {
        public PixelFormat PixelFormat { get; private set; }
        public Int32 Width { get; protected set; }
        public Int32 Height { get; protected set; }

        internal Texture(Int32 width, Int32 height, PixelFormat pixelFormat)
        {
            Width = width;
            Height = height;
            PixelFormat = pixelFormat;
        }

        internal Texture(String fileName)
        {

        }

        public abstract IntPtr GetSubTexture(Int32 x, Int32 y, Int32 width, Int32 height);
        public abstract void SetSubTexture(Int32 x, Int32 y, Int32 width, Int32 height, IntPtr pixelsData);
        public abstract void Dispose();
    }

}