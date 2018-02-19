using System;

namespace Mesher.GraphicsCore.ShaderProgram.Exceptions
{
    public class CompileShaderException:Exception
    {
        public CompileShaderException() { }
        public CompileShaderException(String log) : base(log) { }
    }
}
