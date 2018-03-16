using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using Mesher.Core.Collections;
using Mesher.Core.Components;
using Mesher.GraphicsCore;
using Camera = Mesher.Core.Objects.Camera.Camera;
using Light = Mesher.Core.Objects.Light.Light;
using Material = Mesher.Core.Objects.Material.Material;
using Mesh = Mesher.Core.Objects.Mesh.Mesh;
using Scene = Mesher.Core.Objects.Scene.Scene;

namespace Mesher.Core.Renderers
{
    public class RendererDefault : RendererBase
    {
        public RendererDefault(RenderContext renderContext, String vertexShaderSource, String fragmentShaderSource) 
            : base(renderContext, vertexShaderSource, fragmentShaderSource)
        {
        }

        public override void Render(Scene scene, SceneContext sceneContext)
        {
            ShaderProgram.Bind();

            foreach (var mesh in scene.Meshes)
            {
                InitLights(scene.Lights);

                InitCamera(sceneContext.Camera);

                RenderMesh(mesh);
            }

            ShaderProgram.Unbind();
        }

        private void InitCamera(Camera camera)
        {
            ShaderProgram.SetValue("proj", camera.ProjectionMatrix);
            ShaderProgram.SetValue("modelView", camera.ViewMatrix);
        }

        private void RenderMesh(Mesh mesh)
        {
            ShaderProgram.SetValue("hasPosition", mesh.HasVertexes);
            if (mesh.HasVertexes)
                ShaderProgram.SetBuffer("position", mesh.Vertexes, 3);

            ShaderProgram.SetValue("hasNormal", mesh.HasNormals);
            if (mesh.HasNormals)
                ShaderProgram.SetBuffer("normal", mesh.Normals, 3);

            ShaderProgram.SetValue("hasTexCoord", mesh.HasTextureVertexes);
            if (mesh.HasTextureVertexes)
                ShaderProgram.SetBuffer("texCoord", mesh.TextureVertexes, 2);

            ShaderProgram.SetValue("hasTangentBasis", mesh.HasTangentBasis);
            if (mesh.HasTangentBasis)
            {
                ShaderProgram.SetBuffer("tangent", mesh.Tangents, 3);
                ShaderProgram.SetBuffer("biTangent", mesh.BiTangents, 3);
            }

            if (mesh.HasMaterial)
                InitMaterial(mesh.Material);

            ShaderProgram.SetBuffer(mesh.Indicies);

            ShaderProgram.Render(mesh.IndexedRendering);
        }

        private void InitMaterial(Material material)
        {
            ShaderProgram.SetValue("material.hasColorAmbient", material.HasColorAmbient);
            if (material.HasColorAmbient)
                ShaderProgram.SetValue("material.colorAmbient", material.ColorAmbient);

            ShaderProgram.SetValue("material.hasColorDiffuse", material.HasColorDiffuse);
            if (material.HasColorDiffuse)
                ShaderProgram.SetValue("material.colorDiffuse", material.ColorDiffuse);

            ShaderProgram.SetValue("material.hasColorSpecular", material.HasColorSpecular);
            if (material.HasColorSpecular)
                ShaderProgram.SetValue("material.colorSpecular", material.ColorSpecular);

            ShaderProgram.SetValue("material.hasTextureAmbient", material.HasTextureAmbient);
            if (material.HasTextureAmbient)
                ShaderProgram.SetValue("material.textureAmbient", material.TextureAmbient);

            ShaderProgram.SetValue("material.hasTextureDiffuse", material.HasTextureDiffuse);
            if (material.HasTextureDiffuse)
                ShaderProgram.SetValue("material.textureDiffuse", material.TextureDiffuse);

            ShaderProgram.SetValue("material.hasTextureSpecular", material.HasTextureSpecular);
            if (material.HasTextureSpecular)
                ShaderProgram.SetValue("material.textureSpecular", material.TextureSpecular);

            ShaderProgram.SetValue("material.hasTextureNormal", material.HasTextureNormal);
            if (material.HasTextureNormal)
                ShaderProgram.SetValue("material.textureNormal", material.TextureNormal);
        }

        private void InitLights(Lights lights)
        {
            ShaderProgram.SetValue("lightsCount", lights.Count);

            for (var i = 0; i < lights.Count; i++)
            {
                var light = lights[i];

                ShaderProgram.SetValue($"lights[{i}].lightType", (Int32)light.LightType);
                ShaderProgram.SetValue($"lights[{i}].ambientColor", light.AmbientColor);
                ShaderProgram.SetValue($"lights[{i}].diffuseColor", light.DiffuseColor);
                ShaderProgram.SetValue($"lights[{i}].specularColor", light.SpecularColor);
                ShaderProgram.SetValue($"lights[{i}].position", light.Position);
                ShaderProgram.SetValue($"lights[{i}].direction", light.Direction);
                ShaderProgram.SetValue($"lights[{i}].innerAngle", light.InnerAngle);
                ShaderProgram.SetValue($"lights[{i}].outerAngle", light.OuterAngle);
                ShaderProgram.SetValue($"lights[{i}].attenuationConstant", light.AttenuationConstant);
                ShaderProgram.SetValue($"lights[{i}].attenuationLinear", light.AttenuationLinear);
                ShaderProgram.SetValue($"lights[{i}].attenuationQuadratic", light.AttenuationQuadratic);
            }
        }
    }
}
