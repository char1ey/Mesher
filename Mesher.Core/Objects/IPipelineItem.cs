using Mesher.Core.Components;
using Mesher.GraphicsCore.ShaderProgram;

namespace Mesher.Core.Objects
{
    public interface IRenderItem
    {
        void Render(SceneContextPrototype sceneContext, ShaderProgram shaderProgram);
    }
}