using System;
using Mesher.Core.Objects;
using Mesher.Core.SceneContexts.Components;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Camera;
using Mesher.GraphicsCore.Data.OpenGL;
using Mesher.GraphicsCore.Primitives;
using Mesher.GraphicsCore.RenderContexts;

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