﻿using System;
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
        public GraphicsCore.Texture.GlTexture TextureAmbient { get; set; }

        public Boolean HasTextureDiffuse { get; set; }
        public GraphicsCore.Texture.GlTexture TextureDiffuse { get; set; }

        public Boolean HasTextureSpecular { get; set; }
        public GraphicsCore.Texture.GlTexture TextureSpecular { get; set; }

        public Boolean HasTextureEmissive { get; set; }
        public GraphicsCore.Texture.GlTexture TextureEmissive { get; set; }
        public Boolean HasTextureNormal { get; set; }
        public GraphicsCore.Texture.GlTexture TextureNormal { get; set; }

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
