using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.GraphicsCore.BufferObjects;
using Mesher.GraphicsCore.Light;

namespace Mesher.Document
{
    public class Scene
    {
        public List<Light> Lights { get; set; }
        public Mesh Mesh { get; set; }

        public Scene()
        {
            Lights = new List<Light>();
        }

        public Scene(Mesh mesh) : this()
        {
            Mesh = mesh;            
        }

        public Scene(Mesh mesh, List<Light> lights)
        {
            Mesh = mesh;
            Lights = lights ?? new List<Light>();
        }
    }
}
