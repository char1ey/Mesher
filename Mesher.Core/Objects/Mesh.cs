using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Primitives;

namespace Mesher.Core.Objects
{
    public class Mesh : IDocumentItem
    {
        private MesherGraphics m_graphics;

        public RTriangles RTriangles { get; private set; }

        public Mesh(MesherGraphics graphics)
        {
            RTriangles = graphics.CreateRTriangles();
            m_graphics = graphics;
        }

        public void Rebuild()
        {
            throw new NotImplementedException();
        }
    }
}
