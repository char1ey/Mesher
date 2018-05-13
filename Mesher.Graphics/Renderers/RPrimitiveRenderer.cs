using Mesher.Graphics.Primitives;

namespace Mesher.Graphics.Renderers
{
    public abstract class RPrimitiveRenderer<T> where T : RPrimitive
    {
        public abstract void Render(T rTriangles, RenderArgs renderArgs);
    }
}