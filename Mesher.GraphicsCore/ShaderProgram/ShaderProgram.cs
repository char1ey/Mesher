using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Mesher.GraphicsCore.Buffers;
using Mesher.GraphicsCore.ShaderProgram.Exceptions;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.ShaderProgram
{
    public class ShaderProgram : IDisposable, IBindableItem
    {
        private const Int32 LOG_INFO_MAX_SIZE = 1000000;

        private UInt32 m_shaderProgramId;

        private List<IBindableItem> m_items;

        private Int32 m_IndiciesCount;
        private Int32 m_VerticesCount;

        public UInt32 ShaderProgramId { get { return m_shaderProgramId; } }

        public ShaderProgram()
        {
            var vertexShaderSource = GetShaderSource(ShaderProgramType.Vertex);
            var fragmentShaderSource = GetShaderSource(ShaderProgramType.Fragment);

            m_shaderProgramId = Gl.CreateProgram();

            var vertexShaderId = CreateShader(Gl.GL_VERTEX_SHADER, vertexShaderSource);
            Gl.AttachShader(m_shaderProgramId, vertexShaderId);

            var fragmentShaderId = CreateShader(Gl.GL_FRAGMENT_SHADER, fragmentShaderSource);
            Gl.AttachShader(m_shaderProgramId, fragmentShaderId);

            LinkShaderProgram();

            ValidateShaderProgram();

            Gl.DeleteShader(vertexShaderId);
            Gl.DeleteShader(fragmentShaderId);

            m_items = new List<IBindableItem>();
        }

        private String GetShaderSource(ShaderProgramType shaderProgramType)
        {
            Byte[] bytes = null;

            switch (shaderProgramType)
            {
                case ShaderProgramType.Vertex:
                    bytes = Properties.Resources.VertexShaderProgramSource;
                    break;
                case ShaderProgramType.Fragment:
                    bytes = Properties.Resources.FragmentShaderProgramSource;
                    break;
            }

            return new String(bytes.Select(t => (Char)t).ToArray());
        }

        private void ValidateShaderProgram()
        {
            Gl.ValidateProgram(m_shaderProgramId);

            var success = new Int32[1];

            Gl.GetProgram(m_shaderProgramId, Gl.GL_VALIDATE_STATUS, success);

            if (success[0] == 0)
            {
                var infoLog = new StringBuilder(LOG_INFO_MAX_SIZE);
                Gl.GetProgramInfoLog(m_shaderProgramId, LOG_INFO_MAX_SIZE, IntPtr.Zero, infoLog);

                throw new ValidateShaderProgramException(infoLog.ToString());
            }
        }

        private void LinkShaderProgram()
        {
            Gl.LinkProgram(m_shaderProgramId);

            var success = new Int32[1];

            Gl.GetProgram(m_shaderProgramId, Gl.GL_LINK_STATUS, success);

            if (success[0] == 0)
            {
                var infoLog = new StringBuilder(LOG_INFO_MAX_SIZE);
                Gl.GetProgramInfoLog(m_shaderProgramId, LOG_INFO_MAX_SIZE, IntPtr.Zero, infoLog);

                throw new LinkShaderProgramException(infoLog.ToString());
            }
        }

        private static UInt32 CreateShader(UInt32 type, String source)
        {
            var ret = Gl.CreateShader(type);

            Gl.ShaderSource(ret, source);
            Gl.CompileShader(ret);
            var success = new Int32[1];
            Gl.GetShader(ret, Gl.GL_COMPILE_STATUS, success);

            if (success[0] == 0)
            {
                var infoLog = new StringBuilder(LOG_INFO_MAX_SIZE);
                Gl.GetShaderInfoLog(ret, LOG_INFO_MAX_SIZE, IntPtr.Zero, infoLog);

                throw new CompileShaderException(infoLog.ToString());
            }

            return ret;
        }

        public void Bind()
        {
            Gl.UseProgram(m_shaderProgramId);
        }

        public void Unbind()
        {
            Gl.UseProgram(0);
        }

        public void SetVertexBuffer<T>(String name, VertexBuffer<T> vertexBuffer) where T : struct
        {
            var variableLocation = Gl.GetAttribLocation(m_shaderProgramId, name);

            if (variableLocation != -1)
                SetVertexBuffer((UInt32)variableLocation, vertexBuffer);
        }

        public void SetVertexBuffer<T>(UInt32 variableLocation, VertexBuffer<T> vertexBuffer) where T : struct
        {
            vertexBuffer.Bind();

            Gl.EnableVertexAttribArray(variableLocation);

            var size = Marshal.SizeOf(typeof(T));

            Gl.VertexAttribPointer(variableLocation, size / 4, Gl.GL_FLOAT, false, 0, IntPtr.Zero);

            m_items.Add(vertexBuffer);
        }

        public void SetIndexBuffer(IndexBuffer indexBuffer)
        {
            indexBuffer.Bind();

            m_IndiciesCount = indexBuffer.Count;

            m_items.Add(indexBuffer);
        }

        public void SetVariableValue(String name, Texture.Texture value)
        {
            value.Bind();
            Gl.Uniform1(Gl.GetUniformLocation(m_shaderProgramId, name), value.Bind());
            m_items.Add(value);
        }

        public void SetVariableValue(String name, Mat4 matrix)
        {
            Gl.UniformMatrix4(Gl.GetUniformLocation(m_shaderProgramId, name), 1, false, matrix.ToArray());
        }

        public void SetVariableValue(String name, Mat3 matrix)
        {
            Gl.UniformMatrix3(Gl.GetUniformLocation(m_shaderProgramId, name), 1, false, matrix.ToArray());
        }

        public void SetVariableValue(String name, Vec3 v)
        {
            Gl.Uniform3(Gl.GetUniformLocation(m_shaderProgramId, name), (Single)v.X, (Single)v.Y, (Single)v.Z);
        }

        public void SetVariableValue(String name, Vec4 v)
        {
            Gl.Uniform4(Gl.GetUniformLocation(m_shaderProgramId, name), (Single)v.X, (Single)v.Y, (Single)v.Z, (Single)v.W);
        }

        public void SetVariableValue(String name, Color3 v)
        {
            Gl.Uniform3(Gl.GetUniformLocation(m_shaderProgramId, name), v.R, v.G, v.B);
        }

        public void SetVariableValue(String name, Color4 v)
        {
            Gl.Uniform4(Gl.GetUniformLocation(m_shaderProgramId, name), v.R, v.G, v.B, v.A);
        }

        public void SetVariableValue(String name, Single v)
        {
            Gl.Uniform1(Gl.GetUniformLocation(m_shaderProgramId, name), v);
        }

        public void SetVariableValue(String name, Int32 v)
        {
            Gl.Uniform1(Gl.GetUniformLocation(m_shaderProgramId, name), v);
        }

        public void SetVariableValue(String name, Boolean v)
        {
            Gl.Uniform1(Gl.GetUniformLocation(m_shaderProgramId, name), v ? 1 : 0);
        }

        public void Render(Boolean indexed)
        {
            if (indexed)
                Gl.DrawElements(Gl.GL_TRIANGLES, m_IndiciesCount, Gl.GL_UNSIGNED_INT, IntPtr.Zero);
            else Gl.DrawArrays(Gl.GL_TRIANGLES, 0, m_VerticesCount);

            foreach(var item in m_items)
                item.Unbind();

            m_items.Clear();
        }

        public void Dispose()
        {
            Gl.DeleteProgram(ShaderProgramId);
        }

        void IBindableItem.Bind()
        {
            Bind();
        }

        void IBindableItem.Unbind()
        {
            Unbind();  
        }
    }
}
