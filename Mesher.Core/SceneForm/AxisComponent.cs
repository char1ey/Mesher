using System;
using System.Drawing;
using Mesher.GraphicsCore;
using Mesher.Mathematics;

namespace Mesher.Core.SceneForm
{
    public class AxisComponent : SceneContextComponent
    {
        private const Single AXIS_LENGTH = 0.2f;
        public AxisComponent(SceneForm sceneForm) : base(sceneForm)
        {
        }

        public override void Draw()
        {
            var ratio = (Single) SceneForm.Width / SceneForm.Height;

            Single x, y;
            if (SceneForm.Width > SceneForm.Height)
            {
                x = 1 / ratio;
                y = 1;
            }
            else
            {
                x = 1;
                y = ratio;
            }

            var v = new Vec3(x, y, 1);

            var axisX = new Vec3(SceneForm.Camera.ViewMatrix.Col0).Normalize() * AXIS_LENGTH * v;
            var axisY = new Vec3(SceneForm.Camera.ViewMatrix.Col1).Normalize() * AXIS_LENGTH * v;
            var axisZ = new Vec3(SceneForm.Camera.ViewMatrix.Col2).Normalize() * AXIS_LENGTH * v;
            var p0 = new Vec3(-0.8f * y, -0.9f * x, 0);

            DrawAxis(p0, axisX, Color.Blue);
            DrawAxis(p0, axisY, Color.Red);
            DrawAxis(p0, axisZ, Color.Green);
        }

        private void DrawAxis(Vec3 p0, Vec3 direction, Color color)
        {
            Gl.LineWidth(3.0f);
            Gl.Color(color);
            Gl.Begin(Gl.GL_LINES);
            Gl.Vertex(p0);
            Gl.Vertex(p0 + direction);
            Gl.End();
        }
    }
}