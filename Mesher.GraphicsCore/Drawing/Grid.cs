namespace Mesher.GraphicsCore.Drawing
{
    public class Grid
    {
        public static void Draw()
        {
            for (float i = -50; i <= 50; i += 5)
            {
                Gl.Begin(Gl.GL_LINES);
                Gl.Color(149 / 256f, 147 / 256f, 149 / 256f, 1.0f);
                Gl.Vertex(-50, i);
                Gl.Vertex(50, i);
                Gl.Vertex(i, -50);
                Gl.Vertex(i, 50);
                Gl.End();
            }
        }
    }
}
