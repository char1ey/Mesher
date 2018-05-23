using System;
using System.Windows.Forms;
using Mesher.Core.Events;
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
        void Render(Scene scene);

        event MouseEventHandler MouseMove;
        event MouseEventHandler MouseWheel;
        event MouseEventHandler MouseClick;
        event KeyEventHandler KeyDown;
        event KeyEventHandler KeyUp;
        event EventHandler Resize;
        event OnAfterDocumentViewRender AfterDocumentViewRender;
    }
}