using System;
using Mesher.Core.Collections;
using Mesher.Core.Components;
using Mesher.GraphicsCore.ShaderProgram;

namespace Mesher.Core.Objects.Scene
{
    public class Scene : IRenderItem, IDisposable
    {
        public Cameras Cameras { get; }

        public Meshes Meshes { get; }

        public Lights Lights { get; }

        public Scene()
        {
            Cameras = new Cameras();
            Meshes = new Meshes();
            Lights = new Lights();
        }

        public void Dispose()
        {

        }

        public void Render(SceneContext sceneContext, ShaderProgram shaderProgram)
        {
            shaderProgram.Bind();

            for (var i = 0; i < Meshes.Count; i++)
            {
                RenderLights(shaderProgram);

                sceneContext.Camera.Render(sceneContext, shaderProgram);
     
                Meshes[i].Render(sceneContext, shaderProgram);

                shaderProgram.SetBuffer(Meshes[i].Indicies);

                shaderProgram.Render(Meshes[i].IndexedRendering);
            }

            shaderProgram.Unbind();
        }

        private void RenderLights(ShaderProgram shaderProgram)
        {
            shaderProgram.SetValue("lightsCount", Lights.Count);

            for (var i = 0; i < Lights.Count; i++)
            {
                var light = Lights[i];

                shaderProgram.SetValue($"lights[{i}].lightType", (Int32)light.LightType);
                shaderProgram.SetValue($"lights[{i}].ambientColor", light.AmbientColor);
                shaderProgram.SetValue($"lights[{i}].diffuseColor", light.DiffuseColor);
                shaderProgram.SetValue($"lights[{i}].specularColor", light.SpecularColor);
                shaderProgram.SetValue($"lights[{i}].position", light.Position);
                shaderProgram.SetValue($"lights[{i}].direction", light.Direction);
                shaderProgram.SetValue($"lights[{i}].innerAngle", light.InnerAngle);
                shaderProgram.SetValue($"lights[{i}].outerAngle", light.OuterAngle);
                shaderProgram.SetValue($"lights[{i}].attenuationConstant", light.AttenuationConstant);
                shaderProgram.SetValue($"lights[{i}].attenuationLinear", light.AttenuationLinear);
                shaderProgram.SetValue($"lights[{i}].attenuationQuadratic", light.AttenuationQuadratic);
            }
        }
    }
}
