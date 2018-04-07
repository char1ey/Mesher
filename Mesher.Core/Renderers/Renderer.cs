using System;
using Mesher.Core.Components;
using Mesher.Core.Objects.Camera;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.ShaderProgram;
using Scene = Mesher.Core.Objects.Scene.Scene;

namespace Mesher.Core.Renderers
{
    public abstract class Renderer
    {
        public ShaderProgram ShaderProgram { get; }

        public Renderer(DataContext dataContext, String vertexShaderSource, String geometryShaderSource, String fragmentShaderSource)
        {
            ShaderProgram = dataContext.CreateShaderProgram(vertexShaderSource, geometryShaderSource, fragmentShaderSource);
        }

        public Renderer(DataContext dataContext, String vertexShaderSource, String fragmentShaderSource)
        {
            ShaderProgram = dataContext.CreateShaderProgram(vertexShaderSource, fragmentShaderSource);
        }

        public abstract void Render(Scene scene, Camera sceneContext);
    }
}
