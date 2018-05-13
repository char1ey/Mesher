using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Core.Events.EventArgs;

namespace Mesher.Core.Events
{
    public delegate void OnBeforeRender(object sender, RenderEventArgs args);

    public delegate void OnAfterRender(object sender, RenderEventArgs args);

}
