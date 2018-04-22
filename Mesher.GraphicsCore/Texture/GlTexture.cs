using System;
using System.Drawing;
using System.Drawing.Imaging;
using Mesher.GraphicsCore.Buffers;

namespace Mesher.GraphicsCore.Texture
{
    public class GlTexture: Texture, IBindableItem
    {
        private static readonly Int32[] ActiveTextures;

		private UInt32[] m_id;

        private DataContext m_dataContext;

        internal DataContext DataContext { get { return m_dataContext; } }

        static GlTexture()
        {
            var textureUnitsCount = new Int32[1];
            Gl.GetInteger(Gl.GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS, textureUnitsCount);

            ActiveTextures = new Int32[textureUnitsCount[0]];

            for (var i = 0; i < ActiveTextures.Length; i++)
                ActiveTextures[i] = -1;
        }

        internal GlTexture(Bitmap image, DataContext dataContext) : base(image.Width, image.Height, PixelFormat.Format32)
        {
            m_dataContext = dataContext;

	        Width = image.Width;
	        Height = image.Height;

            var d = image.LockBits(new Rectangle(Point.Empty, image.Size), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
           
            GenTexture();
            SetSize(Width, Height, d.Scan0);

            image.UnlockBits(d);
        }

	    internal GlTexture(Int32 width, Int32 height, DataContext dataContext) : this(width, height, IntPtr.Zero, dataContext)
	    {
		}

        internal GlTexture(Int32 width, Int32 height, IntPtr pixels, DataContext dataContext) : base(width, height, PixelFormat.Format32)
        {
            m_dataContext = dataContext;

            Width = width;
            Height = height;

            GenTexture();
            SetSize(width, height, pixels);
        }

        private void GenTexture()
        {
            m_id = new UInt32[1];

            Bind();

            Gl.GenTextures(1, m_id);

            Unbind();
        }

        public void SetSize(Int32 width, Int32 height, IntPtr data)
        {
            Bind();
            Gl.TexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, width, height, 0, Gl.GL_BGRA, Gl.GL_UNSIGNED_BYTE, data);
            Gl.TexParameter(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
            Gl.TexParameter(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);
            Gl.TexParameter(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_CLAMP_TO_EDGE);
            Gl.TexParameter(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_CLAMP_TO_EDGE);
            Unbind();
            Width = width;
            Height = height;
        }

        public unsafe void SetSubTexture(Int32 posX, Int32 posY, Int32 width, Int32 height, Int32[] pixelsData, Int32 offset = 0)
        {
            fixed (Int32* p = pixelsData)
                SetSubTexture(posX, posY, width, height, new IntPtr(p + offset));
        }

        public unsafe void SetSubTexture(Int32 posX, Int32 posY, Int32 width, Int32 height, UInt32[] pixelsData, Int32 offset = 0)
        {
            fixed (UInt32* p = pixelsData)
                SetSubTexture(posX, posY, width, height, new IntPtr(p + offset));
        }

        public override IntPtr GetSubTexture(Int32 x, Int32 y, Int32 width, Int32 height)
        {
            throw new NotImplementedException();
        }

        public override void SetSubTexture(Int32 posX, Int32 posY, Int32 width, Int32 height, IntPtr p)
        {
            Bind();

            Gl.TexSubImage2D(Gl.GL_TEXTURE_2D, 0, posX, posY, width, height, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, p);

            Unbind();
        }


        public Int32 Bind()
        {
            for (UInt32 i = 0; i < ActiveTextures.Length; i++)
            {
                if (ActiveTextures[i] == -1)
                {
                    Gl.ActiveTexture(Gl.GL_TEXTURE0 + i);
                    Gl.BindTexture(Gl.GL_TEXTURE_2D, m_id[0]);
                    ActiveTextures[i] = (Int32) m_id[0];
                }

                if (ActiveTextures[i] == m_id[0])
                {
                    Gl.Enable(Gl.GL_TEXTURE_2D);
                    return (Int32) i;
                }
            }

            return -1;
        }

        public void Unbind()
        {
            for(UInt32 i = 0; i < ActiveTextures.Length; i++)
                if (ActiveTextures[i] == m_id[0])
                {
                    ActiveTextures[i] = -1;
                    Gl.ActiveTexture(Gl.GL_TEXTURE0 + i);
                    Gl.BindTexture(Gl.GL_TEXTURE_2D, 0);
                    Gl.Disable(Gl.GL_TEXTURE_2D);
                    break;
                }
        }

        public override void Dispose()
        {
            Unbind();
            Gl.DeleteTextures(1, m_id);
        }

        void IBindableItem.Bind()
        {
            Bind();
        }

        void IBindableItem.Unbind()
        {
            Unbind();
        }
    }
}
