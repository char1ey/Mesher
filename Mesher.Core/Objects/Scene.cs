using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using Mesher.Core.Collections;

namespace Mesher.Core.Objects
{
    public class Scene : IDocumentItem
    {
        public Lights Lights { get; private set; }
        public List<Mesh> Meshes { get; private set; }
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
