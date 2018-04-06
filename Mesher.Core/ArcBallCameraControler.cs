using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Mathematics;

namespace Mesher.Core
{
    public class ArcBallCameraControler : CameraControler
    {
        private const Single ROTATION_SPEED = 5f;
        private const Single ZOOM_SPEED = 1.2f;

        private Point m_previousMousePosition;

        public ArcBallCameraControler(SceneForm.SceneForm sceneForm) : base(sceneForm)
        {
        }

        public override void ReleseControler(Point currentScreenCoordinate)
        {
            SceneForm.Camera.ClearStack();
            m_previousMousePosition = currentScreenCoordinate;
        }

        public override void Move(Point currentScreenCoordinate)
        {
            SceneForm.Camera.Pop();

            var a = SceneForm.Camera.UnProject(m_previousMousePosition.X, SceneForm.Height - m_previousMousePosition.Y, 0, SceneForm.Width, SceneForm.Height);
            var b = SceneForm.Camera.UnProject(currentScreenCoordinate.X, SceneForm.Height - currentScreenCoordinate.Y, 0, SceneForm.Width, SceneForm.Height);

            SceneForm.Camera.Push();
            SceneForm.Camera.Move(a - b);
        }

        public override void Rotate(Point currentScreeCoordinate)
        {
            SceneForm.Camera.Pop();

            SceneForm.Camera.Push();

            var v0 = GetArcBallVector((Int32)m_previousMousePosition.X, (Int32)m_previousMousePosition.Y);
            var v1 = GetArcBallVector(currentScreeCoordinate.X, currentScreeCoordinate.Y);

            var axis = Mat3.Inverse(SceneForm.Camera.ViewMatrix.ToMat3()) * (SceneForm.Camera.ProjectionMatrix.ToMat3() * v0.Cross(v1).Normalize()).Normalize();
            var angle = v0.Angle(v1);

            SceneForm.Camera.Rotate(axis, -angle * ROTATION_SPEED);
        }

        public override void Zoom(Point currentScreenCoordinate, Single delta)
        {
            var zoom = delta * ZOOM_SPEED;
            SceneForm.Camera.Zoom(zoom < 0 ? -1 / zoom : zoom);

            var a = SceneForm.Camera.UnProject(SceneForm.Width / 2f, SceneForm.Height / 2f, 0, SceneForm.Width, SceneForm.Height);
            var b = SceneForm.Camera.UnProject(currentScreenCoordinate.X, SceneForm.Height - currentScreenCoordinate.Y, 0, SceneForm.Width, SceneForm.Height);

            a = Plane.XYPlane.Cross(new Line(a, (SceneForm.Camera.LookAtPoint - SceneForm.Camera.Position).Normalize()));
            b = Plane.XYPlane.Cross(new Line(b, (SceneForm.Camera.LookAtPoint - SceneForm.Camera.Position).Normalize()));

            SceneForm.Camera.Move((b - a) * zoom / 7.2f);
        }

        private Vec3 GetArcBallVector(Int32 x, Int32 y)
        {
            var ret = new Vec3(2f * x / SceneForm.Width - 1, 2f * y / SceneForm.Height - 1, 0);

            ret.Y = -ret.Y;

            var opSqr = ret.X * ret.X + ret.Y * ret.Y;

            if (opSqr <= 1)
                ret.Z = (Single)Math.Sqrt(1 - opSqr);
            else ret = ret.Normalize();

            return ret;
        }
    }
}
