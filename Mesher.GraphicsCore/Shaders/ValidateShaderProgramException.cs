using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesher.GraphicsCore.Shaders
{
    public class ValidateShaderProgramException:Exception
    {
        public ValidateShaderProgramException() { }
        public ValidateShaderProgramException(string log):base(log) { }
    }
}
