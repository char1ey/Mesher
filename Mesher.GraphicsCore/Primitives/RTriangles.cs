using System;
using Mesher.GraphicsCore.Buffers;
using Mesher.GraphicsCore.Collections;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.GraphicsCore.Renderers;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Primitives
{
    public class RTriangles : RPrimitive
    {
        public Boolean HasTextureVertexes { get; set; }
        public IDataBuffer<Vec2> TexCoords { get; set; }

        public Boolean HasTangentBasis { get; set; }
        public IDataBuffer<Vec3> Tangents { get; set; }
        public IDataBuffer<Vec3> BiTangents { get; set; }

        public Boolean HasNormals { get; set; }
        public IDataBuffer<Vec3> Normals { get; set; }

        public Boolean HasMaterial { get; set; }
        public Material.RMaterial Material { get; set; }

        internal RTriangles(IDataFactory dataFactory) : base(dataFactory)
        {
            TexCoords = dataFactory.CreateDataBuffer<Vec2>();
            Normals = dataFactory.CreateDataBuffer<Vec3>();
            Tangents = dataFactory.CreateDataBuffer<Vec3>();
            BiTangents = dataFactory.CreateDataBuffer<Vec3>();
        }

        public override void Render(RenderersFactory renderersFactory, RLights rLights, IRenderContext renderContext)
        {
            renderersFactory.TrianglesRenderer.Render(this, rLights, renderContext);
        }
    }
}