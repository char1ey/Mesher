using System;
using System.Drawing;

namespace Mesher.GraphicsCore.RenderContexts
{
    public interface IRenderContext
    {
        Int32 Width { get; }
        Int32 Height { get; }
        Camera.Camera Camera { get; set; }

        Color ClearColor { get; set; }

        void ClearColorBuffer(Color color);
        void ClearDepthBuffer();
        void SetSize(Int32 width, Int32 height);

        void SwapBuffers();
        void BeginRender();
        void EndRender();
    }
}