using System;
using Mesher.Core.Renderers;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Data.OpenGL;

namespace Mesher.Core.Plugins
{
    public abstract class Plugin
    {
        public MesherApplication MesherApplication { get; private set; }
		public Boolean Enabled { get; protected set; }
        public String Name { get; protected set; }

        public Plugin(MesherApplication mesherApplication)
        {
            MesherApplication = mesherApplication;
        }

        public abstract void Execute();
    }
}
