using System;
using Mesher.Core.Camera;
using Mesher.Core.Light;

namespace Mesher.Core.Objects
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
