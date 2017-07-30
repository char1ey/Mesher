using System;
using System.Drawing;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.BufferObjects
{
    public class Mesh : IBufferObject, IDisposable
    {
        private readonly uint[] m_vao;
        private readonly uint[] m_ebo;
        private readonly uint[] m_vboVert;
        private readonly uint[] m_vboNormals;
        private readonly uint[] m_vboTexVert;

        private readonly int m_elementsCount;

        private readonly Texture.Texture m_texture;

        public Mesh(Vec3[] vertexes, Vec2[] textureVertexes, Vec3[] normals, int[] indicies, Texture.Texture texture)
        {
            m_texture = texture;

            m_vao = new uint[1];         

            Gl.GenVertexArrays(1, m_vao);
            Gl.BindVertexArray(m_vao[0]);

            m_elementsCount = indicies.Length;

            if (vertexes != null)
            {
                m_vboVert = new uint[1]; 
                Gl.GenBuffers(1, m_vboVert);
                Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, m_vboVert[0]);
                Gl.BufferData(Gl.GL_ARRAY_BUFFER, vertexes, Gl.GL_STATIC_DRAW);
                Gl.VertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
            }

            if (normals != null)
            {
                m_vboNormals = new uint[1];
                Gl.GenBuffers(1, m_vboNormals);
                Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, m_vboNormals[0]);
                Gl.BufferData(Gl.GL_ARRAY_BUFFER, normals, Gl.GL_STATIC_DRAW);
                Gl.VertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
            }

            if (textureVertexes != null)
            {
                m_vboTexVert = new uint[1];
                Gl.GenBuffers(1, m_vboTexVert);
                Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, m_vboTexVert[0]);
                Gl.BufferData(Gl.GL_ARRAY_BUFFER, textureVertexes, Gl.GL_STATIC_DRAW);
                Gl.TexCoordPointer(2, Gl.GL_FLOAT, 0, IntPtr.Zero);
            }

            m_ebo = new uint[1];
            Gl.GenBuffers(1, m_ebo);
            Gl.BindBuffer(Gl.GL_ELEMENT_ARRAY_BUFFER, m_ebo[0]);
            Gl.BufferData(Gl.GL_ELEMENT_ARRAY_BUFFER, indicies, Gl.GL_STATIC_DRAW);

            Gl.BindVertexArray(0);
            Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, 0);
            Gl.BindBuffer(Gl.GL_ELEMENT_ARRAY_BUFFER, 0);
        }

        public void Render()
        {
            m_texture?.Activate();

            Gl.BindVertexArray(m_vao[0]);

            Gl.Color(Color.Transparent);

            if(m_vboNormals != null)
                Gl.EnableClientState(Gl.GL_NORMAL_ARRAY);
            if(m_vboVert != null)
                Gl.EnableClientState(Gl.GL_VERTEX_ARRAY);
            if(m_vboTexVert != null)
                Gl.EnableClientState(Gl.GL_TEXTURE_COORD_ARRAY);

            Gl.DrawElements(Gl.GL_TRIANGLES, m_elementsCount, Gl.GL_UNSIGNED_INT, IntPtr.Zero);

            if (m_vboNormals != null)
                Gl.DisableClientState(Gl.GL_NORMAL_ARRAY);
            if (m_vboVert != null)
                Gl.DisableClientState(Gl.GL_VERTEX_ARRAY);
            if (m_vboTexVert != null)
                Gl.DisableClientState(Gl.GL_TEXTURE_COORD_ARRAY);

            m_texture?.Deactivate();
        }

        public void Dispose()
        {
            if(m_vboVert != null)
                Gl.DeleteBuffers(1, m_vboVert);
            if(m_vboNormals != null)
                Gl.DeleteBuffers(1, m_vboNormals);
            if(m_vboTexVert != null)
                Gl.DeleteBuffers(1, m_vboTexVert);

            Gl.DeleteBuffers(1, m_ebo);
            Gl.DeleteVertexArrays(1, m_vao);
        }
    }
}
