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

        public ArcBallCameraControler(ISceneContext sceneContext) : base(sceneContext)
        {
        }

        public override void Inaction(Point currentScreenCoordinate)
        {
            SceneContext.Camera.ClearStack();
            m_previousMousePosition = currentScreenCoordinate;
        }

        public override void Move(Point currentScreenCoordinate)
        {
            SceneContext.Camera.Pop();

            var a = SceneContext.Camera.UnProject(m_previousMousePosition.X, SceneContext.Height - m_previousMousePosition.Y, 0, SceneContext.Width, SceneContext.Height);
            var b = SceneContext.Camera.UnProject(currentScreenCoordinate.X, SceneContext.Height - currentScreenCoordinate.Y, 0, SceneContext.Width, SceneContext.Height);

            SceneContext.Camera.Push();
            SceneContext.Camera.Move(a - b);
        }

        public override void Rotate(Point currentScreeCoordinate)
        {
            SceneContext.Camera.Pop();

            SceneContext.Camera.Push();

            var v0 = GetArcBallVector((Int32)m_previousMousePosition.X, (Int32)m_previousMousePosition.Y);
            var v1 = GetArcBallVector(currentScreeCoordinate.X, currentScreeCoordinate.Y);

            var axis = Mat3.Inverse(SceneContext.Camera.ViewMatrix.ToMat3()) * (SceneContext.Camera.ProjectionMatrix.ToMat3() * v0.Cross(v1).Normalize()).Normalize();
            var angle = v0.Angle(v1);

            SceneContext.Camera.Rotate(axis, -angle * ROTATION_SPEED);
        }

        public override void Zoom(Point currentScreenCoordinate, Single delta)
        {
            var zoom = delta * ZOOM_SPEED;
            SceneContext.Camera.Zoom(zoom < 0 ? -1 / zoom : zoom);

            var a = SceneContext.Camera.UnProject(SceneContext.Width / 2f, SceneContext.Height / 2f, 0, SceneContext.Width, SceneContext.Height);
            var b = SceneContext.Camera.UnProject(currentScreenCoordinate.X, SceneContext.Height - currentScreenCoordinate.Y, 0, SceneContext.Width, SceneContext.Height);

            a = Plane.XYPlane.Cross(new Line(a, (SceneContext.Camera.LookAtPoint - SceneContext.Camera.Position).Normalize()));
            b = Plane.XYPlane.Cross(new Line(b, (SceneContext.Camera.LookAtPoint - SceneContext.Camera.Position).Normalize()));

            SceneContext.Camera.Move((b - a) * zoom / 7.2f);
        }

        private Vec3 GetArcBallVector(Int32 x, Int32 y)
        {
            var ret = new Vec3(2f * x / SceneContext.Width - 1, 2f * y / SceneContext.Height - 1, 0);

            ret.Y = -ret.Y;

            var opSqr = ret.X * ret.X + ret.Y * ret.Y;

            if (opSqr <= 1)
                ret.Z = (Single)Math.Sqrt(1 - opSqr);
            else ret = ret.Normalize();

            return ret;
        }
    }
}
