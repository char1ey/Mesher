using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Core.Plugins;
using System.Windows.Forms;
using Mesher.Core.Objects;
using Mesher.Core.Objects.Scene;
using Mesher.GraphicsCore;

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

        public void Execute(RenderContext context, Scene scene)
        {
            m_form = new MainForm(context, scene);
            m_form.Show();
        }
    }
}
