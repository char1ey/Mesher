using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Core.Objects.Camera;
using Mesher.Core.Objects.Scene;
using Mesher.GraphicsCore;

namespace Mesher.Core
{
    public class SceneForm
    {
        private RenderContext m_renderContext;

        public Scene Scene { get; set; }
        public Camera Camera { get; set; }

        public SceneForm(RenderContext renderContext)
        {
            m_renderContext = renderContext;
        }
    }
}
