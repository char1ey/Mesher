using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.GraphicsCore;

namespace Mesher.Core.Renderers
{
    public class RendererDefault : RendererBase
    {
        public RendererDefault(RenderContext renderContext, String vertexShaderSource, String fragmentShaderSource) 
            : base(renderContext, vertexShaderSource, fragmentShaderSource)
        {
        }
    }
}
