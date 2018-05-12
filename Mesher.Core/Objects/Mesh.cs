using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.Primitives;
using Mesher.Mathematics;

namespace Mesher.Core.Objects
{
    public class Mesh : IDocumentItem
    {
        private MesherGraphics m_graphics;

        public Scene Scene { get; private set; }
        public RTriangles RTriangles { get; private set; }

        public List<Vec3> Positions { get; set; }

        public Boolean HasTextureVertexes { get; set; }
        public List<Vec2> TexCoords { get; set; }

        public Boolean HasTangentBasis { get; set; }
        public List<Vec3> Tangents { get; set; }
        public List<Vec3> BiTangents { get; set; }

        public Boolean HasNormals { get; set; }
        public List<Vec3> Normals { get; set; }

        public Boolean HasMaterial { get; set; }
        public Material Material { get; set; }

        public Boolean IndexedRendering { get; set; }
        public List<Int32> Indexes { get; private set; }

        public Mesh(Scene scene, MesherGraphics graphics)
        {
            RTriangles = graphics.CreateRTriangles();
            m_graphics = graphics;
            Scene = scene;

            Positions = new List<Vec3>();
            TexCoords = new List<Vec2>();
            Tangents = new List<Vec3>();
            BiTangents = new List<Vec3>();
            Normals = new List<Vec3>();
            Indexes = new List<Int32>();
        }

        public void Rebuild()
        {
            RTriangles.Positions.Clear();
            RTriangles.Positions.AddRange(Positions.ToArray());

            RTriangles.HasTextureVertexes = HasTextureVertexes;
            if (HasTextureVertexes)
            {
                RTriangles.TexCoords.Clear();
                RTriangles.TexCoords.AddRange(TexCoords.ToArray());
            }

            RTriangles.HasTangentBasis = HasTangentBasis;
            if (HasTangentBasis)
            {
                RTriangles.Tangents.Clear();
                RTriangles.Tangents.AddRange(Tangents.ToArray());

                RTriangles.BiTangents.Clear();
                RTriangles.BiTangents.AddRange(BiTangents.ToArray());
            }

            RTriangles.HasNormals = HasNormals;
            if (HasNormals)
            {
                RTriangles.Normals.Clear();
                RTriangles.Normals.AddRange(Normals.ToArray());
            }

            RTriangles.IndexedRendering = IndexedRendering;
            if (IndexedRendering)
            {
                RTriangles.Indexes.Clear();
                RTriangles.Indexes.AddRange(Indexes);
            }

            Material.Rebuild();

            RTriangles.HasMaterial = HasMaterial;
            if (HasMaterial)
                RTriangles.Material = Material.RMaterial;
        }

        public void Render(IDocumentView documentView)
        {
            m_graphics.RenderersFactory.TrianglesRenderer.Render(RTriangles, Scene.Lights.RLights, documentView.RenderContext);
        }
    }
}
