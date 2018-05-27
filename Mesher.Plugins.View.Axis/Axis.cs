using Mesher.Core;
using Mesher.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mesher.Graphics;
using Mesher.Graphics.Collections;
using Mesher.Graphics.Material;
using Mesher.Graphics.Primitives;
using Mesher.Mathematics;

namespace Mesher.Plugins.View.Axis
{
    public class Axis : ViewDependetPlugin
    {
        private const Single AXIS_LENGTH = 75f;
        private const Single PLANE_SIZE = 20f;

        private static readonly Color XColorSel = Color.Blue;
        private static readonly Color XColor = Color.FromArgb(255, XColorSel.R / 2, XColorSel.G / 2, XColorSel.B / 2);
        private static readonly Color YColorSel = Color.Red;
        private static readonly Color YColor = Color.FromArgb(255, YColorSel.R / 2, YColorSel.G / 2, YColorSel.B / 2);
        private static readonly Color ZColorSel = Color.Green;
        private static readonly Color ZColor = Color.FromArgb(255, ZColorSel.R / 2, ZColorSel.G / 2, ZColorSel.B / 2);

        private bool m_intersect;

        private Vec3 m_axisesCenter;

        private Color m_axisXColor;
        private Color m_axisYColor;
        private Color m_axisZColor;
        private Color m_planeXYColor;
        private Color m_planeYZColor;
        private Color m_planeZXColor;

        public Axis(MesherApplication mesherApplication, IDocumentView documentView) : base(mesherApplication, documentView)
        {
            Name = @"Axises";

            m_axisesCenter = new Vec3(80, 80, 0);

            m_axisXColor = XColor;
            m_axisYColor = YColor;
            m_axisZColor = ZColor;
            m_planeXYColor = XColor;
            m_planeYZColor = YColor;
            m_planeZXColor = ZColor;
        }

        private void DocumentView_AfterDocumentViewRender(object sender, Core.Events.EventArgs.DocumentViewRenderEventArgs args)
        {
            DocumentView.RenderContext.ClearDepthBuffer();

            var camera = args.Graphics.CreateCamera();

            camera.ProjectionMatrix = Mat4.Ortho(0, DocumentView.Width, 0, DocumentView.Height, -100000, 100000);
            camera.LookAtPoint = Vec3.Zero;
            camera.Position = new Vec3(0, 0, 1);
            camera.UpVector = new Vec3(0, 1, 0);

            var axisX = DocumentView.RenderContext.RCamera.ViewMatrix.Col0.ToVec3();
            var axisY = DocumentView.RenderContext.RCamera.ViewMatrix.Col1.ToVec3();
            var axisZ = DocumentView.RenderContext.RCamera.ViewMatrix.Col2.ToVec3();

            var eAxisX = CreateAxis(args.Graphics, m_axisXColor, axisX);
            var eAxisY = CreateAxis(args.Graphics, m_axisYColor, axisY);
            var eAxisZ = CreateAxis(args.Graphics, m_axisZColor, axisZ);

            var ePlaneXY = CreatePlane(args.Graphics, m_planeXYColor, axisX, axisY);
            var ePlaneYZ = CreatePlane(args.Graphics, m_planeYZColor, axisY, axisZ);
            var ePlaneZX = CreatePlane(args.Graphics, m_planeZXColor, axisZ, axisX);

            var postRenderItem = new PostRenderItem
            {
                RenderArgs =
                {
                    RCamera = camera,
                    RLights = new RLights()
                }
            };

            postRenderItem.Primitives.Add(eAxisX);
            postRenderItem.Primitives.Add(eAxisY);
            postRenderItem.Primitives.Add(eAxisZ);
            postRenderItem.Primitives.Add(ePlaneXY);
            postRenderItem.Primitives.Add(ePlaneYZ);
            postRenderItem.Primitives.Add(ePlaneZX);

            args.PostRenderItems.Add(postRenderItem);
        }

