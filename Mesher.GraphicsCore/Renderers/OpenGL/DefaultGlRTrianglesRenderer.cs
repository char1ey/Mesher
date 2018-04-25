using System;
using Assimp;
using Mesher.GraphicsCore.Buffers;
using Mesher.GraphicsCore.Collections;
using Mesher.GraphicsCore.Primitives;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Renderers.OpenGL
{
    public class DefaultGlRTrianglesRenderer : RTrianglesRenderer
    {
        private ShaderProgram.GlShaderProgram m_shaderProgram;

        internal DefaultGlRTrianglesRenderer()
        {
            m_shaderProgram = new ShaderProgram.GlShaderProgram(null, Properties.Resources.DefaultVertexShaderProgram, Properties.Resources.DefaultFragmentShaderProgram);
        }
        public override void Render(RTriangles rTriangles, IRenderContext renderContext)
        {
            m_shaderProgram.Bind();
            
            m_shaderProgram.SetValue("lightsCount", rTriangles.RScene.Lights.Count);

            for (var i = 0; i < rTriangles.RScene.Lights.Count; i++)
            {
                var light = rTriangles.RScene.Lights[i];

                m_shaderProgram.SetValue($"lights[{i}].lightType", (Int32)light.LightType);
                m_shaderProgram.SetValue($"lights[{i}].ambientColor", light.AmbientColor);
                m_shaderProgram.SetValue($"lights[{i}].diffuseColor", light.DiffuseColor);
                m_shaderProgram.SetValue($"lights[{i}].specularColor", light.SpecularColor);
                m_shaderProgram.SetValue($"lights[{i}].position", light.Position);
                m_shaderProgram.SetValue($"lights[{i}].direction", light.Direction);
                m_shaderProgram.SetValue($"lights[{i}].innerAngle", light.InnerAngle);
                m_shaderProgram.SetValue($"lights[{i}].outerAngle", light.OuterAngle);
                m_shaderProgram.SetValue($"lights[{i}].attenuationConstant", light.AttenuationConstant);
                m_shaderProgram.SetValue($"lights[{i}].attenuationLinear", light.AttenuationLinear);
                m_shaderProgram.SetValue($"lights[{i}].attenuationQuadratic", light.AttenuationQuadratic);
            }

            m_shaderProgram.SetValue("proj", renderContext.Camera.ProjectionMatrix);
            m_shaderProgram.SetValue("modelView", renderContext.Camera.ViewMatrix);

            m_shaderProgram.SetBuffer("position", (GlDataBuffer<Vec3>)rTriangles.Positions, 3);

            m_shaderProgram.SetValue("hasNormal", rTriangles.HasNormals);
            if (rTriangles.HasNormals)
                m_shaderProgram.SetBuffer("normal", (GlDataBuffer<Vec3>)rTriangles.Normals, 3);

            m_shaderProgram.SetValue("hasTexCoord", rTriangles.HasTextureVertexes);
            if (rTriangles.HasTextureVertexes)
                m_shaderProgram.SetBuffer("texCoord", (GlDataBuffer<Vec2>)rTriangles.TexCoords, 2);

            m_shaderProgram.SetValue("hasTangentBasis", rTriangles.HasTangentBasis);
            if (rTriangles.HasTangentBasis)
            {
                m_shaderProgram.SetBuffer("tangent", (GlDataBuffer<Vec3>)rTriangles.Tangents, 3);
                m_shaderProgram.SetBuffer("biTangent", (GlDataBuffer<Vec3>)rTriangles.BiTangents, 3);
            }

            if (rTriangles.HasMaterial)
            {
                m_shaderProgram.SetValue("material.hasColorAmbient", rTriangles.Material.HasColorAmbient);
                if (rTriangles.Material.HasColorAmbient)
                    m_shaderProgram.SetValue("material.colorAmbient", rTriangles.Material.ColorAmbient);

                m_shaderProgram.SetValue("material.hasColorDiffuse", rTriangles.Material.HasColorDiffuse);
                if (rTriangles.Material.HasColorDiffuse)
                    m_shaderProgram.SetValue("material.colorDiffuse", rTriangles.Material.ColorDiffuse);

                m_shaderProgram.SetValue("material.hasColorSpecular", rTriangles.Material.HasColorSpecular);
                if (rTriangles.Material.HasColorSpecular)
                    m_shaderProgram.SetValue("material.colorSpecular", rTriangles.Material.ColorSpecular);

                m_shaderProgram.SetValue("material.hasTextureAmbient", rTriangles.Material.HasTextureAmbient);
                if (rTriangles.Material.HasTextureAmbient)
                    m_shaderProgram.SetValue("material.textureAmbient", rTriangles.Material.TextureAmbient);

                m_shaderProgram.SetValue("material.hasTextureDiffuse", rTriangles.Material.HasTextureDiffuse);
                if (rTriangles.Material.HasTextureDiffuse)
                    m_shaderProgram.SetValue("material.textureDiffuse", rTriangles.Material.TextureDiffuse);

                m_shaderProgram.SetValue("material.hasTextureSpecular", rTriangles.Material.HasTextureSpecular);
                if (rTriangles.Material.HasTextureSpecular)
                    m_shaderProgram.SetValue("material.textureSpecular", rTriangles.Material.TextureSpecular);

                m_shaderProgram.SetValue("material.hasTextureNormal", rTriangles.Material.HasTextureNormal);
                if (rTriangles.Material.HasTextureNormal)
                    m_shaderProgram.SetValue("material.textureNormal", rTriangles.Material.TextureNormal);
            }

            m_shaderProgram.SetBuffer(rTriangles.Indexes);

            m_shaderProgram.RenderTriangles(rTriangles.IndexedRendering);

            m_shaderProgram.Unbind();
        }
    }
}