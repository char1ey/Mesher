using System;
using Mesher.Core.Components;
using Mesher.Core.Objects.Camera;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.ShaderProgram;
using Scene = Mesher.Core.Objects.Scene.Scene;

namespace Mesher.Core.Renderers
{
    public abstract class RendererBase
    {
        public ShaderProgram ShaderProgram { get; }

        public RendererBase(DataContext dataContext, String vertexShaderSource, String geometryShaderSource, String fragmentShaderSource)
        {
            ShaderProgram = dataContext.CreateShaderProgram(vertexShaderSource, geometryShaderSource, fragmentShaderSource);
        }

        public RendererBase(DataContext dataContext, String vertexShaderSource, String fragmentShaderSource)
        {
            ShaderProgram = dataContext.CreateShaderProgram(vertexShaderSource, fragmentShaderSource);
        }

        public abstract void Render(Scene scene, Camera sceneContext);
    }
}
