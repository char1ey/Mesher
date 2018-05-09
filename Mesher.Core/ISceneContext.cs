using System;
using Mesher.Core.SceneContexts.Components;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Camera;
using Mesher.GraphicsCore.Data.OpenGL;
using Mesher.GraphicsCore.Primitives;
using Mesher.GraphicsCore.RenderContexts;

namespace Mesher.Core
{
    public interface ISceneContext
    {
        int Width { get; }
        int Height { get; }
        
        RCamera Camera { get; set; }

        RScene Scene { get; set; }

        CameraControler CameraControler { get; set; }

        IRenderContext RenderContext { get; set; }

        IntPtr Handle { get; }

        void Add(SceneContextComponent component);
        void Remove(SceneContextComponent component);
        void RemoveAt(int id);

        void Render();
    }
}