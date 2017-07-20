using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Shaders
{
    public class ShaderProgram: IDisposable
    {
        private const int LogInfoMaxSize = 1000;

        private readonly uint m_shaderProgramId;

        internal uint ShaderProgramId { get { return m_shaderProgramId; } }

        public ShaderProgram(string vertexShaderSource, string fragmentShaderSource)
        {
            m_shaderProgramId = Gl.CreateProgram();

            var vertexShader = CreateShader(Gl.GL_VERTEX_SHADER, vertexShaderSource);
            Gl.AttachShader(m_shaderProgramId, vertexShader);

            var fragmentShader = CreateShader(Gl.GL_FRAGMENT_SHADER, fragmentShaderSource);
            Gl.AttachShader(m_shaderProgramId, fragmentShader);

            LinkShaderProgram();

            ValidateShaderProgram();

            Gl.DeleteShader(vertexShader);
            Gl.DeleteShader(fragmentShader);
        }

        private void ValidateShaderProgram()
        {
            Gl.ValidateProgram(m_shaderProgramId);

            var success = new int[1];

            Gl.GetProgram(m_shaderProgramId, Gl.GL_VALIDATE_STATUS, success);

            if (success[0] == 0)
            {
                var infoLog = new StringBuilder(LogInfoMaxSize);
                Gl.GetProgramInfoLog(m_shaderProgramId, LogInfoMaxSize, IntPtr.Zero, infoLog);

                throw new ValidateShaderProgramException(infoLog.ToString());
            }
        }

        private void LinkShaderProgram()
        {
            Gl.LinkProgram(m_shaderProgramId);

            var success = new int[1];

            Gl.GetProgram(m_shaderProgramId, Gl.GL_LINK_STATUS, success);

            if (success[0] == 0)
            {
                var infoLog = new StringBuilder(LogInfoMaxSize);
                Gl.GetProgramInfoLog(m_shaderProgramId, LogInfoMaxSize, IntPtr.Zero, infoLog);

                throw new LinkShaderProgramException(infoLog.ToString());
            }
        }

        private static uint CreateShader(uint type, string source)
        {
            var ret = Gl.CreateShader(type);

            Gl.ShaderSource(ret, source);
            Gl.CompileShader(ret);
            var success = new int[1];
            Gl.GetShader(ret, Gl.GL_COMPILE_STATUS, success);

            if (success[0] == 0)
            {
                var infoLog = new StringBuilder(LogInfoMaxSize);
                Gl.GetShaderInfoLog(ret, LogInfoMaxSize, IntPtr.Zero, infoLog);

                throw new CompileShaderException(infoLog.ToString());
            }

            return ret;
        }

        public void Enable()
        {
            Gl.UseProgram(m_shaderProgramId);
        }

        public void Disable()
        {
            Gl.UseProgram(0);
        }

        public void SetVariableValue(string variableName, Texture.Texture value)
        {
            Gl.Uniform1(Gl.GetUniformLocation(m_shaderProgramId, variableName), value.Activate());
        }

        public void SetVariableValue(string variableName, Matrix matrix)
        {
            Gl.UniformMatrix4(Gl.GetUniformLocation(m_shaderProgramId, variableName), 1, false, matrix.ToArrayFloat());
        }

        public void Dispose()
        {
            Gl.DeleteProgram(m_shaderProgramId);
        }
    }
}
