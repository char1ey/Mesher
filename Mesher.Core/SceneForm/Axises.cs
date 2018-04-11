using System;
using System.Drawing;
using Mesher.Core.Renderers;
using Mesher.GraphicsCore;
using Mesher.Mathematics;

namespace Mesher.Core.SceneForm
{
    public class Axises : SceneContextComponent
    {
        private const Single AXIS_LENGTH = 75f;
        private const Single PLANE_SIZE = 20f;

        private static readonly Color XColorSel = Color.Blue;
        private static readonly Color XColor = Color.FromArgb(255, XColorSel.R / 2, XColorSel.G / 2, XColorSel.B / 2);
        private static readonly Color YColorSel = Color.Red;
        private static readonly Color YColor = Color.FromArgb(255, YColorSel.R / 2, YColorSel.G / 2, YColorSel.B / 2);
        private static readonly Color ZColorSel = Color.Green;
        private static readonly Color ZColor = Color.FromArgb(255, ZColorSel.R / 2, ZColorSel.G / 2, ZColorSel.B / 2);

        private bool intersect;

        private Vec3 m_axisesCenter;

        private Color m_axisXColor;
        private Color m_axisYColor;
        private Color m_axisZColor;
        private Color m_planeXYColor;
        private Color m_planeYZColor;
        private Color m_planeZXColor;

        public Axises(ISceneContext sceneContext) : base(sceneContext)
        {
            m_axisesCenter = new Vec3(80, 80, 0);
            m_axisXColor = XColor;
            m_axisYColor = YColor;
            m_axisZColor = ZColor;
            m_planeXYColor = XColor;
            m_planeYZColor = YColor;
            m_planeZXColor = ZColor;
        }

        public override void Draw(SceneContextGraphics graphics)
        {
            var axisX = new Vec3(SceneContext.Camera.ViewMatrix.Col0).Normalize();
            var axisY = new Vec3(SceneContext.Camera.ViewMatrix.Col1).Normalize();
            var axisZ = new Vec3(SceneContext.Camera.ViewMatrix.Col2).Normalize();
            var p0 = m_axisesCenter;

            graphics.DrawLine(p0, p0 + axisX * AXIS_LENGTH, 3f, m_axisXColor);
            graphics.DrawLine(p0, p0 + axisY * AXIS_LENGTH, 3f, m_axisYColor);
            graphics.DrawLine(p0, p0 + axisZ * AXIS_LENGTH, 3f, m_axisZColor);

            graphics.DrawQuad(p0, p0 + axisX * PLANE_SIZE, p0 + axisY * PLANE_SIZE, p0 + axisX * PLANE_SIZE + axisY * PLANE_SIZE, m_planeXYColor);
            graphics.DrawQuad(p0, p0 + axisY * PLANE_SIZE, p0 + axisZ * PLANE_SIZE, p0 + axisY * PLANE_SIZE + axisZ * PLANE_SIZE, m_planeYZColor);
            graphics.DrawQuad(p0, p0 + axisZ * PLANE_SIZE, p0 + axisX * PLANE_SIZE, p0 + axisZ * PLANE_SIZE + axisX * PLANE_SIZE, m_planeZXColor);
        }

        public override void MouseMove(Point location)
        {
            InitColor(XColor, YColor, ZColor, XColor, YColor, ZColor);

            if (CheckPlaneXYIntersect(location))
                InitColor(XColorSel, YColor, ZColor, XColorSel, YColorSel, ZColor);

            if (CheckPlaneYZIntersect(location))
                InitColor(XColor, YColorSel, ZColor, XColor, YColorSel, ZColorSel);

            if (CheckPlaneZXIntersect(location))
                InitColor(XColor, YColor, ZColorSel, XColorSel, YColor, ZColorSel);
        }

        private void InitColor(Color xy, Color yz, Color zx, Color x, Color y, Color z)
        {
            m_axisXColor = x;
            m_axisYColor = y;
            m_axisZColor = z;
            m_planeXYColor = xy;
            m_planeYZColor = yz;
            m_planeZXColor = zx;
        }

