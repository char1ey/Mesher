using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Core.Data;
using Mesher.Core.Objects;
using Mesher.GraphicsCore;

namespace Mesher.Core
{
	public class Document
	{
	    private MesherApplication m_mesherApplication;
        public Scene Scene { get; set; }

	    public Document(MesherApplication application)
	    {
	        m_mesherApplication = application;
            Scene = new Scene(m_mesherApplication.Graphics);
	    }

	    public void Rebuild()
	    {
            Scene.Rebuild();
	    }

	    public void Render()
	    {
	        foreach (var documentView in m_mesherApplication.DocumentViews)
	        {
	            documentView.BeginRender();
	            Scene.Render(documentView);
	            documentView.EndRender();
	        }
	    }

	    public void Save(String path)
	    {
	        throw new NotImplementedException();
	    }

	    public static Document Load(String fileName, MesherApplication application)
	    {
	        return DataLoaderPrototype.LoadDocument(fileName, application);
	    }
	}
}
