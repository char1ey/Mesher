using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Core.Plugins;
using System.Windows.Forms;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Objects;

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

        public void Execute(RenderManager manager, Scene scene)
        {
            m_form = new MainForm(manager, scene);
            m_form.Show();
        }
    }
}
