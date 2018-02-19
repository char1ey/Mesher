using System;

namespace Mesher.GraphicsCore.ShaderProgram.Exceptions
{
    public class LinkShaderProgramException:Exception
    {
        public LinkShaderProgramException() { }
        public LinkShaderProgramException(String log): base(log) {  }
    }
}
