using System;
using Mesher.Core;
using Mesher.Core.Plugins;
using Mesher.Core.Renderers;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Data.OpenGL;

namespace Mesher.Plugins.EditLight
{
    public class EditLight : Plugin
    {
        private MainForm m_form;

        public EditLight(MesherApplication mesherApplication) : base(mesherApplication)
        {
            Name = @"Edit light";
        }

        public override void Execute()
        {
           // m_form = new MainForm(factory, scene);
            m_form.Show();
        }
    }
}
