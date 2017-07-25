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

        public Mesh(Vertex[] vertexes, Vertex[] textureVertexes, Vertex[] normals, int[] indicies, Texture.Texture texture)
        {
            m_texture = texture;

            m_vao = new uint[1];         

            Gl.GenVertexArrays(1, m_vao);
            Gl.BindVertexArray(m_vao[0]);

            m_elementsCount = indicies.Length;

            if (vertexes != null)
            {
                m_vboVert = new uint[1]; 
                
                var vertexesData = new float[vertexes.Length * 3];

                for (var i = 0; i < vertexes.Length; i++)
                {
                    vertexesData[3 * i + 0] = (float)vertexes[i].X;
                    vertexesData[3 * i + 1] = (float)vertexes[i].Y;
                    vertexesData[3 * i + 2] = (float)vertexes[i].Z;
                }

                Gl.GenBuffers(1, m_vboVert);
                Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, m_vboVert[0]);
                Gl.BufferData(Gl.GL_ARRAY_BUFFER, vertexesData, Gl.GL_STATIC_DRAW);
                Gl.EnableClientState(Gl.GL_VERTEX_ARRAY);
                Gl.VertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
                Gl.DisableClientState(Gl.GL_VERTEX_ARRAY);
            }

            if (normals != null)
            {
                m_vboNormals = new uint[1];

                var normalsData = new float[normals.Length * 3];

                for (var i = 0; i < normals.Length; i++)
                {
                    normalsData[3 * i + 0] = (float)normals[i].X;
                    normalsData[3 * i + 1] = (float)normals[i].Y;
                    normalsData[3 * i + 2] = (float)normals[i].Z;
                }

                Gl.GenBuffers(1, m_vboNormals);
                Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, m_vboNormals[0]);
                Gl.BufferData(Gl.GL_ARRAY_BUFFER, normalsData, Gl.GL_STATIC_DRAW);
                Gl.EnableClientState(Gl.GL_NORMAL_ARRAY);
                Gl.VertexPointer(3, Gl.GL_FLOAT, 0, IntPtr.Zero);
                Gl.DisableClientState(Gl.GL_NORMAL_ARRAY);
            }

            if (textureVertexes != null)
            {
                m_vboTexVert = new uint[1];

                var textureVertexesData = new float[textureVertexes.Length * 2];

                for (var i = 0; i < textureVertexes.Length; i++)
                {
                    textureVertexesData[2 * i + 0] = (float) textureVertexes[i].X;
                    textureVertexesData[2 * i + 1] = (float) textureVertexes[i].Y;
                }

                Gl.GenBuffers(1, m_vboTexVert);
                Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, m_vboTexVert[0]);
                Gl.BufferData(Gl.GL_ARRAY_BUFFER, textureVertexesData, Gl.GL_STATIC_DRAW);
                Gl.EnableClientState(Gl.GL_TEXTURE_COORD_ARRAY);
                Gl.TexCoordPointer(2, Gl.GL_FLOAT, 0, IntPtr.Zero);
                Gl.DisableClientState(Gl.GL_TEXTURE_COORD_ARRAY);
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
            m_texture.Activate();

            Gl.BindVertexArray(m_vao[0]);

            Gl.Color(Color.Transparent);

            Gl.EnableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.EnableClientState(Gl.GL_TEXTURE_COORD_ARRAY);

            Gl.DrawElements(Gl.GL_TRIANGLES, m_elementsCount, Gl.GL_UNSIGNED_INT, IntPtr.Zero);

            Gl.DisableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.DisableClientState(Gl.GL_TEXTURE_COORD_ARRAY);

            m_texture.Deactivate();
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
