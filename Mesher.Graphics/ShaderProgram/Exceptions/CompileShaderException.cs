using System;

namespace Mesher.Graphics.ShaderProgram.Exceptions
{
    public class CompileShaderException:Exception
    {
        public CompileShaderException() { }
        public CompileShaderException(String log) : base(log) { }
    }
}
