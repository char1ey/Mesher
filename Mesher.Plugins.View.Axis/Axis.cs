using Mesher.Core;
using Mesher.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Mesher.Graphics;
using Mesher.Graphics.Collections;
using Mesher.Graphics.Primitives;
using Mesher.Mathematics;

namespace Mesher.Plugins.View.Axis
{
    public class Axis : Plugin
    {
        private const Single AXIS_LENGTH = 75f;
        private const Single PLANE_SIZE = 20f;

        private Vec3 m_axisesCenter;

        public Axis(MesherApplication mesherApplication) : base(mesherApplication)
        {
            Name = @"Axises";

            m_axisesCenter = new Vec3(80, 80, 0);
        }

        private void MesherApplication_AfterDocumentChange(object sender, Core.Events.EventArgs.ChangeDocumentEventArgs args)
        {
            if (MesherApplication.CurrentDocument != null)
                MesherApplication.CurrentDocument.AfterRender += Document_AfterRender;
        }

        private void MesherApplication_BeforeDocumentChange(object sender, Core.Events.EventArgs.ChangeDocumentEventArgs args)
        {
            if (MesherApplication.CurrentDocument != null)
                MesherApplication.CurrentDocument.AfterRender -= Document_AfterRender;
        }

        private void Document_AfterRender(object sender, Core.Events.EventArgs.RenderEventArgs args)
        {
            args.DocumentView.RenderContext.ClearDepthBuffer();

            var camera = args.Graphics.CreateCamera();

            camera.ProjectionMatrix = Mat4.Ortho(0, args.DocumentView.Width, 0, args.DocumentView.Height, -100000, 100000);
            camera.LookAtPoint = Vec3.Zero;
            camera.Position = new Vec3(0, 0, 1);
            camera.UpVector = new Vec3(0, 1, 0);

            var eAxisX = CreateAxis(args.Graphics, Color.Red, args.DocumentView.RenderContext.RCamera.ViewMatrix.Col0.ToVec3());
            var eAxisY = CreateAxis(args.Graphics, Color.Green, args.DocumentView.RenderContext.RCamera.ViewMatrix.Col1.ToVec3());
            var eAxisZ = CreateAxis(args.Graphics, Color.Blue, args.DocumentView.RenderContext.RCamera.ViewMatrix.Col2.ToVec3());

            var renderArgs = new RenderArgs
            {
                RCamera = camera,
                RLights = new RLights()
            };


            eAxisX.Render(args.Graphics.RenderersFactory, renderArgs);
            eAxisY.Render(args.Graphics.RenderersFactory, renderArgs);
            eAxisZ.Render(args.Graphics.RenderersFactory, renderArgs);

            eAxisX.Dispose();
            eAxisY.Dispose();
            eAxisZ.Dispose();
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

            MesherApplication.BeforeDocumentChange += MesherApplication_BeforeDocumentChange;
            MesherApplication.AfterDocumentChange += MesherApplication_AfterDocumentChange;

            if (MesherApplication.CurrentDocument != null)
                MesherApplication.CurrentDocument.AfterRender += Document_AfterRender;
        }
    }
}
