using System;
using Mesher.GraphicsCore.Camera;
using Mesher.GraphicsCore.Light;

namespace Mesher.GraphicsCore.Objects
{
    public class Scene : IDisposable
    {
		public Cameras Cameras { get; }

		public Meshes Meshes { get;}

		public Lights Lights { get; }

        public Scene()
        {
            Cameras = new Cameras();
            Meshes = new Meshes();
            Lights = new Lights();
        }

        public void Dispose()
        {

        }
    }
}
