using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.RenderContexts;

namespace Mesher.Core.Events.EventArgs
{
    public class RenderEventArgs : System.EventArgs
    {
        public MesherGraphics Graphics { get; private set; }
        public IRenderContext RenderContext { get; private set; }

        public RenderEventArgs(MesherGraphics graphics, IRenderContext renderContext)
        {
            Graphics = graphics;
            RenderContext = renderContext;
        }
    }
}
