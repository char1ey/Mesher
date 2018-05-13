using System;
using Mesher.Core.Objects;
using Mesher.Core.SceneContexts.Components;
using Mesher.Graphics.Camera;
using Mesher.Graphics.RenderContexts;

namespace Mesher.Core
{
    public interface IDocumentView
    {
        int Width { get; }
        int Height { get; }
        
        RCamera Camera { get; set; }

        Document Document { get; set; }

        CameraControler CameraControler { get; set; }

        IRenderContext RenderContext { get; }

        void BeginRender();
        void EndRender();
        void Render();
    }
}