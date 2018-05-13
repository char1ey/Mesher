using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Graphics;

namespace Mesher.Core.Events.EventArgs
{
    public class RenderEventArgs : System.EventArgs
    {
        public MesherGraphics Graphics { get; private set; }
        public IDocumentView DocumentView { get; private set; }

        public RenderEventArgs(MesherGraphics graphics, IDocumentView documentView)
        {
            Graphics = graphics;
            DocumentView = documentView;
        }
    }
}
