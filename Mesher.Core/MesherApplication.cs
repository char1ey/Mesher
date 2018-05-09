using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Core.Test;

namespace Mesher.Core
{
    public class MesherApplication
    {
        public Document Document { get; set; }
        public MainWindow MainWindow { get; private set; }

        public List<Plugins.IPlugin> Plugins { get; private set; }

        public MesherApplication()
        {
            MainWindow = new MainWindow();
        }
    }
}
