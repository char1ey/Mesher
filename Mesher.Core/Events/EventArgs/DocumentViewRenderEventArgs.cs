using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Mesher.Graphics;
using Mesher.Graphics.Primitives;

namespace Mesher.Core.Events.EventArgs
{
    public class DocumentViewRenderEventArgs : System.EventArgs
    {
        public MesherGraphics Graphics { get; private set; }
        public List<PostRenderItem> PostRenderItems { get; private set; }

        public DocumentViewRenderEventArgs(MesherGraphics graphics)
        {
            Graphics = graphics;
            PostRenderItems = new List<PostRenderItem>();
        }
    }
}
