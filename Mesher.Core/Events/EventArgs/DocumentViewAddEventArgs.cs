using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesher.Core.Events.EventArgs
{
    public class DocumentViewAddEventArgs : System.EventArgs
    {
        public IDocumentView DocumentView { get; private set; }

        public DocumentViewAddEventArgs(IDocumentView documentView)
        {
            DocumentView = documentView;
        }
    }
}
