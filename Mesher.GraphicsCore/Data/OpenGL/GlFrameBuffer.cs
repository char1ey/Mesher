using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.GraphicsCore.Buffers;

namespace Mesher.GraphicsCore.Data.OpenGL
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
