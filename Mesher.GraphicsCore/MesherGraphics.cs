using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.RenderContexts;

namespace Mesher.GraphicsCore
{
    public abstract class MesherGraphics
    {
        public abstract IDataContext DataContext { get; }

        public abstract IRenderContext CreateRenderContext(IntPtr handle);
    }
}
