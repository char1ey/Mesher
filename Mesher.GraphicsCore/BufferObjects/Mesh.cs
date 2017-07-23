using System;
using System.Drawing;
using Mesher.GraphicsCore.Shaders;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.BufferObjects
{
    public class Mesh: IBufferObject
    {
        private static ShaderProgram s_shaderProgram;

        private static ShaderProgram ShaderProgram
        {
            get
            {
                if (s_shaderProgram == null)
                    s_shaderProgram = new ShaderProgram(@"
                            #version 330
                            layout(location = 0) in vec3 vert;
                            layout(location = 1) in vec2 vertTexCoord;
                            out vec2 fragTexCoord;
    
                            uniform mat4 projectionMatrix;
                            uniform mat4 modelviewMatrix;

                            void main()
                            {
                                fragTexCoord = vertTexCoord;

                                gl_Position = projectionMatrix * modelviewMatrix * vec4(vert.xyz, 1);
                            }
                        ",
                        @"
                            #version 330
                            uniform sampler2D tex; 
                            in vec2 fragTexCoord;
                            out vec4 finalColor;

                            void main() {
                                finalColor = texture2D(tex, fragTexCoord);
                            }
                        ");

                return s_shaderProgram;
            }
        }

        private readonly uint[] m_vao;
        private readonly uint[] m_vboNormals;

        private readonly uint[] m_ebo;

        private int m_count;

        private Texture.Texture m_texture;

        public Mesh(Vertex[] vertexes, Vertex[] textureVertexes, Vertex[] normals, int[,] indicies, Texture.Texture texture)
        {
            m_texture = texture;

            var indiciesData = new int[3 * indicies.GetLength(0)];
            for (var i = 0; i < indiciesData.Length; i++)
                indiciesData[i] = indicies[i / 3, i % 3];

            m_count = indiciesData.Length;
            var vertexesData = new float[vertexes.Length * 3];

            for (var i = 0; i < vertexes.Length; i++)
            {
                vertexesData[3 * i + 0] = (float)vertexes[i].X;
                vertexesData[3 * i + 1] = (float)vertexes[i].Y;
                vertexesData[3 * i + 2] = (float)vertexes[i].Z;
            }

            var textureVertexesData = new float[textureVertexes.Length * 2];

            for (var i = 0; i < vertexes.Length; i++)
            {
                textureVertexesData[2 * i + 0] = (float)textureVertexes[i].X;
                textureVertexesData[2 * i + 1] = (float)textureVertexes[i].Y;
            }

            ShaderProgram.Enable();

            m_vao = new uint[1];
            var vboVertexes = new uint[1];
            var vboTextureVertexes = new uint[1];
            m_ebo = new uint[1];

            Gl.GenVertexArrays(1, m_vao); 
            Gl.BindVertexArray(m_vao[0]);

            Gl.GenBuffers(1, vboVertexes); 
            Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, vboVertexes[0]); 
            Gl.BufferData(Gl.GL_ARRAY_BUFFER, vertexesData, Gl.GL_STATIC_DRAW);           
            Gl.EnableVertexAttribArray(0);       
            Gl.VertexAttribPointer(0, 3, Gl.GL_FLOAT, false, 0, IntPtr.Zero);
            
            Gl.GenBuffers(1, vboTextureVertexes); 
            Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, vboTextureVertexes[0]); 
            Gl.BufferData(Gl.GL_ARRAY_BUFFER, textureVertexesData, Gl.GL_STATIC_DRAW);
            Gl.EnableVertexAttribArray(1);
            Gl.VertexAttribPointer(1, 2, Gl.GL_FLOAT, false, 0, IntPtr.Zero);

            Gl.GenBuffers(1, m_ebo);
            Gl.BindBuffer(Gl.GL_ELEMENT_ARRAY_BUFFER, m_ebo[0]);
            Gl.BufferData(Gl.GL_ELEMENT_ARRAY_BUFFER, indiciesData, Gl.GL_STATIC_DRAW);
           
            Gl.BindVertexArray(0);
            Gl.BindBuffer(Gl.GL_ARRAY_BUFFER, 0);
            Gl.BindBuffer(Gl.GL_ELEMENT_ARRAY_BUFFER, 0);

            ShaderProgram.Disable();
        }

        public void Render()
        {
            ShaderProgram.Enable();
            m_texture.Activate();

            var projectionMatrix = new float[16];
            Gl.GetFloat(Gl.GL_PROJECTION_MATRIX, projectionMatrix);        

            var modelviewMatrix = new float[16];
            Gl.GetFloat(Gl.GL_MODELVIEW_MATRIX, modelviewMatrix);

            ShaderProgram.SetVariableValue("projectionMatrix", new Matrix(projectionMatrix));
            ShaderProgram.SetVariableValue("modelviewMatrix", new Matrix(modelviewMatrix));
            ShaderProgram.SetVariableValue("tex", m_texture);

            Gl.EnableVertexAttribArray(0);
            Gl.EnableVertexAttribArray(1);

            Gl.Color(Color.Transparent);
            
            Gl.BindVertexArray(m_vao[0]);

            Gl.DrawElements(Gl.GL_TRIANGLES, m_count, Gl.GL_UNSIGNED_INT, IntPtr.Zero);

            m_texture.Deactivate();
            ShaderProgram.Disable();
        }
    }
}
