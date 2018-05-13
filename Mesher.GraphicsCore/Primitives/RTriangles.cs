using System;
using Mesher.GraphicsCore.Camera;
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

        internal RTriangles(IDataContext dataContext) : base(dataContext)
        {
            TexCoords = dataContext.CreateDataBuffer<Vec2>();
            Normals = dataContext.CreateDataBuffer<Vec3>();
            Tangents = dataContext.CreateDataBuffer<Vec3>();
            BiTangents = dataContext.CreateDataBuffer<Vec3>();
        }

        public override void Render(RenderersFactory renderersFactory, RenderArgs renderArgs)
        {
            renderersFactory.TrianglesRenderer.Render(this, renderArgs);
        }

        public override void Dispose()
        {
            base.Dispose();
            TexCoords?.Dispose();
            Normals?.Dispose();
            Tangents?.Dispose();
            BiTangents.Dispose();
        }
    }
}