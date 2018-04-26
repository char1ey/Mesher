using Mesher.Core.Objects.Scene;
using Mesher.Core.Renderers;
using Mesher.Core.SceneContexts.Components;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Camera;
using Mesher.GraphicsCore.RenderContexts;

namespace Mesher.Core
{
    public interface ISceneContext
    {
        int Width { get; }
        int Height { get; }
        
        Camera Camera { get; set; }

        Scene Scene { get; set; }

        SceneRendererBase SceneRenderer { get; set; }

        CameraControler CameraControler { get; set; }

        WindowsRenderContext RenderContext { get; }

        DataContext DataContext { get; }

        void Add(SceneContextComponent component);
        void Remove(SceneContextComponent component);
        void RemoveAt(int id);

        void Render();
    }
}