using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Mesher.Graphics.Primitives;

namespace Mesher.Core.Events.EventArgs
{
    public class DocumentViewEventArgs : System.EventArgs
    {
        public List<PostRenderItem> PostRenderItems { get; private set; }

        public DocumentViewEventArgs()
        {
            PostRenderItems = new List<PostRenderItem>();
        }
    }
}
