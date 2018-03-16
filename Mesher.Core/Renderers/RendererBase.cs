using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.ShaderProgram;

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
    }
}
