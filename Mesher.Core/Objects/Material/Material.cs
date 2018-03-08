using System;
using Mesher.Core.Components;
using Mesher.GraphicsCore.ShaderProgram;
using Mesher.Mathematics;

namespace Mesher.Core.Objects.Material
{
    public class Material : IRenderItem, IDisposable
    {
        public Int32 Id { get; internal set; }
        public Boolean HasColorAmbient { get; set; }
        public Color4 ColorAmbient { get; set; }

        public Boolean HasColorDiffuse { get; set; }
        public Color4 ColorDiffuse { get; set; }

        public Boolean HasColorSpecular { get; set; }
        public Color4 ColorSpecular { get; set; }

        public Boolean HasTextureAmbient { get; set; }
        public GraphicsCore.Texture.Texture TextureAmbient { get; set; }

        public Boolean HasTextureDiffuse { get; set; }
        public GraphicsCore.Texture.Texture TextureDiffuse { get; set; }

        public Boolean HasTextureSpecular { get; set; }
        public GraphicsCore.Texture.Texture TextureSpecular { get; set; }

        public Boolean HasTextureEmissive { get; set; }
        public GraphicsCore.Texture.Texture TextureEmissive { get; set; }
        public Boolean HasTextureNormal { get; set; }
        public GraphicsCore.Texture.Texture TextureNormal { get; set; }

        public void Activate()
        {
            TextureAmbient?.Bind();
            TextureDiffuse?.Bind();
            TextureSpecular?.Bind();
            TextureEmissive?.Bind();
            TextureNormal?.Bind();
        }

        public void Deactivate()
        {
            TextureAmbient?.Unbind();
            TextureDiffuse?.Unbind();
            TextureSpecular?.Unbind();
            TextureEmissive?.Unbind();
            TextureNormal?.Unbind();
        }

        public void Dispose()
        {
            TextureAmbient?.Dispose();
            TextureDiffuse?.Dispose();
            TextureSpecular?.Dispose();
            TextureEmissive?.Dispose();
            TextureNormal?.Dispose();
        }

        public void Render(SceneContext sceneContext, ShaderProgram shaderProgram)
        {
            shaderProgram.SetValue("material.hasColorAmbient", HasColorAmbient);
            if (HasColorAmbient)
                shaderProgram.SetValue("material.colorAmbient", ColorAmbient);

            shaderProgram.SetValue("material.hasColorDiffuse", HasColorDiffuse);
            if (HasColorDiffuse)
                shaderProgram.SetValue("material.colorDiffuse", ColorDiffuse);

            shaderProgram.SetValue("material.hasColorSpecular", HasColorSpecular);
            if (HasColorSpecular)
                shaderProgram.SetValue("material.colorSpecular", ColorSpecular);

            shaderProgram.SetValue("material.hasTextureAmbient", HasTextureAmbient);
            if (HasTextureAmbient)
                shaderProgram.SetValue("material.textureAmbient", TextureAmbient);

            shaderProgram.SetValue("material.hasTextureDiffuse", HasTextureDiffuse);
            if (HasTextureDiffuse)
                shaderProgram.SetValue("material.textureDiffuse", TextureDiffuse);

            shaderProgram.SetValue("material.hasTextureSpecular", HasTextureSpecular);
            if (HasTextureSpecular)
                shaderProgram.SetValue("material.textureSpecular", TextureSpecular);

            shaderProgram.SetValue("material.hasTextureNormal", HasTextureNormal);
            if (HasTextureNormal)
                shaderProgram.SetValue("material.textureNormal", TextureNormal);
        }
    }
}
