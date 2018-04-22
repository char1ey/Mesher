using System.Collections.Generic;
using Mesher.GraphicsCore.Collections;
using Mesher.GraphicsCore.Data;

namespace Mesher.GraphicsCore.Primitives
{
    public class RScene
    {
        private IDataContext m_dataContext;

        private List<RPrimitive> m_primitives;
        public Lights Lights { get; private set; }

        public List<RPrimitive> Primitives
        {
            get { return m_primitives; }
        }

        public RScene(IDataContext dataContext)
        {
            m_dataContext = dataContext;
            Lights = new Lights();
        }

        public RTriangles AddTriangles()
        {
            var triangles = new RTriangles(m_dataContext, this);
            m_primitives.Add(triangles);
            return triangles;
        }

        public REdges AddEdges()
        {
            var edges = new REdges(m_dataContext, this);
            m_primitives.Add(edges);
            return edges;
        }

        public RGlyphs AddGlyphs()
        {
            var glyphs = new RGlyphs(m_dataContext, this);
            m_primitives.Add(glyphs);
            return glyphs;
        }
    }
}