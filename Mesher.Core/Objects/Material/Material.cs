using System;
using Mesher.Core.Components;
using Mesher.GraphicsCore.ShaderProgram;
using Mesher.Mathematics;

namespace Mesher.Core.Objects.Material
{
    public class Material : IDisposable
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

        public void Dispose()
        {
            TextureAmbient?.Dispose();
            TextureDiffuse?.Dispose();
            TextureSpecular?.Dispose();
            TextureEmissive?.Dispose();
            TextureNormal?.Dispose();
        }
    }
}
