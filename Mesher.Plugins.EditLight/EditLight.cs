using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Core.Plugins;
using System.Windows.Forms;

namespace Mesher.Plugins.EditLight
{
    public class EditLight : IPlugin
    {
        public String Name { get; }

        public EditLight()
        {
            Name = @"Edit light";
        }

        public void Execute()
        {
            MessageBox.Show(@"Hello plugin!");
        }
    }
}
