using System;
using Mesher.GraphicsCore.Data.OpenGL;
using Mesher.GraphicsCore.Texture;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Pipeline.OpenGL
{
    public class GlDefaultPipelineVariable : IPipelineVariable
    {
        private GlDefaultPipeline m_pipeline;

        private Int32 m_variableId;

        public GlDefaultPipelineVariable(GlDefaultPipeline pipeline, Int32 variableId)
        {
            m_pipeline = pipeline;
            m_variableId = variableId;
        }

        public void SetBuffer<T>(GlDataBuffer<T> dataBuffer) where T : struct
        {
            m_pipeline.ShaderProgram.SetBuffer(m_variableId, dataBuffer);
        }

        public void SetTexture(GlTexture texture)
        {
            m_pipeline.ShaderProgram.SetValue(m_variableId, texture);
        }

        public void SetIndexBuffer(GlIndexBuffer indexBuffer)
        {
            m_pipeline.ShaderProgram.SetBuffer(indexBuffer);
        }

        public void SetValue(Mat4 matrix)
        {
            m_pipeline.ShaderProgram.SetValue(m_variableId, matrix);
        }

        public void SetValue(Mat3 matrix)
        {
            throw new NotImplementedException();
        }

        public void SetValue(Int32 value)
        {
            throw new NotImplementedException();
        }
        public void SetValue(Boolean value)
        {
            throw new NotImplementedException();
        }
    }
}
