using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesher.GraphicsCore.Buffers
{
    internal interface IBindableItem
    {
        void Bind();
        void Unbind();
    }
}
