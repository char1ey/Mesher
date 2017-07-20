using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace Mesher.GraphicsCore.Texture
{
    public class Texture: IDisposable
    {
        private static readonly int[] ActiveTextures;

        static Texture()
        {
            var textureUnitsCount = new int[1];
            Gl.GetInteger(Gl.GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS, textureUnitsCount);

            ActiveTextures = new int[textureUnitsCount[0]];

            for (var i = 0; i < ActiveTextures.Length; i++)
                ActiveTextures[i] = -1;
        }

        private readonly uint[] m_id;

        public Texture(Bitmap image)
        {
            m_id = new uint[1];

            var d = image.LockBits(new Rectangle(Point.Empty, image.Size), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            image.UnlockBits(d);

            Gl.GenTextures(1, m_id);

            Activate();

            Gl.TexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, image.Width, image.Height, 0, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, d.Scan0);
            Gl.TexParameter(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);
            Gl.TexParameter(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);
            
            Deactivate();
        }

        public int Activate()
        {
            for (uint i = 0; i < ActiveTextures.Length; i++)
            {
                if (ActiveTextures[i] == -1)
                {
                    Gl.ActiveTexture(Gl.GL_TEXTURE0 + i);
                    Gl.BindTexture(Gl.GL_TEXTURE_2D, m_id[0]);
                    ActiveTextures[i] = (int) m_id[0];
                }

                if (ActiveTextures[i] == m_id[0])
                {
                    Gl.Enable(Gl.GL_TEXTURE_2D);
                    return (int) i;
                }
            }

            return -1;
        }

        public void Deactivate()
        {
            for(uint i = 0; i < ActiveTextures.Length; i++)
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
