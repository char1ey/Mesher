using System;
using Mesher.GraphicsCore.Buffers;
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

        public RTriangles(IDataContext dataContext, RScene scene) : base(dataContext, scene)
        {
            TexCoords = dataContext.CreateDataBuffer<Vec2>();
            Normals = dataContext.CreateDataBuffer<Vec3>();
            Tangents = dataContext.CreateDataBuffer<Vec3>();
            BiTangents = dataContext.CreateDataBuffer<Vec3>();
        }

        public override void Render(RSceneRenderer sceneRenderer, IRenderContext renderContext)
        {
            sceneRenderer.TrianglesRenderer.Render(this, renderContext);
        }
    }
}