using System;
using Mesher.GraphicsCore.Buffers;
using Mesher.GraphicsCore.Collections;
using Mesher.GraphicsCore.Primitives;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.GraphicsCore.Texture;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Renderers.OpenGL
{
    public class DefaultGlREdgesRenderer : REdgesRenderer
    {
		private const Int32 MAX_LIGHTS_COUNT = 32;
		private ShaderProgram.GlShaderProgram m_shaderProgram;

		private Int32 m_lightsCountId;

		private Int32[] m_lightTypeIds;
		private Int32[] m_lightAmbientColorIds;
		private Int32[] m_lightDiffuseColorIds;
		private Int32[] m_lightSpecularColorIds;
		private Int32[] m_lightPositionIds;
		private Int32[] m_lightDirectionIds;
		private Int32[] m_lightInnerAngleIds;
		private Int32[] m_lightOuterAngleIds;
		private Int32[] m_lightAttenuationConstantIds;
		private Int32[] m_lightAttenuationLinearIds;
		private Int32[] m_lightAttenuationQuadraticIds;

		private Int32 m_cameraProjectionMatrixId;
		private Int32 m_cameraViewMatrixId;

		private Int32 m_positionsId;

		internal DefaultGlREdgesRenderer()
		{
			m_shaderProgram = new ShaderProgram.GlShaderProgram(null, Properties.Resources.DefaultEdgesVertexShader, Properties.Resources.DefaultEdgesFragmentShader);

			InitVariablesLocations();
		}

		private void InitVariablesLocations()
		{
			m_lightsCountId = m_shaderProgram.GetUniformLocation("lightsCount");
			m_lightTypeIds = new Int32[MAX_LIGHTS_COUNT];
			m_lightAmbientColorIds = new Int32[MAX_LIGHTS_COUNT];
			m_lightDiffuseColorIds = new Int32[MAX_LIGHTS_COUNT];
			m_lightSpecularColorIds = new Int32[MAX_LIGHTS_COUNT];
			m_lightPositionIds = new Int32[MAX_LIGHTS_COUNT];
			m_lightDirectionIds = new Int32[MAX_LIGHTS_COUNT];
			m_lightInnerAngleIds = new Int32[MAX_LIGHTS_COUNT];
			m_lightOuterAngleIds = new Int32[MAX_LIGHTS_COUNT];
			m_lightAttenuationConstantIds = new Int32[MAX_LIGHTS_COUNT];
			m_lightAttenuationLinearIds = new Int32[MAX_LIGHTS_COUNT];
			m_lightAttenuationQuadraticIds = new Int32[MAX_LIGHTS_COUNT];

			for (var i = 0; i < MAX_LIGHTS_COUNT; i++)
			{
				m_lightTypeIds[i] = m_shaderProgram.GetUniformLocation($"lights[{i}].lightType");
				m_lightAmbientColorIds[i] = m_shaderProgram.GetUniformLocation($"lights[{i}].ambientColor");
				m_lightDiffuseColorIds[i] = m_shaderProgram.GetUniformLocation($"lights[{i}].diffuseColor");
				m_lightSpecularColorIds[i] = m_shaderProgram.GetUniformLocation($"lights[{i}].specularColor");
				m_lightPositionIds[i] = m_shaderProgram.GetUniformLocation($"lights[{i}].position");
				m_lightDirectionIds[i] = m_shaderProgram.GetUniformLocation($"lights[{i}].direction");
				m_lightInnerAngleIds[i] = m_shaderProgram.GetUniformLocation($"lights[{i}].innerAngle");
				m_lightOuterAngleIds[i] = m_shaderProgram.GetUniformLocation($"lights[{i}].outerAngle");
				m_lightAttenuationConstantIds[i] = m_shaderProgram.GetUniformLocation($"lights[{i}].attenuationConstant");
				m_lightAttenuationLinearIds[i] = m_shaderProgram.GetUniformLocation($"lights[{i}].attenuationLinear");
				m_lightAttenuationQuadraticIds[i] = m_shaderProgram.GetUniformLocation($"lights[{i}].attenuationQuadratic");
			}

			m_cameraProjectionMatrixId = m_shaderProgram.GetUniformLocation("proj");
			m_cameraViewMatrixId = m_shaderProgram.GetUniformLocation("modelView");
			m_positionsId = m_shaderProgram.GetAttributeLocation("position");
		}

		public override void Render(REdges rEdges, Lights lights, IRenderContext renderContext)
        {
			m_shaderProgram.Bind();

			m_shaderProgram.SetValue(m_lightsCountId, lights.Count);

			for (var i = 0; i < lights.Count; i++)
			{
				var light = lights[i];

				m_shaderProgram.SetValue(m_lightTypeIds[i], (Int32)light.LightType);
				m_shaderProgram.SetValue(m_lightAmbientColorIds[i], light.AmbientColor);
				m_shaderProgram.SetValue(m_lightDiffuseColorIds[i], light.DiffuseColor);
				m_shaderProgram.SetValue(m_lightSpecularColorIds[i], light.SpecularColor);
				m_shaderProgram.SetValue(m_lightPositionIds[i], light.Position);
				m_shaderProgram.SetValue(m_lightDirectionIds[i], light.Direction);
				m_shaderProgram.SetValue(m_lightInnerAngleIds[i], light.InnerAngle);
				m_shaderProgram.SetValue(m_lightOuterAngleIds[i], light.OuterAngle);
				m_shaderProgram.SetValue(m_lightAttenuationConstantIds[i], light.AttenuationConstant);
				m_shaderProgram.SetValue(m_lightAttenuationLinearIds[i], light.AttenuationLinear);
				m_shaderProgram.SetValue(m_lightAttenuationQuadraticIds[i], light.AttenuationQuadratic);
			}

			m_shaderProgram.SetValue(m_cameraProjectionMatrixId, renderContext.RCamera.ProjectionMatrix);
			m_shaderProgram.SetValue(m_cameraViewMatrixId, renderContext.RCamera.ViewMatrix);

			m_shaderProgram.SetBuffer(m_positionsId, (GlDataBuffer<Vec3>)rEdges.Positions);
			
			m_shaderProgram.SetBuffer(rEdges.Indexes);

			m_shaderProgram.RenderLines(rEdges.Width, rEdges.IndexedRendering);

			m_shaderProgram.Unbind();
		}
    }
}