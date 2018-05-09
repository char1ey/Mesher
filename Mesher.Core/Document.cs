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
        public Scene Scene { get; set; }

	    public void Rebuild()
	    {

	    }

	    public void Render()
	    {

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
