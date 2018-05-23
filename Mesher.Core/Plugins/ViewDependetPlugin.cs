using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesher.Core.Plugins
{
    public abstract class ViewDependetPlugin : Plugin
    {
        protected IDocumentView DocumentView { get; private set; }
        public ViewDependetPlugin(MesherApplication mesherApplication, IDocumentView documentView) : base(mesherApplication)
        {
            DocumentView = documentView;
        }
    }
}
