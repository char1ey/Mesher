using System;
using Mesher.Core.Objects.Scene;
using Mesher.Core.Renderers;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Data.OpenGL;

namespace Mesher.Core.Plugins
{
    public interface IPlugin
    {
        String Name { get; }
        void Execute(GlDataContext context, Scene scene, SceneRendererBase sceneRenderer);
    }
}
