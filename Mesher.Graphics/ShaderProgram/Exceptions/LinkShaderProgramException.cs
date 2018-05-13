using System;

namespace Mesher.Graphics.ShaderProgram.Exceptions
{
    public class LinkShaderProgramException:Exception
    {
        public LinkShaderProgramException() { }
        public LinkShaderProgramException(String log): base(log) {  }
    }
}
