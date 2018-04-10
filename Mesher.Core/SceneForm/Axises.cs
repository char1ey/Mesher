using System;
using System.Drawing;
using Mesher.GraphicsCore;
using Mesher.Mathematics;

namespace Mesher.Core.SceneForm
{
    public class Axises : SceneContextComponent
    {
        private const Single AXIS_LENGTH = 45f;

        private bool intersect;

        public Axises(ISceneContext sceneContext) : base(sceneContext)
        {
        }

        public override void Draw()
        {
            var axisX = new Vec3(SceneContext.Camera.ViewMatrix.Col0).Normalize() * AXIS_LENGTH;
            var axisY = new Vec3(SceneContext.Camera.ViewMatrix.Col1).Normalize() * AXIS_LENGTH;
            var axisZ = new Vec3(SceneContext.Camera.ViewMatrix.Col2).Normalize() * AXIS_LENGTH;
            var p0 = new Vec3(50, 50, 0);

            DrawAxis(p0, axisX, Color.Blue);
            DrawAxis(p0, axisY, Color.Red);
            DrawAxis(p0, axisZ, Color.Green);
        }

        public override void MouseMove(Point location)
        {
            var p0 = new Vec3(50, 50, 0);
            var p1 = new Vec3(location.X, SceneContext.Height - location.Y, 0);

            if ((p0 - p1).Length() < 5f)
                intersect = true;
            else intersect = false;
        }

        public override void MouseClick()
        {
            throw new NotImplementedException();
        }

        private void DrawAxis(Vec3 p0, Vec3 direction, Color color)
        {
            if (intersect)
                Gl.LineWidth(5.0f);
            else Gl.LineWidth(3.0f);

            Gl.Color(color);
            Gl.Begin(Gl.GL_LINES);
            Gl.Vertex(ToWorld(p0));
            Gl.Vertex(ToWorld(p0 + direction));
            Gl.End();
        }

        private Vec3 ToScreen(Vec3 p)
        {
            return (p + 1) / 2 * new Vec3(SceneContext.Width, SceneContext.Height, 0);
        }

        private Vec3 ToWorld(Vec3 p)
        {
            return new Vec3(p.X / SceneContext.Width, p.Y / SceneContext.Height, 0) * 2 - 1;
        }
    }
}