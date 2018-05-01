using System.Collections.Generic;
using Mesher.GraphicsCore.Collections;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.Light;

namespace Mesher.GraphicsCore.Primitives
{
    public class RScene
    {
        private IDataFactory m_dataFactory;

        private List<RPrimitive> m_primitives;
	    public List<RLight> m_lights;
		
        public List<RPrimitive> Primitives
        {
            get { return m_primitives; }
        }

		public List<RLight> Lights { get { return m_lights; } }

        public RScene(IDataFactory dataFactory)
        {
            m_dataFactory = dataFactory;
            m_primitives = new List<RPrimitive>();
            m_lights = new List<RLight>();
        }

	    public RLight AddLight()
	    {
		    var light = new RLight(m_dataFactory, this);
			m_lights.Add(light);
		    return light;
	    }

        public RTriangles AddTriangles()
        {
            var triangles = new RTriangles(m_dataFactory, this);
            m_primitives.Add(triangles);
            return triangles;
        }

        public REdges AddEdges()
        {
            var edges = new REdges(m_dataFactory, this);
            m_primitives.Add(edges);
            return edges;
        }

        public RGlyphs AddGlyphs()
        {
            var glyphs = new RGlyphs(m_dataFactory, this);
            m_primitives.Add(glyphs);
            return glyphs;
        }
    }
}