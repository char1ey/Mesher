using System;
using Mesher.Core.Plugins;
using Mesher.Core.Renderers;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Data.OpenGL;

namespace Mesher.Plugins.EditLight
{
    public class EditLight : IPlugin
    {
        private MainForm m_form;

        public String Name { get; }

        public EditLight()
        {
            Name = @"Edit light";
        }

        public void Execute(MesherGraphics graphics)
        {
           // m_form = new MainForm(factory, scene);
            m_form.Show();
        }

	    public void Render()
	    {
		    throw new NotImplementedException();
	    }

	    public Boolean Enabled { get; }
    }
}
