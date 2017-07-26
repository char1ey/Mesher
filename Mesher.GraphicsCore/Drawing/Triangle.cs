using System.Drawing;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Drawing
{
    public static class Triangle
    {
        public static void Draw(Vertex a, Vertex b, Vertex c, Color color, RenderContextPrototype renderContext)
        {
            Gl.Begin(Gl.GL_TRIANGLES);

            Gl.Color(color);
            Gl.Vertex(a);
            Gl.Vertex(b);
            Gl.Vertex(c);

            Gl.End();
        }
    }
}

