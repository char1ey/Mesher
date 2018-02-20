using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesher.Core.Plugins
{
    public interface IPlugin
    {
        String Name { get; }
        void Execute();
    }
}
