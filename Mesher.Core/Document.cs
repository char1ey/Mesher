using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Core.Data;
using Mesher.Core.Events;
using Mesher.Core.Events.EventArgs;
using Mesher.Core.Objects;

namespace Mesher.Core
{
	public class Document
	{
	    private MesherApplication m_mesherApplication;
        public Scene Scene { get; set; }

	    public event OnBeforeRender BeforeRender;
	    public event OnAfterRender AfterRender;

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

	            var args = new RenderEventArgs(m_mesherApplication.Graphics, documentView);
                BeforeRender?.Invoke(this, args);

	            Scene.Render(documentView);

                AfterRender?.Invoke(this, args);

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
