using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Graphics.Camera;
using Mesher.Mathematics;

namespace Mesher.Core
{
    public class ArcBallCameraControler : CameraControler
    {
        private const Single ROTATION_SPEED = 5f;
        private const Single ZOOM_SPEED = 1.2f;

        private Point m_previousMousePosition;

        public ArcBallCameraControler(IDocumentView documentView) : base(documentView)
        {
        }

        public override void Inaction(Point currentScreenCoordinate)
        {
            DocumentView.Camera.ClearStack();
            m_previousMousePosition = currentScreenCoordinate;
        }

        public override void Move(Point currentScreenCoordinate)
        {
            DocumentView.Camera.Pop();

            var a = DocumentView.Camera.UnProject(m_previousMousePosition.X, DocumentView.Height - m_previousMousePosition.Y, 0, DocumentView.Width, DocumentView.Height);
            var b = DocumentView.Camera.UnProject(currentScreenCoordinate.X, DocumentView.Height - currentScreenCoordinate.Y, 0, DocumentView.Width, DocumentView.Height);

            DocumentView.Camera.Push();
            DocumentView.Camera.Move(a - b);
        }

        public override void Rotate(Point currentScreeCoordinate)
        {
            DocumentView.Camera.Pop();

            DocumentView.Camera.Push();

            var v0 = GetArcBallVector(m_previousMousePosition.X, m_previousMousePosition.Y);
            var v1 = GetArcBallVector(currentScreeCoordinate.X, currentScreeCoordinate.Y);

            var axis = Mat3.Inverse(DocumentView.Camera.ViewMatrix.ToMat3()) * (DocumentView.Camera.ProjectionMatrix.ToMat3() * v0.Cross(v1).Normalize()).Normalize();
            var angle = v0.Angle(v1);

            DocumentView.Camera.Rotate(axis, -angle * ROTATION_SPEED);
        }

        public override void Zoom(Point currentScreenCoordinate, Single delta)
        {
            var zoom = delta * ZOOM_SPEED;
            ((OrthographicRCamera)DocumentView.Camera).Zoom(zoom < 0 ? -1 / zoom : zoom);

            var a = DocumentView.Camera.UnProject(DocumentView.Width / 2f, DocumentView.Height / 2f, 0, DocumentView.Width, DocumentView.Height);
            var b = DocumentView.Camera.UnProject(currentScreenCoordinate.X, DocumentView.Height - currentScreenCoordinate.Y, 0, DocumentView.Width, DocumentView.Height);

            a = Plane.XYPlane.Cross(new Line(a, (DocumentView.Camera.LookAtPoint - DocumentView.Camera.Position).Normalize()));
            b = Plane.XYPlane.Cross(new Line(b, (DocumentView.Camera.LookAtPoint - DocumentView.Camera.Position).Normalize()));

            DocumentView.Camera.Move((b - a) * zoom / 7.2f);
        }

        private Vec3 GetArcBallVector(Int32 x, Int32 y)
        {
            var ret = new Vec3(2f * x / DocumentView.Width - 1, 2f * y / DocumentView.Height - 1, 0);

            ret.Y = -ret.Y;

            var opSqr = ret.X * ret.X + ret.Y * ret.Y;

            if (opSqr <= 1)
                ret.Z = (Single)Math.Sqrt(1 - opSqr);
            else ret = ret.Normalize();

            return ret;
        }
    }
}
