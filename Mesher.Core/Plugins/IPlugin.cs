using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Core.Objects;
using Mesher.Core.Objects.Scene;
using Mesher.Core.Renderers;
using Mesher.GraphicsCore;

namespace Mesher.Core.Plugins
{
    public interface IPlugin
    {
        String Name { get; }
        void Execute(RenderContext context, Scene scene, RendererBase renderer);
    }
}
