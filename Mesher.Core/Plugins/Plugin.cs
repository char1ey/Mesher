using System;
using Mesher.Core.Renderers;

namespace Mesher.Core.Plugins
{
    public abstract class Plugin : IDisposable
    {
        public MesherApplication MesherApplication { get; private set; }
		public Boolean Enabled { get; protected set; }
        public String Name { get; protected set; }

        public Plugin(MesherApplication mesherApplication)
        {
            MesherApplication = mesherApplication;
        }

        public abstract void Execute();
        public abstract void Dispose();
    }
}
