using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesher.Core.Events.EventArgs
{
    public class DocumentViewRemoveEventArgs : System.EventArgs
    {
        public IDocumentView DocumentView { get; private set; }

        public DocumentViewRemoveEventArgs(IDocumentView documentView)
        {
            DocumentView = documentView;
        }
    }
}
