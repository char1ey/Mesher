using System;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Buffers;
using Mesher.Mathematics;

namespace Mesher.Core.Objects
{
    public class Mesh : IDisposable
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

        internal void Render()
        {
            if (IndexedRendering && Indicies != null)
            {
                Indicies.Bind();
                Gl.DrawElements(Gl.GL_TRIANGLES, Indicies.Count, Gl.GL_UNSIGNED_INT, IntPtr.Zero);
            }
            else Gl.DrawArrays(Gl.GL_TRIANGLES, 0, Vertexes.Count);
        }

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