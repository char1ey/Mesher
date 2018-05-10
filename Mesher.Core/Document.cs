using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Core.Objects;

namespace Mesher.Core
{
	public class Document
	{
        public List<Camera> Cameras { get; private set; }
        public Scene Scene { get; set; }

	    public Document()
	    {
	        Cameras = new List<Camera>();
	    }

	    public void Rebuild()
	    {

	    }

	    public void Render()
	    {
            foreach(var camera in Cameras)
                Scene.Render(camera.SceneContext);
	    }

	    public void Save(String path)
	    {
	        throw new NotImplementedException();
	    }

	    public void Load(String fileName)
	    {
	        throw new NotImplementedException();
	    }
	}
}
