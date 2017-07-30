using System.Drawing;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Drawing
{
    public static class Triangle
    {
        public static void Draw(Mathematics.Triangle t, Color color)
        {
            Gl.Begin(Gl.GL_TRIANGLES);

            Gl.Color(color);
            Gl.Vertex(t.A);
            Gl.Vertex(t.B);
            Gl.Vertex(t.C);

            Gl.End();
        }
    }
}

