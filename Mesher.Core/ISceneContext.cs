using Mesher.Core.Objects.Camera;
using Mesher.Core.Objects.Scene;
using Mesher.Core.Renderers;
using Mesher.Core.SceneForm;
using Mesher.GraphicsCore;

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

        RenderContext RenderContext { get; }

        DataContext DataContext { get; }

        void Add(SceneContextComponent component);
        void Remove(SceneContextComponent component);
        void RemoveAt(int id);

        void Render();
    }
}