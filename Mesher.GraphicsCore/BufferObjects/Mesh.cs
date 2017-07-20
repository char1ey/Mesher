using System;
using System.Collections.Generic;
using System.Drawing;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.BufferObjects
{
    public class Mesh: IBufferObject
    {
        private Triangle[] m_triangles;
        private Triangle[] m_textureTriangles;
        
        private readonly uint[] m_vbo;

        public Mesh(Triangle[] triangles, Triangle[] textureTriangles, Texture.Texture texture)
        {
            m_triangles = triangles;

            m_vbo = new uint[1];

            var vertexesData = new float[9 * triangles.Length];

            for (var i = 0; i < triangles.Length; i++)
            {
                vertexesData[9 * i + 0] = (float)triangles[i].A.X;
                vertexesData[9 * i + 1] = (float)triangles[i].A.Y;
                vertexesData[9 * i + 2] = (float)triangles[i].A.Z;
                vertexesData[9 * i + 3] = (float)triangles[i].B.X;
                vertexesData[9 * i + 4] = (float)triangles[i].B.Y;
                vertexesData[9 * i + 5] = (float)triangles[i].B.Z;
                vertexesData[9 * i + 6] = (float)triangles[i].C.X;
                vertexesData[9 * i + 7] = (float)triangles[i].C.Y;
                vertexesData[9 * i + 8] = (float)triangles[i].C.Z;
            }

            Gl.GenBuffers(1, m_vbo);
            Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, m_vbo[0]);
            Gl.BufferData(Gl.GL_ARRAY_BUFFER, vertexesData, Gl.GL_STATIC_DRAW);
        }

        public void Render()
        {
            Gl.EnableVertexAttribArray(0);
            Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, m_vbo[0]);
            Gl.VertexAttribPointer(0, 3, Gl.GL_FLOAT, false, 0, IntPtr.Zero);
            Gl.PolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);
            Gl.Color(Color.Black);
            Gl.DrawArrays(Gl.GL_TRIANGLES, 0, 3 * m_triangles.Length);
            Gl.DisableVertexAttribArray(0);
        }
    }
}
