using Mesher.GraphicsCore.Primitives;
using Mesher.GraphicsCore.RenderContexts;

namespace Mesher.GraphicsCore.Renderers
{
    public abstract class RPrimitiveRenderer<T> where T : RPrimitive
    {
        public abstract void Render(T rItem, IRenderContext renderContext);
    }
}