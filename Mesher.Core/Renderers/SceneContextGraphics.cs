using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Graphics;
using Mesher.Graphics.ShaderProgram;
using Mesher.Mathematics;

namespace Mesher.Core.Renderers
{
    public class SceneContextGraphics
    {
        private IDocumentView m_documentView;
        private GlShaderProgram m_shaderProgram;

	    private MesherGraphics m_graphics;

        public SceneContextGraphics(MesherGraphics graphics, IDocumentView documentView)
        {
            m_documentView = documentView;
	        m_graphics = graphics;
        }

        public void DrawLine(Vec3 p0, Vec3 p1, Single lineWidth, Color color)
        {
            m_shaderProgram.Bind();

            InitView();
            m_shaderProgram.SetBuffer("position", new []{p0.X, p0.Y, p0.Z, p1.X, p1.Y, p1.Z}, 3);
            m_shaderProgram.SetValue("color", new Color4(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f));
            m_shaderProgram.SetValue("hasNormal", 0);
            m_shaderProgram.RenderLines(lineWidth, false);

            m_shaderProgram.Unbind();
        }

        public void DrawTriangle(Vec3 p0, Vec3 p1, Vec3 p2, Color color)
        {
            m_shaderProgram.Bind();

            InitView();

            var n = (p1 - p0).Cross(p2 - p0).Normalize();

            m_shaderProgram.SetBuffer("position", new[] { p0.X, p0.Y, p0.Z, p1.X, p1.Y, p1.Z, p2.X, p2.Y, p2.Z }, 3);
            m_shaderProgram.SetBuffer("normal", new[] { n.X, n.Y, n.Z, n.X, n.Y, n.Z, n.X, n.Y, n.Z }, 3);
            m_shaderProgram.SetValue("hasNormal", 1);
            m_shaderProgram.SetValue("color", new Color4(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f));
            m_shaderProgram.RenderTriangles(false);

            m_shaderProgram.Unbind();
        }

        public void DrawQuad(Vec3 p0, Vec3 p1, Vec3 p2, Vec3 p3, Color color)
        {
            DrawTriangle(p0, p1, p2, color);
            DrawTriangle(p1, p2, p3, color);
        }

        private void InitView()
        {
            m_shaderProgram.SetValue("viewPort", new Vec4(0, 0, m_documentView.Width, m_documentView.Height));
            m_shaderProgram.SetValue("clipDistance", (Single)Math.Max(m_documentView.Width, m_documentView.Height));
        }
    }
}
