using Mesher.Core.Components;
using Mesher.GraphicsCore.ShaderProgram;

namespace Mesher.Core.Objects
{
    public interface IRenderItem
    {
        void Render(SceneContextWinforms sceneContext, ShaderProgram shaderProgram);
    }
}