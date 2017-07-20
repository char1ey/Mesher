using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesher.GraphicsCore.Shaders
{
    public class CompileShaderException:Exception
    {
        public CompileShaderException() { }
        public CompileShaderException(string log) : base(log) { }
    }
}
