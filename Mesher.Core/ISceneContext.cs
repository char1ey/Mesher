using Mesher.Core.Objects.Camera;
using Mesher.Core.Objects.Scene;
using Mesher.Core.Renderers;
using Mesher.Core.SceneForm;

namespace Mesher.Core
{
    public interface ISceneContext
    {
        int Width { get; }
        int Height { get; }
        
        Camera Camera { get; set; }

        Scene Scene { get; set; }

        Renderer Renderer { get; set; }

        CameraControler CameraControler { get; set; }

        void Add(SceneContextComponent component);
        void Remove(SceneContextComponent component);
        void RemoveAt(int id);

        void Render();
    }
}