using System;
using Mesher.GraphicsCore.Buffers;
using Mesher.GraphicsCore.Data.OpenGL;
using Mesher.GraphicsCore.Material;
using Mesher.Mathematics;

namespace Mesher.Core.Objects.Mesh
{
    public class Mesh : IDisposable
    {
        public Int32 Id { get; internal set; }
        
        public GlDataBuffer<Vec3> Vertexes { get; set; }
        
        public GlDataBuffer<Vec3> Normals { get; set; }
        
        public GlDataBuffer<Vec2> TextureVertexes { get; set; }
        
        public GlDataBuffer<Vec3> Tangents { get; set; }
        
        public GlDataBuffer<Vec3> BiTangents { get; set; }

        public Boolean HasMaterial { get; set; }
        
        public Material Material { get; set; }
        
        public GlIndexBuffer Indicies { get; set; }

        public Boolean IndexedRendering { get; set; }
        
        public Boolean HasVertexes { get; set; }
        
        public Boolean HasNormals { get; set; }

        public Boolean HasTextureVertexes { get; set; }
        
        public Boolean HasTangentBasis { get; set; }

        public void Dispose()
        {
            Vertexes?.Dispose();
            Normals?.Dispose();
            TextureVertexes?.Dispose();
            Tangents?.Dispose();
            BiTangents?.Dispose();
            Material?.Dispose();
        }
    }
}