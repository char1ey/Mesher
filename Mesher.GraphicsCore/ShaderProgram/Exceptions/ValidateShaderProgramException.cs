using System;

namespace Mesher.GraphicsCore.ShaderProgram.Exceptions
{
    public class ValidateShaderProgramException:Exception
    {
        public ValidateShaderProgramException() { }
        public ValidateShaderProgramException(String log):base(log) { }
    }
}
