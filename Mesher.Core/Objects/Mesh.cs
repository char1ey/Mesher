using System;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Buffers;
using Mesher.Mathematics;

namespace Mesher.Core.Objects
{
    public class Mesh : IDisposable
    {
        public Int32 Id { get; internal set; }

        [ShaderProgramMember(Name = "position")]
        public VertexBuffer<Vec3> Vertexes { get; set; }

        [ShaderProgramMember(Name = "normal")]
        public VertexBuffer<Vec3> Normals { get; set; }

        [ShaderProgramMember(Name = "texCoord")]
        public VertexBuffer<Vec2> TextureVertexes { get; set; }

        [ShaderProgramMember(Name = "tangent")]
        public VertexBuffer<Vec3> Tangents { get; set; }

        [ShaderProgramMember(Name = "biTangent")]
        public VertexBuffer<Vec3> BiTangents { get; set; }

        public Boolean HasMaterial { get; set; }

        [ShaderProgramMember(Name = "material")]
        public Material.Material Material { get; set; }

        [ShaderProgramMember]
        public IndexBuffer Indicies { get; set; }

        public Boolean IndexedRendering { get; set; }

        [ShaderProgramMember(Name = "hasPosition")]
        public Boolean HasVertexes { get; set; }

        [ShaderProgramMember(Name = "hasNormal")]
        public Boolean HasNormals { get; set; }

        [ShaderProgramMember(Name = "hasTexCoord")]
        public Boolean HasTextureVertexes { get; set; }

        [ShaderProgramMember(Name = "hasTangentBasis")]
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