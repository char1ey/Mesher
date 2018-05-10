using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Primitives;

namespace Mesher.Core.Objects
{
    public class Mesh : IDocumentItem
    {
        private MesherGraphics m_graphics;

        public Scene Scene { get; private set; }
        public RTriangles RTriangles { get; private set; }

        public Mesh(Scene scene, MesherGraphics graphics)
        {
            RTriangles = graphics.CreateRTriangles();
            m_graphics = graphics;
            Scene = scene;
        }

        public void Rebuild()
        {
            throw new NotImplementedException();
        }

        public void Render(ISceneContext sceneContext)
        {
            m_graphics.RenderersFactory.TrianglesRenderer.Render(RTriangles, Scene.Lights.RLights, sceneContext.RenderContext);
        }
    }
}