        public override void MouseClick(Point location)
        {
            if (CheckPlaneXYIntersect(location))
            {
                var len = (SceneContext.Camera.Position - SceneContext.Camera.LookAtPoint).Length();
                SceneContext.Camera.Position = SceneContext.Camera.LookAtPoint + new Vec3(0, 0, 1) * len;
                SceneContext.Camera.UpVector = new Vec3(0, 1, 0);
            }
            else if (CheckPlaneYZIntersect(location))
            {
                var len = (SceneContext.Camera.Position - SceneContext.Camera.LookAtPoint).Length();
                SceneContext.Camera.Position = SceneContext.Camera.LookAtPoint + new Vec3(1, 0, 0) * len;
                SceneContext.Camera.UpVector = new Vec3(0, 0, 1);
            }
            else if (CheckPlaneZXIntersect(location))
            {
                var len = (SceneContext.Camera.Position - SceneContext.Camera.LookAtPoint).Length();
                SceneContext.Camera.Position = SceneContext.Camera.LookAtPoint + new Vec3(0, 1, 0) * len;
                SceneContext.Camera.UpVector = new Vec3(1, 0, 0);
            }
        }

        private bool CheckPlaneXYIntersect(Point p)
        {
            var axisX = new Vec3(SceneContext.Camera.ViewMatrix.Col0).Normalize();
            var axisY = new Vec3(SceneContext.Camera.ViewMatrix.Col1).Normalize();

            var p0 = m_axisesCenter;
            var p1 = p0 + axisX * PLANE_SIZE;
            var p2 = p0 + axisY * PLANE_SIZE;
            var p3 = p0 + axisX * PLANE_SIZE + axisY * PLANE_SIZE;

            var pt = new Vec3(p.X, p.Y, 0);

            return PointInTriangle(pt, p0, p1, p2) || PointInTriangle(pt, p1, p2, p3);
        }

        private bool CheckPlaneYZIntersect(Point p)
        {
            var axisY = new Vec3(SceneContext.Camera.ViewMatrix.Col1).Normalize();
            var axisZ = new Vec3(SceneContext.Camera.ViewMatrix.Col2).Normalize();

            var p0 = m_axisesCenter;
            var p1 = p0 + axisZ * PLANE_SIZE;
            var p2 = p0 + axisY * PLANE_SIZE;
            var p3 = p0 + axisZ * PLANE_SIZE + axisY * PLANE_SIZE;

            var pt = new Vec3(p.X, p.Y, 0);

            return PointInTriangle(pt, p0, p1, p2) || PointInTriangle(pt, p1, p2, p3);
        }

        private bool CheckPlaneZXIntersect(Point p)
        {
            var axisX = new Vec3(SceneContext.Camera.ViewMatrix.Col0).Normalize();
            var axisZ = new Vec3(SceneContext.Camera.ViewMatrix.Col2).Normalize();

            var p0 = m_axisesCenter;
            var p1 = p0 + axisZ * PLANE_SIZE;
            var p2 = p0 + axisX * PLANE_SIZE;
            var p3 = p0 + axisZ * PLANE_SIZE + axisX * PLANE_SIZE;

            var pt = new Vec3(p.X, p.Y, 0);

            return PointInTriangle(pt, p0, p1, p2) || PointInTriangle(pt, p1, p2, p3);
        }

        private bool PointInTriangle(Vec3 pt, Vec3 v1, Vec3 v2, Vec3 v3)
        {
            var b1 = Sign(pt, v1, v2) < 0.0f;
            var b2 = Sign(pt, v2, v3) < 0.0f;
            var b3 = Sign(pt, v3, v1) < 0.0f;

            return b1 == b2 && b2 == b3;
        }
        private float Sign(Vec3 p1, Vec3 p2, Vec3 p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }
    }
}