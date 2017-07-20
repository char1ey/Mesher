using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesher.GraphicsCore.Shaders
{
    public class LinkShaderProgramException:Exception
    {
        public LinkShaderProgramException() { }
        public LinkShaderProgramException(string log): base(log) {  }
    }
}
