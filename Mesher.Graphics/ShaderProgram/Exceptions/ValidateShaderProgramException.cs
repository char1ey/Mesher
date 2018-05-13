using System;

namespace Mesher.Graphics.ShaderProgram.Exceptions
{
    public class ValidateShaderProgramException:Exception
    {
        public ValidateShaderProgramException() { }
        public ValidateShaderProgramException(String log):base(log) { }
    }
}
