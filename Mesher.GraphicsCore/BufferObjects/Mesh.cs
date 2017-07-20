using System;
using System.Collections.Generic;
using System.Drawing;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.BufferObjects
{
    public class Mesh: IBufferObject
    {
        private readonly uint[] m_vao;
        private readonly uint[] m_vbo;
        private readonly uint[] m_ebo;

        private int count;

        private Texture.Texture m_texture;

        public Mesh(Vertex[] vertexes, Vertex[] textureVertexes, Vertex[] normals, int[,] indicies, Texture.Texture texture)
        {
            m_texture = texture;

            m_vao = new uint[1];
            m_vbo = new uint[1];
            m_ebo = new uint[1];

            var size = 3;

            if (textureVertexes != null)
                size += 2;

            if (normals != null)
                size += 3;

            var vertexesData = new float[size * vertexes.Length];

            for (var i = 0; i < vertexes.Length; i++)
            {
                vertexesData[i * size + 0] = (float)vertexes[i].X;
                vertexesData[i * size + 1] = (float)vertexes[i].Y;
                vertexesData[i * size + 2] = (float)vertexes[i].Z;

                if (textureVertexes != null)
                {
                    vertexesData[i * size + 3] = (float) textureVertexes[i].X;
                    vertexesData[i * size + 4] = (float) textureVertexes[i].Y;
                }

                if (normals != null)
                {
                    var shift = textureVertexes == null ? 3 : 5;
                    vertexesData[i * size + shift + 0] = (float)normals[i].X;
                    vertexesData[i * size + shift + 1] = (float)normals[i].Y;
                    vertexesData[i * size + shift + 2] = (float)normals[i].Z;
                }
            }

            var indiciesData = new int[3 * indicies.GetLength(0)];

            for (var i = 0; i < indiciesData.Length; i++)
                indiciesData[i] = indicies[i / 3, i % 3];
            count = indiciesData.Length;

            Gl.GenVertexArrays(1, m_vao);
            Gl.GenBuffers(1, m_vbo);
            Gl.GenBuffers(1, m_ebo);

            Gl.BindVertexArray(m_vao[0]);
            
            Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, m_vbo[0]);

            Gl.BufferData(Gl.GL_ARRAY_BUFFER, vertexesData, Gl.GL_STATIC_DRAW);

            Gl.BindBuffer(Gl.GL_ELEMENT_ARRAY_BUFFER, m_ebo[0]);
            Gl.BufferData(Gl.GL_ELEMENT_ARRAY_BUFFER, indiciesData, Gl.GL_STATIC_DRAW);

            Gl.EnableVertexAttribArray(0);
            Gl.VertexAttribPointer(0, 3, Gl.GL_FLOAT, false, size, IntPtr.Zero);

            if (textureVertexes != null)
            {
                Gl.EnableVertexAttribArray(1);
                Gl.VertexAttribPointer(1, 2, Gl.GL_FLOAT, false, size, IntPtr.Zero + 3);
            }

            if (normals != null)
            {
                uint shift, location;

                if (textureVertexes == null)
                {
                    shift = 3;
                    location = 1;
                }
                else
                {
                    shift = 5;
                    location = 2;
                }

                Gl.EnableVertexAttribArray(location);
                Gl.VertexAttribPointer(location, 3, Gl.GL_FLOAT, false, size, IntPtr.Zero + (int) shift);
            }

            Gl.BindVertexArray(0);
        }

        public void Render()
        {
            m_texture.Activate();
            Gl.BindVertexArray(m_vao[0]);
            Gl.DrawElements(Gl.GL_TRIANGLES, count, Gl.GL_INT, IntPtr.Zero);
            Gl.BindVertexArray(0);
            m_texture.Deactivate();
        }
    }
}
