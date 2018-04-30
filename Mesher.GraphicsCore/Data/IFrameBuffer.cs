namespace Mesher.GraphicsCore.Data
{
    public interface IFrameBuffer
    {
        void AttachDepthTexture(Texture.Texture texture);
        void AttachColorTexture(Texture.Texture texture);
    }
}