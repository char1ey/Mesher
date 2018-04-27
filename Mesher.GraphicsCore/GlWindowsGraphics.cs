using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.Data.OpenGL;
using Mesher.GraphicsCore.RenderContexts;

namespace Mesher.GraphicsCore
{
    public class GlWindowsGraphics : MesherGraphics
    {
        public override IDataContext DataContext { get; }

        public GlWindowsGraphics(GlWindowsRenderContext renderContext)
        {
            var dataContext = new GlDataContext(renderContext);
            renderContext.DataContext = dataContext;
            DataContext = dataContext;
        }

        public override IRenderContext CreateRenderContext(IntPtr handle)
        {
            return new GlWindowsRenderContext(handle);
        }
    }
}
