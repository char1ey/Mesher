using System;
using Mesher.Core.Components;
using Mesher.GraphicsCore.Buffers;
using Mesher.GraphicsCore.ShaderProgram;
using Mesher.Mathematics;

namespace Mesher.Core.Objects.Mesh
{
    public class Mesh : IRenderItem, IDisposable
    {
        public Int32 Id { get; internal set; }
        
        public VertexBuffer<Vec3> Vertexes { get; set; }
        
        public VertexBuffer<Vec3> Normals { get; set; }
        
        public VertexBuffer<Vec2> TextureVertexes { get; set; }
        
        public VertexBuffer<Vec3> Tangents { get; set; }
        
        public VertexBuffer<Vec3> BiTangents { get; set; }

        public Boolean HasMaterial { get; set; }
        
        public Material.Material Material { get; set; }
        
        public IndexBuffer Indicies { get; set; }

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

        public void Render(SceneContext sceneContext, ShaderProgram shaderProgram)
        {
            shaderProgram.SetValue("hasPosition", HasVertexes);
            if (HasVertexes)
                shaderProgram.SetBuffer("position", Vertexes, 3);

            shaderProgram.SetValue("hasNormal", HasNormals);
            if (HasNormals)
                shaderProgram.SetBuffer("normal", Normals, 3);

            shaderProgram.SetValue("hasTexCoord", HasTextureVertexes);
            if (HasTextureVertexes)
                shaderProgram.SetBuffer("texCoord", TextureVertexes, 2);

            shaderProgram.SetValue("hasTangentBasis", HasTangentBasis);
            if (HasTangentBasis)
            {
                shaderProgram.SetBuffer("tangent", Tangents, 3);
                shaderProgram.SetBuffer("biTangent", BiTangents, 3);
            }

            if (HasMaterial)
                Material.Render(sceneContext, shaderProgram);
        }
    }
}