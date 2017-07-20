using System.Drawing;

namespace Mesher.GraphicsCore.Drawing
{
    public static class TextureTriangle
    {
        public static void Draw(Mathematics.Triangle triangle, Mathematics.Triangle textureTriangle, Texture.Texture texture)
        {
            texture.Activate();

            Gl.PolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);

            Gl.Color(Color.Transparent);

            Gl.Begin(Gl.GL_TRIANGLES);

            Gl.TexCoord(textureTriangle.A.X, textureTriangle.A.Y);
            Gl.Vertex(triangle.A.X, triangle.A.Y, triangle.A.Z);  
            
            Gl.TexCoord(textureTriangle.B.X, textureTriangle.B.Y);
            Gl.Vertex(triangle.B.X, triangle.B.Y, triangle.B.Z);   
            
            Gl.TexCoord(textureTriangle.C.X, textureTriangle.C.Y);
            Gl.Vertex(triangle.C.X, triangle.C.Y, triangle.C.Z);   

            Gl.End();

            texture.Deactivate();

            Gl.Flush();
        }
    }
}
