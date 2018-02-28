using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Mesher.GraphicsCore.Buffers;
using Mesher.GraphicsCore.ShaderProgram.Exceptions;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.ShaderProgram
{
    public class ShaderProgram : IDisposable
    {
        private const Int32 LOG_INFO_MAX_SIZE = 1000000;

        private UInt32 m_shaderProgramId;

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

        public void Begin()
        {
            Gl.UseProgram(m_shaderProgramId);
        }

        public void End()
        {
            Gl.UseProgram(0);
        }

        public void SetVertexBuffer<T>(ShaderVariable variableName, VertexBuffer<T> vertexBuffer) where T : struct
        {
            var name = ShaderVariablesNames.GetVariableName(variableName);
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
        }

        public void SetVariableValue(ShaderVariable variableName, Texture.Texture value)
        {
            var name = ShaderVariablesNames.GetVariableName(variableName);
            Gl.Uniform1(Gl.GetUniformLocation(m_shaderProgramId, name), value.Activate());
        }

        public void SetVariableValue(ShaderVariable variableName, Mat4 matrix)
        {
            var name = ShaderVariablesNames.GetVariableName(variableName);
            Gl.UniformMatrix4(Gl.GetUniformLocation(m_shaderProgramId, name), 1, false, matrix.ToArray());
        }

        public void SetVariableValue(ShaderVariable variableName, Mat3 matrix)
        {
            var name = ShaderVariablesNames.GetVariableName(variableName);
            Gl.UniformMatrix3(Gl.GetUniformLocation(m_shaderProgramId, name), 1, false, matrix.ToArray());
        }

        public void SetVariableValue(ShaderVariable variableName, Vec3 v)
        {
            var name = ShaderVariablesNames.GetVariableName(variableName);
            Gl.Uniform3(Gl.GetUniformLocation(m_shaderProgramId, name), (Single)v.X, (Single)v.Y, (Single)v.Z);
        }

        public void SetVariableValue(ShaderVariable variableName, Vec4 v)
        {
            var name = ShaderVariablesNames.GetVariableName(variableName);
            Gl.Uniform4(Gl.GetUniformLocation(m_shaderProgramId, name), (Single)v.X, (Single)v.Y, (Single)v.Z, (Single)v.W);
        }

        public void SetVariableValue(ShaderVariable variableName, Color3 v)
        {
            var name = ShaderVariablesNames.GetVariableName(variableName);
            Gl.Uniform3(Gl.GetUniformLocation(m_shaderProgramId, name), v.R, v.G, v.B);
        }

        public void SetVariableValue(ShaderVariable variableName, Color4 v)
        {
            var name = ShaderVariablesNames.GetVariableName(variableName);
            Gl.Uniform4(Gl.GetUniformLocation(m_shaderProgramId, name), v.R, v.G, v.B, v.A);
        }

        public void SetVariableValue(ShaderVariable variableName, Single v)
        {
            var name = ShaderVariablesNames.GetVariableName(variableName);
            Gl.Uniform1(Gl.GetUniformLocation(m_shaderProgramId, name), v);
        }

        public void SetVariableValue(ShaderVariable variableName, Int32 v)
        {
            var name = ShaderVariablesNames.GetVariableName(variableName);
            Gl.Uniform1(Gl.GetUniformLocation(m_shaderProgramId, name), v);
        }

        public void SetVariableValue(ShaderVariable variableName, Boolean v)
        {
            var name = ShaderVariablesNames.GetVariableName(variableName);
            Gl.Uniform1(Gl.GetUniformLocation(m_shaderProgramId, name), v ? 1 : 0);
        }

        public void SetArrayValue(Int32 index, ShaderVariable variableName, Texture.Texture value)
        {
            var name = String.Format(ShaderVariablesNames.GetVariableName(variableName), index);
            Gl.Uniform1(Gl.GetUniformLocation(m_shaderProgramId, name), value.Activate());
        }

        public void SetArrayValue(Int32 index, ShaderVariable variableName, Mat4 matrix)
        {
            var name = String.Format(ShaderVariablesNames.GetVariableName(variableName), index);
            Gl.UniformMatrix4(Gl.GetUniformLocation(m_shaderProgramId, name), 1, false, matrix.ToArray());
        }

        public void SetArrayValue(Int32 index, ShaderVariable variableName, Mat3 matrix)
        {
            var name = String.Format(ShaderVariablesNames.GetVariableName(variableName), index);
            Gl.UniformMatrix3(Gl.GetUniformLocation(m_shaderProgramId, name), 1, false, matrix.ToArray());
        }

        public void SetArrayValue(Int32 index, ShaderVariable variableName, Vec3 v)
        {
            var name = String.Format(ShaderVariablesNames.GetVariableName(variableName), index);
            Gl.Uniform3(Gl.GetUniformLocation(m_shaderProgramId, name), (Single)v.X, (Single)v.Y, (Single)v.Z);
        }

        public void SetArrayValue(Int32 index, ShaderVariable variableName, Vec4 v)
        {
            var name = String.Format(ShaderVariablesNames.GetVariableName(variableName), index);
            Gl.Uniform4(Gl.GetUniformLocation(m_shaderProgramId, name), (Single)v.X, (Single)v.Y, (Single)v.Z, (Single)v.W);
        }

        public void SetArrayValue(Int32 index, ShaderVariable variableName, Color3 v)
        {
            var name = String.Format(ShaderVariablesNames.GetVariableName(variableName), index);
            Gl.Uniform3(Gl.GetUniformLocation(m_shaderProgramId, name), v.R, v.G, v.B);
        }

        public void SetArrayValue(Int32 index, ShaderVariable variableName, Color4 v)
        {
            var name = String.Format(ShaderVariablesNames.GetVariableName(variableName), index);
            Gl.Uniform4(Gl.GetUniformLocation(m_shaderProgramId, name), v.R, v.G, v.B, v.A);
        }

        public void SetArrayValue(Int32 index, ShaderVariable variableName, Single v)
        {
            var name = String.Format(ShaderVariablesNames.GetVariableName(variableName), index);
            Gl.Uniform1(Gl.GetUniformLocation(m_shaderProgramId, name), v);
        }

        public void Dispose()
        {
            Gl.DeleteProgram(ShaderProgramId);
        }
    }
}
