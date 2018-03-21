using System;
using Mesher.Core.Components;
using Mesher.GraphicsCore.ShaderProgram;
using Mesher.Mathematics;

namespace Mesher.Core.Objects.Light
{
    public class Light : IRenderItem
    {
        public Int32 Id { get; internal set; }
        public String Name { get; set; }
        public LightType LightType { get; set; }
        public Color3 AmbientColor { get; set; }
        public Color3 DiffuseColor { get; set; }
        public Color3 SpecularColor { get; set; }
        public Vec3 Position { get; set; }
        public Vec3 Direction { get; set; }
        public Single InnerAngle { get; set; }
        public Single OuterAngle { get; set; }
        public Single AttenuationConstant { get; set; }
        public Single AttenuationLinear { get; set; }
        public Single AttenuationQuadratic { get; set; }

        public void Render(SceneContextPrototype sceneContext, ShaderProgram shaderProgram)
        {
        }
    }
}
