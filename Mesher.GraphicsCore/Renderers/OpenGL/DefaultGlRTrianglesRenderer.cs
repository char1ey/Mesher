using System;
using Assimp;
using Mesher.GraphicsCore.Buffers;
using Mesher.GraphicsCore.Collections;
using Mesher.GraphicsCore.Primitives;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.GraphicsCore.Texture;
using Mesher.GraphicsCore.Texture.OpenGL;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Renderers.OpenGL
{
    public class DefaultGlRTrianglesRenderer : RTrianglesRenderer
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

        private Int32 m_hasNormalId;
        private Int32 m_normalsId;

        private Int32 m_hasTexCoordId;
        private Int32 m_texCoordId;

        private Int32 m_hasTangentBasisId;
        private Int32 m_tangentsId;
        private Int32 m_biTangentsId;

        private Int32 m_materialHasColorAmbientId;
        private Int32 m_materialColorAmbientId;
        private Int32 m_materialHasColorDiffuseId;
        private Int32 m_materialColorDiffuseId;
        private Int32 m_materialHasColorSpecularId;
        private Int32 m_materialColorSpecularId;
        private Int32 m_materialHasTextureAmbientId;
        private Int32 m_materialTextureAmbientId;
        private Int32 m_materialHasTextureDiffuseId;
        private Int32 m_materialTextureDiffuseId;
        private Int32 m_materialHasTextureSpecularId;
        private Int32 m_materialTextureSpecularId;
        private Int32 m_materialHasTextureNormalId;
        private Int32 m_materialTextureNormalId;

        internal DefaultGlRTrianglesRenderer()
        {
            m_shaderProgram = new ShaderProgram.GlShaderProgram(null, Properties.Resources.DefaultTrianglesVertexShader, Properties.Resources.DefaultTrianglesFragmentShader);

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
            m_hasNormalId = m_shaderProgram.GetUniformLocation("hasNormal");
            m_normalsId = m_shaderProgram.GetAttributeLocation("normal");
            m_hasTexCoordId = m_shaderProgram.GetUniformLocation("hasTexCoord");
            m_texCoordId = m_shaderProgram.GetAttributeLocation("texCoord");
            m_hasTangentBasisId = m_shaderProgram.GetUniformLocation("hasTangentBasis");
            m_tangentsId = m_shaderProgram.GetAttributeLocation("tangent");
            m_biTangentsId = m_shaderProgram.GetAttributeLocation("biTangent");
            m_materialHasColorAmbientId = m_shaderProgram.GetUniformLocation("material.hasColorAmbient");
            m_materialColorAmbientId = m_shaderProgram.GetUniformLocation("material.colorAmbient");
            m_materialHasColorDiffuseId = m_shaderProgram.GetUniformLocation("material.hasColorDiffuse");
            m_materialColorDiffuseId = m_shaderProgram.GetUniformLocation("material.colorDiffuse");
            m_materialHasColorSpecularId = m_shaderProgram.GetUniformLocation("material.hasColorSpecular");
            m_materialColorSpecularId = m_shaderProgram.GetUniformLocation("material.colorSpecular");
            m_materialHasTextureAmbientId = m_shaderProgram.GetUniformLocation("material.hasTextureAmbient");
            m_materialTextureAmbientId = m_shaderProgram.GetUniformLocation("material.textureAmbient");
            m_materialHasTextureDiffuseId = m_shaderProgram.GetUniformLocation("material.hasTextureDiffuse");
            m_materialTextureDiffuseId = m_shaderProgram.GetUniformLocation("material.textureDiffuse");
            m_materialHasTextureSpecularId = m_shaderProgram.GetUniformLocation("material.hasTextureSpecular");
            m_materialTextureSpecularId = m_shaderProgram.GetUniformLocation("material.textureSpecular");
            m_materialHasTextureNormalId = m_shaderProgram.GetUniformLocation("material.hasTextureNormal");
            m_materialTextureNormalId = m_shaderProgram.GetUniformLocation("material.textureNormal");
        }

        public override void Render(RTriangles rTriangles, IRenderContext renderContext)
        {
            m_shaderProgram.Bind();

            m_shaderProgram.SetValue(m_lightsCountId, rTriangles.RScene.Lights.Count);

            for (var i = 0; i < rTriangles.RScene.Lights.Count; i++)
            {
                var light = rTriangles.RScene.Lights[i];

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

            m_shaderProgram.SetValue(m_cameraProjectionMatrixId, renderContext.Camera.ProjectionMatrix);
            m_shaderProgram.SetValue(m_cameraViewMatrixId, renderContext.Camera.ViewMatrix);

            m_shaderProgram.SetBuffer(m_positionsId, (GlDataBuffer<Vec3>)rTriangles.Positions);

            m_shaderProgram.SetValue(m_hasNormalId, rTriangles.HasNormals);
            if (rTriangles.HasNormals)
                m_shaderProgram.SetBuffer(m_normalsId, (GlDataBuffer<Vec3>)rTriangles.Normals);

            m_shaderProgram.SetValue(m_hasTexCoordId, rTriangles.HasTextureVertexes);
            if (rTriangles.HasTextureVertexes)
                m_shaderProgram.SetBuffer(m_texCoordId, (GlDataBuffer<Vec2>)rTriangles.TexCoords);

            m_shaderProgram.SetValue(m_hasTangentBasisId, rTriangles.HasTangentBasis);
            if (rTriangles.HasTangentBasis)
            {
                m_shaderProgram.SetBuffer(m_tangentsId, (GlDataBuffer<Vec3>)rTriangles.Tangents);
                m_shaderProgram.SetBuffer(m_biTangentsId, (GlDataBuffer<Vec3>)rTriangles.BiTangents);
            }

            if (rTriangles.HasMaterial)
            {
                m_shaderProgram.SetValue(m_materialHasColorAmbientId, rTriangles.Material.HasColorAmbient);
                if (rTriangles.Material.HasColorAmbient)
                    m_shaderProgram.SetValue(m_materialColorAmbientId, rTriangles.Material.ColorAmbient);

                m_shaderProgram.SetValue(m_materialHasColorDiffuseId, rTriangles.Material.HasColorDiffuse);
                if (rTriangles.Material.HasColorDiffuse)
                    m_shaderProgram.SetValue(m_materialColorDiffuseId, rTriangles.Material.ColorDiffuse);

                m_shaderProgram.SetValue(m_materialHasColorSpecularId, rTriangles.Material.HasColorSpecular);
                if (rTriangles.Material.HasColorSpecular)
                    m_shaderProgram.SetValue(m_materialColorSpecularId, rTriangles.Material.ColorSpecular);

                m_shaderProgram.SetValue(m_materialHasTextureAmbientId, rTriangles.Material.HasTextureAmbient);
                if (rTriangles.Material.HasTextureAmbient)
                    m_shaderProgram.SetValue(m_materialTextureAmbientId, (GlTexture)rTriangles.Material.TextureAmbient);

                m_shaderProgram.SetValue(m_materialHasTextureDiffuseId, rTriangles.Material.HasTextureDiffuse);
                if (rTriangles.Material.HasTextureDiffuse)
                    m_shaderProgram.SetValue(m_materialTextureDiffuseId, (GlTexture)rTriangles.Material.TextureDiffuse);

                m_shaderProgram.SetValue(m_materialHasTextureSpecularId, rTriangles.Material.HasTextureSpecular);
                if (rTriangles.Material.HasTextureSpecular)
                    m_shaderProgram.SetValue(m_materialTextureSpecularId, (GlTexture)rTriangles.Material.TextureSpecular);

                m_shaderProgram.SetValue(m_materialHasTextureNormalId, rTriangles.Material.HasTextureNormal);
                if (rTriangles.Material.HasTextureNormal)
                    m_shaderProgram.SetValue(m_materialTextureNormalId, (GlTexture)rTriangles.Material.TextureNormal);
            }

            m_shaderProgram.SetBuffer(rTriangles.Indexes);

            m_shaderProgram.RenderTriangles(rTriangles.IndexedRendering);

            m_shaderProgram.Unbind();
        }
    }
}