using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using Mesher.Core.Collections;
using Mesher.GraphicsCore;

namespace Mesher.Core.Objects
{
    public class Scene : IDocumentItem
    {
        private MesherGraphics m_graphics;
        public Lights Lights { get; private set; }
        public List<Mesh> Meshes { get; private set; }

        public Scene(MesherGraphics graphics)
        {
            Meshes = new List<Mesh>();
            Lights = new Lights();
        }

        public Mesh AddMesh()
        {
            var mesh = new Mesh(this, m_graphics);
            Meshes.Add(mesh);
            return mesh;
        }

        public void Rebuild()
        {
            throw new NotImplementedException();
        }

        public void Render(ISceneContext sceneContext)
        {
            foreach (var mesh in Meshes)
                mesh.Render(sceneContext);
        }
    }
}
