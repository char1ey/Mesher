using System;

namespace Mesher.Graphics.Data.OpenGL
{
    public class GlFrameBuffer : IFrameBuffer, IBindableItem
    {
        public void AttachDepthTexture(Texture.Texture texture)
        {
            throw new NotImplementedException();
        }

        public void AttachColorTexture(Texture.Texture texture)
        {
            throw new NotImplementedException();
        }

        public void Bind()
        {
            throw new NotImplementedException();
        }

        public void Unbind()
        {
            throw new NotImplementedException();
        }
    }
}
