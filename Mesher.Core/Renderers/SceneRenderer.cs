using System;
using Mesher.Core.Collections;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.ShaderProgram;
using Mesher.Mathematics;
using Camera = Mesher.Core.Objects.Camera;
using Material = Mesher.Core.Objects.Material.Material;
using Mesh = Mesher.Core.Objects.Mesh.Mesh;
using Scene = Mesher.Core.Objects.Scene.Scene;

namespace Mesher.Core.Renderers
{
    public class SceneRenderer : SceneRendererBase
    {
        public SceneRenderer(DataContext dataContext, String vertexShaderSource, String fragmentShaderSource) 
            : base(dataContext, vertexShaderSource, fragmentShaderSource)
        {
        }

        public override void Render(Scene scene, GraphicsCore.Camera.Camera camera)
        {
            ShaderProgram.Bind();

            foreach (var mesh in scene.Meshes)
            {
                InitLights(scene.Lights);

                InitCamera(camera);

                RenderMesh(mesh);
            }

            ShaderProgram.Unbind();
        }

        private void InitCamera(GraphicsCore.Camera.Camera camera)
        {
            ShaderProgram.SetValue("proj", camera.ProjectionMatrix);
            ShaderProgram.SetValue("modelView", camera.ViewMatrix);
        }

        private void RenderMesh(Mesh mesh)
        {
            ShaderProgram.SetValue("hasPosition", mesh.HasVertexes);
            if (mesh.HasVertexes)
                ShaderProgram.SetBuffer<Vec3>("position", mesh.Vertexes, 3);

            ShaderProgram.SetValue("hasNormal", mesh.HasNormals);
            if (mesh.HasNormals)
                ShaderProgram.SetBuffer<Vec3>("normal", mesh.Normals, 3);

            ShaderProgram.SetValue("hasTexCoord", mesh.HasTextureVertexes);
            if (mesh.HasTextureVertexes)
                ShaderProgram.SetBuffer<Vec2>("texCoord", mesh.TextureVertexes, 2);

            ShaderProgram.SetValue("hasTangentBasis", mesh.HasTangentBasis);
            if (mesh.HasTangentBasis)
            {
                ShaderProgram.SetBuffer<Vec3>("tangent", mesh.Tangents, 3);
                ShaderProgram.SetBuffer<Vec3>("biTangent", mesh.BiTangents, 3);
            }

            if (mesh.HasMaterial)
                InitMaterial(mesh.Material);

            ShaderProgram.SetBuffer(mesh.Indicies);

            ShaderProgram.RenderTriangles(mesh.IndexedRendering);
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
