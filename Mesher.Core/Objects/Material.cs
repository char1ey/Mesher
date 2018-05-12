using System;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Material;
using Mesher.GraphicsCore.Texture;
using Mesher.Mathematics;

namespace Mesher.Core.Objects
{
    public class Material : IDocumentItem
    {
        private MesherGraphics m_graphics;

        public RMaterial RMaterial { get; private set; }
        public Int32 Id { get; internal set; }
        public Boolean HasColorAmbient { get; set; }
        public Color4 ColorAmbient { get; set; }

        public Boolean HasColorDiffuse { get; set; }
        public Color4 ColorDiffuse { get; set; }

        public Boolean HasColorSpecular { get; set; }
        public Color4 ColorSpecular { get; set; }

        public Boolean HasTextureAmbient { get; set; }
        public Texture TextureAmbient { get; set; }

        public Boolean HasTextureDiffuse { get; set; }
        public Texture TextureDiffuse { get; set; }

        public Boolean HasTextureSpecular { get; set; }
        public Texture TextureSpecular { get; set; }

        public Boolean HasTextureEmissive { get; set; }
        public Texture TextureEmissive { get; set; }
        public Boolean HasTextureNormal { get; set; }
        public Texture TextureNormal { get; set; }

        public Material(MesherGraphics graphics)
        {
            m_graphics = graphics;
            RMaterial = new RMaterial();
        }

        public void Rebuild()
        {
            RMaterial.HasColorAmbient = HasColorAmbient;
            if (HasColorAmbient)
                RMaterial.ColorAmbient = ColorAmbient;

            RMaterial.HasColorDiffuse = HasColorDiffuse;
            if (HasColorDiffuse)
                RMaterial.ColorDiffuse = ColorDiffuse;

            RMaterial.HasColorSpecular = HasColorSpecular;
            if (HasColorSpecular)
                RMaterial.ColorSpecular = ColorSpecular;


            RMaterial.HasTextureAmbient = HasTextureAmbient;
            if (HasTextureAmbient)
                RMaterial.TextureAmbient = TextureAmbient;

            RMaterial.HasTextureDiffuse = HasTextureDiffuse;
            if (HasTextureDiffuse)
                RMaterial.TextureDiffuse = TextureDiffuse;

            RMaterial.HasTextureSpecular = HasTextureSpecular;
            if (HasTextureSpecular)
                RMaterial.TextureSpecular = TextureSpecular;

            RMaterial.HasTextureEmissive = HasTextureEmissive;
            if (HasTextureEmissive)
                RMaterial.TextureEmissive = TextureEmissive;

            RMaterial.HasTextureNormal = HasTextureNormal;
            if (HasTextureNormal)
                RMaterial.TextureNormal = TextureNormal;
        }

        public void Render(IDocumentView documentView)
        {
        }
    }
}