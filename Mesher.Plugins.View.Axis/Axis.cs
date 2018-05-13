using Mesher.Core;
using Mesher.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mesher.Mathematics;

namespace Mesher.Plugins.View.Axis
{
    public class Axis : Plugin
    {
        public Axis(Document document) : base(document)
        {
            Document.AfterRender += Document_AfterRender;
        }

        private void Document_AfterRender(object sender, Core.Events.EventArgs.RenderEventArgs args)
        {
            args.RenderContext.ClearDepthBuffer();

            var triangles = args.Graphics.CreateTriangles();
            var edges = args.Graphics.CreateEdges();

            var camera = args.Graphics.CreateCamera();

            camera.ProjectionMatrix = Mat4.Ortho(0, args.RenderContext.Width, 0, args.RenderContext.Height);
            camera.LookAtPoint = Vec3.Zero;
            camera.Position = new Vec3(0, 0, 1);
            camera.UpVector = new Vec3(0, 1, 0);
        }

        public override void Execute()
        {
            Enabled = true;
        }
    }
}
