using System;
using Mesher.Core.Renderers;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Data.OpenGL;

namespace Mesher.Core.Plugins
{
    public abstract class Plugin
    {
        public Document Document { get; private set; }
		public Boolean Enabled { get; protected set; }
        public String Name { get; protected set; }

        public Plugin(Document document)
        {
            Document = document;
        }

        public abstract void Execute();
    }
}
