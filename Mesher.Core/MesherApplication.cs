using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Data.OpenGL;
using Mesher.GraphicsCore.RenderContexts;

namespace Mesher.Core
{
    public class MesherApplication
    {
        public MesherGraphics Graphics { get; set; }
        public Document CurrentDocument { get; set; }
        public MainWindow MainWindow { get; private set; }

        public List<IDocumentView> DocumentViews { get; private set; }
        public List<Plugins.Plugin> Plugins { get; private set; }

        public MesherApplication()
        {
            Graphics = new GlWindowsGraphics();
            MainWindow = new MainWindow(this);
            DocumentViews = new List<IDocumentView> {MainWindow.DocumentView};
        }

        public Document LoadDocument(String fileName)
        {
            return Document.Load(fileName, this);
        }
    }
}
