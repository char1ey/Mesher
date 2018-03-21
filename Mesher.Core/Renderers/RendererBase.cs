using System;
using Mesher.Core.Components;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.ShaderProgram;
using Scene = Mesher.Core.Objects.Scene.Scene;

namespace Mesher.Core.Renderers
{
    public abstract class RendererBase
    {
        public ShaderProgram ShaderProgram { get; }

        public RendererBase(RenderContext renderContext, String vertexShaderSource, String geometryShaderSource, String fragmentShaderSource)
        {
            ShaderProgram = renderContext.CreateShaderProgram(vertexShaderSource, geometryShaderSource, fragmentShaderSource);
        }

        public RendererBase(RenderContext renderContext, String vertexShaderSource, String fragmentShaderSource)
        {
            ShaderProgram = renderContext.CreateShaderProgram(vertexShaderSource, fragmentShaderSource);
        }

        public abstract void Render(Scene scene, SceneContextPrototype sceneContext);
    }
}
