using System;
using Mesher.Core.Objects.Scene;
using Mesher.Core.Renderers;
using Mesher.GraphicsCore;

namespace Mesher.Core.Plugins
{
    public interface IPlugin
    {
        String Name { get; }
        void Execute(DataContext context, Scene scene, RendererBase renderer);
    }
}
