using System;
using Assimp;
using Mesher.GraphicsCore.Collections;
using Mesher.GraphicsCore.Primitives;
using Mesher.GraphicsCore.RenderContexts;

namespace Mesher.GraphicsCore.Renderers.OpenGL
{
    public class DefaultGlRTrianglesRenderer : RTrianglesRenderer
    {
        private ShaderProgram.ShaderProgram m_shaderProgram;

        internal DefaultGlRTrianglesRenderer()
        {
            m_shaderProgram = new ShaderProgram.ShaderProgram(null, Properties.Resources.DefaultVertexShaderProgram, Properties.Resources.DefaultFragmentShaderProgram);
        }
        public override void Render(RTriangles rItem, IRenderContext renderContext)
        {
            m_shaderProgram.Bind();


            InitLights(rItem.RScene.Lights);

            InitCamera(renderContext.Camera);

            RenderMesh(rItem);

            m_shaderProgram.Unbind();
        }

        private void InitCamera(Camera.Camera camera)
        {
            m_shaderProgram.SetValue("proj", camera.ProjectionMatrix);
            m_shaderProgram.SetValue("modelView", camera.ViewMatrix);
        }

        private void RenderMesh(RTriangles mesh)
        {
            m_shaderProgram.SetBuffer("position", mesh.Positions, 3);

            m_shaderProgram.SetValue("hasNormal", mesh.HasNormals);
            if (mesh.HasNormals)
                m_shaderProgram.SetBuffer("normal", mesh.Normals, 3);

            m_shaderProgram.SetValue("hasTexCoord", mesh.HasTextureVertexes);
            if (mesh.HasTextureVertexes)
                m_shaderProgram.SetBuffer("texCoord", mesh.TexCoords, 2);

            m_shaderProgram.SetValue("hasTangentBasis", mesh.HasTangentBasis);
            if (mesh.HasTangentBasis)
            {
                m_shaderProgram.SetBuffer("tangent", mesh.Tangents, 3);
                m_shaderProgram.SetBuffer("biTangent", mesh.BiTangents, 3);
            }

            if (mesh.HasMaterial)
                InitMaterial(mesh.Material);

            m_shaderProgram.SetBuffer(mesh.Indexes);

            m_shaderProgram.RenderTriangles(mesh.IndexedRendering);
        }

        private void InitMaterial(Material.Material material)
        {
            m_shaderProgram.SetValue("material.hasColorAmbient", material.HasColorAmbient);
            if (material.HasColorAmbient)
                m_shaderProgram.SetValue("material.colorAmbient", material.ColorAmbient);

            m_shaderProgram.SetValue("material.hasColorDiffuse", material.HasColorDiffuse);
            if (material.HasColorDiffuse)
                m_shaderProgram.SetValue("material.colorDiffuse", material.ColorDiffuse);

            m_shaderProgram.SetValue("material.hasColorSpecular", material.HasColorSpecular);
            if (material.HasColorSpecular)
                m_shaderProgram.SetValue("material.colorSpecular", material.ColorSpecular);

            m_shaderProgram.SetValue("material.hasTextureAmbient", material.HasTextureAmbient);
            if (material.HasTextureAmbient)
                m_shaderProgram.SetValue("material.textureAmbient", material.TextureAmbient);

            m_shaderProgram.SetValue("material.hasTextureDiffuse", material.HasTextureDiffuse);
            if (material.HasTextureDiffuse)
                m_shaderProgram.SetValue("material.textureDiffuse", material.TextureDiffuse);

            m_shaderProgram.SetValue("material.hasTextureSpecular", material.HasTextureSpecular);
            if (material.HasTextureSpecular)
                m_shaderProgram.SetValue("material.textureSpecular", material.TextureSpecular);

            m_shaderProgram.SetValue("material.hasTextureNormal", material.HasTextureNormal);
            if (material.HasTextureNormal)
                m_shaderProgram.SetValue("material.textureNormal", material.TextureNormal);
        }

        private void InitLights(Lights lights)
        {
            m_shaderProgram.SetValue("lightsCount", lights.Count);

            for (var i = 0; i < lights.Count; i++)
            {
                var light = lights[i];

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
        }
    }
}