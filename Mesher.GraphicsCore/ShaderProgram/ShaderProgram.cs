using System;
using System.Collections.Generic;
using System.Linq;
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

        private String m_vertexShaderSource;
        private String m_fragmentShaderSource;

        private List<IBindableItem> m_items;

        private Int32 m_indiciesCount;
        private Int32 m_verticesCount;

        private DataContext m_dataContext;

        internal DataContext DataContext { get { return m_dataContext; } }

        internal UInt32 ShaderProgramId { get { return m_shaderProgramId; } }

        internal ShaderProgram(DataContext dataContext, String vertexShaderSource, String fragmentShaderSource)
        {
            m_dataContext = dataContext;

            m_vertexShaderSource = vertexShaderSource;
            m_fragmentShaderSource = fragmentShaderSource;

            CreateShaderProgram();

            m_items = new List<IBindableItem>();
        }

        internal ShaderProgram(DataContext dataContext, Byte[] vertexShaderSource, Byte[] fragmentShaderSource)
        :this(dataContext, ToString(vertexShaderSource), ToString(fragmentShaderSource)) { }

        private void CreateShaderProgram()
        {
            m_shaderProgramId = Gl.CreateProgram();

            var vertexShaderId = CreateShader(Gl.GL_VERTEX_SHADER, m_vertexShaderSource);
            Gl.AttachShader(m_shaderProgramId, vertexShaderId);
            
            var fragmentShaderId = CreateShader(Gl.GL_FRAGMENT_SHADER, m_fragmentShaderSource);
            Gl.AttachShader(m_shaderProgramId, fragmentShaderId);

            LinkShaderProgram();

            ValidateShaderProgram();

            Gl.DeleteShader(vertexShaderId);
            Gl.DeleteShader(fragmentShaderId);
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

        public void SetBuffer(String name, VertexBufferBase vertexBuffer, Int32 componentsCount)
        {
            var variableLocation = Gl.GetAttribLocation(m_shaderProgramId, name);

            if (variableLocation != -1)
                SetBuffer((UInt32)variableLocation, vertexBuffer, componentsCount);
        }

        public void SetBuffer(UInt32 variableLocation, VertexBufferBase vertexBuffer, Int32 componentsCount)
        {
            vertexBuffer.Bind();

            Gl.EnableVertexAttribArray(variableLocation);

            Gl.VertexAttribPointer(variableLocation, componentsCount, Gl.GL_FLOAT, false, 0, IntPtr.Zero);

            m_verticesCount = vertexBuffer.Count;

            m_items.Add(vertexBuffer);
        }

        public void SetBuffer(String name, Single[] data, Int32 componentsCount)
        {
            if(data.Length % componentsCount != 0)
                throw new ArgumentException();

            unsafe
            {
                fixed (Single* p = data)
                    SetBuffer(name, new IntPtr(p), data.Length / componentsCount, componentsCount);
            }
        }

        public void SetBuffer(String name, IntPtr data, Int32 count, Int32 componentsCount)
        {
            var variableLocation = Gl.GetAttribLocation(m_shaderProgramId, name);

            if (variableLocation != -1)
                SetBuffer((UInt32)variableLocation, data, count, componentsCount);
        }

        public void SetBuffer(UInt32 variableLocation, IntPtr data, Int32 count, Int32 componentsCount)
        {
            Gl.EnableVertexAttribArray(variableLocation);

            Gl.VertexAttribPointer(variableLocation, componentsCount, Gl.GL_FLOAT, false, 0, data);

            m_verticesCount = count;
        }

        public void SetBuffer(IndexBuffer indexBuffer)
        {
            indexBuffer.Bind();

            m_indiciesCount = indexBuffer.Count;

            m_items.Add(indexBuffer);
        }

        public void SetValue(String name, Texture.Texture value)
        {
            value.Bind();
            Gl.Uniform1(Gl.GetUniformLocation(m_shaderProgramId, name), value.Bind());
            m_items.Add(value);
        }

        public void SetValue(String name, Mat4 matrix)
        {
            Gl.UniformMatrix4(Gl.GetUniformLocation(m_shaderProgramId, name), 1, false, matrix.ToArray());
        }

        public void SetValue(String name, Mat3 matrix)
        {
            Gl.UniformMatrix3(Gl.GetUniformLocation(m_shaderProgramId, name), 1, false, matrix.ToArray());
        }

        public void SetValue(String name, Vec3 v)
        {
            Gl.Uniform3(Gl.GetUniformLocation(m_shaderProgramId, name), v.X, v.Y, v.Z);
        }

        public void SetValue(String name, Vec4 v)
        {
            Gl.Uniform4(Gl.GetUniformLocation(m_shaderProgramId, name), v.X, v.Y, v.Z, v.W);
        }

        public void SetValue(String name, Color3 v)
        {
            Gl.Uniform3(Gl.GetUniformLocation(m_shaderProgramId, name), v.R, v.G, v.B);
        }

        public void SetValue(String name, Color4 v)
        {
            Gl.Uniform4(Gl.GetUniformLocation(m_shaderProgramId, name), v.R, v.G, v.B, v.A);
        }

        public void SetValue(String name, Single v)
        {
            Gl.Uniform1(Gl.GetUniformLocation(m_shaderProgramId, name), v);
        }

        public void SetValue(String name, Int32 v)
        {
            Gl.Uniform1(Gl.GetUniformLocation(m_shaderProgramId, name), v);
        }

        public void SetValue(String name, Boolean v)
        {
            Gl.Uniform1(Gl.GetUniformLocation(m_shaderProgramId, name), v ? 1 : 0);
        }

        public void RenderTriangles(Boolean indexed)
        {
            if (indexed)
                Gl.DrawElements(Gl.GL_TRIANGLES, m_indiciesCount, Gl.GL_UNSIGNED_INT, IntPtr.Zero);
            else Gl.DrawArrays(Gl.GL_TRIANGLES, 0, m_verticesCount);

            foreach(var item in m_items)
                item.Unbind();

            m_items.Clear();    
        }

        public void RenderLines(Single lineWidth, Boolean indexed)
        {
            Gl.LineWidth(lineWidth);

            if (indexed)
                Gl.DrawElements(Gl.GL_LINES, m_indiciesCount, Gl.GL_UNSIGNED_INT, IntPtr.Zero);
            else Gl.DrawArrays(Gl.GL_LINES, 0, m_verticesCount);

            foreach (var item in m_items)
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

        private static String ToString(Byte[] bytes)
        {
            if (bytes == null)
                return null;
            return new String(bytes.Select(t => (Char)t).ToArray());
        }
    }
}
