using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Mesher.GraphicsCore.Texture
{
    public class Texture: IDisposable
    {
        private static readonly Int32[] ActiveTextures;
		private readonly UInt32[] m_id;

		public Int32 Width { get; private set; }
		public Int32 Height { get; private set; }

        static Texture()
        {
            var textureUnitsCount = new Int32[1];
            Gl.GetInteger(Gl.GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS, textureUnitsCount);

            ActiveTextures = new Int32[textureUnitsCount[0]];

            for (var i = 0; i < ActiveTextures.Length; i++)
                ActiveTextures[i] = -1;
        }

        public Texture(Bitmap image)
        {
            m_id = new UInt32[1];

	        Width = image.Width;
	        Height = image.Height;

            var d = image.LockBits(new Rectangle(Point.Empty, image.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
           
            Gl.GenTextures(1, m_id);

            Activate();       

            Gl.TexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, image.Width, image.Height, 0, Gl.GL_BGRA, Gl.GL_UNSIGNED_BYTE, d.Scan0);
            
            Gl.TexParameter(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);
            Gl.TexParameter(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
            
            Deactivate();

            image.UnlockBits(d);
        }

	    public Texture(Int32 width, Int32 height)
	    {
			m_id = new UInt32[1];

		    Width = width;
		    Height = height;

			Gl.GenTextures(1, m_id);

			Activate();

			Gl.TexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, width, height, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, IntPtr.Zero);

			Deactivate();
		}

	    public void SetSubTexture(Int32 posX, Int32 posY, Int32 width, Int32 height, Int32[] pixelsData)
	    {
		    Activate();

		    Gl.TexSubImage2D(Gl.GL_TEXTURE_2D, 0, posX, posY, width, height, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, pixelsData);

			Deactivate();
	    }

        public Int32 Activate()
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

        public void Deactivate()
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

        public void Dispose()
        {
            Deactivate();
            Gl.DeleteTextures(1, m_id);
        }
    }
}