        private void DocumentView_MouseClick(object sender, MouseEventArgs e)
        {
            var location = new Point(e.Location.X, DocumentView.Height - e.Location.Y - 1);

            if (CheckPlaneXYIntersect(location))
            {
                var len = (DocumentView.Camera.Position - DocumentView.Camera.LookAtPoint).Length();
                DocumentView.Camera.Position = DocumentView.Camera.LookAtPoint + new Vec3(0, 0, 1) * len;
                DocumentView.Camera.UpVector = new Vec3(0, 1, 0);
            }
            else if (CheckPlaneYZIntersect(location))
            {
                var len = (DocumentView.Camera.Position - DocumentView.Camera.LookAtPoint).Length();
                DocumentView.Camera.Position = DocumentView.Camera.LookAtPoint + new Vec3(1, 0, 0) * len;
                DocumentView.Camera.UpVector = new Vec3(0, 0, 1);
            }
            else if (CheckPlaneZXIntersect(location))
            {
                var len = (DocumentView.Camera.Position - DocumentView.Camera.LookAtPoint).Length();
                DocumentView.Camera.Position = DocumentView.Camera.LookAtPoint + new Vec3(0, 1, 0) * len;
                DocumentView.Camera.UpVector = new Vec3(1, 0, 0);
            }
        }

        private void DocumentView_MouseMove(object sender, MouseEventArgs args)
        {
            InitColor(XColor, YColor, ZColor, XColor, YColor, ZColor);

            var location = new Point(args.Location.X, DocumentView.Height - args.Location.Y - 1);

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

        private RTriangles CreatePlane(MesherGraphics graphics, Color color, Vec3 axis0, Vec3 axis1)
        {
            var triangles = graphics.CreateTriangles();

            triangles.HasMaterial = true;
            triangles.Material = new RMaterial();
            triangles.Material.HasColorAmbient = true;
            triangles.Material.ColorAmbient = new Color4(color);

            var p0 = m_axisesCenter;
            var p1 = p0 + axis0 * PLANE_SIZE;
            var p2 = p0 + axis1 * PLANE_SIZE;
            var p3 = p0 + axis0 * PLANE_SIZE + axis1 * PLANE_SIZE;

            triangles.Positions.Add(p0);
            triangles.Positions.Add(p1);
            triangles.Positions.Add(p2);
            triangles.Positions.Add(p3);

            triangles.IndexedRendering = true;
            triangles.Indexes.AddRange(new List<Int32>{0, 1, 2, 1, 2, 3});

            return triangles;
        }

        private REdges CreateAxis(MesherGraphics graphics, Color color, Vec3 axis)
        {
            var edges = graphics.CreateEdges();

            edges.Width = 2;
            edges.Color = color;
            edges.Positions.Add(m_axisesCenter);
            edges.Positions.Add(m_axisesCenter + axis.Normalize() * AXIS_LENGTH);

            return edges;
        }

        public override void Execute()
        {
            Enabled = true;

            DocumentView.MouseMove += DocumentView_MouseMove;
            DocumentView.MouseClick += DocumentView_MouseClick;
            DocumentView.AfterDocumentViewRender += DocumentView_AfterDocumentViewRender;
        }

        public override void Dispose()
        {
        }

        private bool CheckPlaneXYIntersect(Point p)
        {
            var axisX = new Vec3(DocumentView.Camera.ViewMatrix.Col0).Normalize();
            var axisY = new Vec3(DocumentView.Camera.ViewMatrix.Col1).Normalize();

            var p0 = m_axisesCenter;
            var p1 = p0 + axisX * PLANE_SIZE;
            var p2 = p0 + axisY * PLANE_SIZE;
            var p3 = p0 + axisX * PLANE_SIZE + axisY * PLANE_SIZE;

            var pt = new Vec3(p.X, p.Y, 0);

            return PointInTriangle(pt, p0, p1, p2) || PointInTriangle(pt, p1, p2, p3);
        }

        private bool CheckPlaneYZIntersect(Point p)
        {
            var axisY = new Vec3(DocumentView.Camera.ViewMatrix.Col1).Normalize();
            var axisZ = new Vec3(DocumentView.Camera.ViewMatrix.Col2).Normalize();

            var p0 = m_axisesCenter;
            var p1 = p0 + axisZ * PLANE_SIZE;
            var p2 = p0 + axisY * PLANE_SIZE;
            var p3 = p0 + axisZ * PLANE_SIZE + axisY * PLANE_SIZE;

            var pt = new Vec3(p.X, p.Y, 0);

            return PointInTriangle(pt, p0, p1, p2) || PointInTriangle(pt, p1, p2, p3);
        }

        private bool CheckPlaneZXIntersect(Point p)
        {
            var axisX = new Vec3(DocumentView.Camera.ViewMatrix.Col0).Normalize();
            var axisZ = new Vec3(DocumentView.Camera.ViewMatrix.Col2).Normalize();

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
