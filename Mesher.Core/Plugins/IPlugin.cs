using System;
using Mesher.Core.Renderers;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Data.OpenGL;

namespace Mesher.Core.Plugins
{
    public interface IPlugin
    {
		Boolean Enabled { get; }
        String Name { get; }
        void Execute(MesherGraphics graphics);
	    void Render();
    }
}
