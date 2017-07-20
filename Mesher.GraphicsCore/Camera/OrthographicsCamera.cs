using System;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Camera
{
    public class OrthographicsCamera : Camera
    {
        #region constants

        private const double DefaultNear = -100000;
        private const double DefaultFar = 100000;
        private const double DefaultRatio = 1;

        private const double ZoomSpeed = 0.002;

        #endregion

        #region variables

        private double m_ratio;

        private double m_left;
        private double m_right;
        private double m_top;
        private double m_bottom;

        #endregion

        #region contructors

        private void Init(double ratio)
        {
            m_ratio = ratio;
            ResizeRenderContext(null, null);
        }

        public OrthographicsCamera(double ratio, Vertex eye, Vertex center, Vertex up, double mouseRotationSpeed, double mouseMovementSpeed, RenderContext renderContext)
            :base(eye, center, up, mouseRotationSpeed, mouseMovementSpeed, renderContext)
        {
            Init(ratio);
        }

        public OrthographicsCamera(double ratio, Vertex eye, Vertex center, Vertex up, RenderContext renderContext)
            :base(eye, center, up, renderContext)
        {
            Init(ratio);
        }

        public OrthographicsCamera(double ratio, double mouseRotationSpeed, double mouseMovementSpeed, RenderContext renderContext)
            :base(mouseRotationSpeed, mouseMovementSpeed, renderContext)
        {
            Init(ratio);
        }
        
        public OrthographicsCamera(double ratio, RenderContext renderContext)
            :base(renderContext)
        {
            Init(ratio);
        }

        public OrthographicsCamera(RenderContext renderContext)
            :base(renderContext)
        {
            Init(DefaultRatio);
        }

        #endregion

        #region override methods

        protected override void ResizeRenderContext(object sender, EventArgs e)
        {
            m_left = -RenderContext.Width / 2.0 * m_ratio;
            m_right = RenderContext.Width / 2.0 * m_ratio;
            m_top = -RenderContext.Height / 2.0 * m_ratio;
            m_bottom = RenderContext.Height / 2.0 * m_ratio;

            m_Projection = Matrix.Ortho(m_left, m_right, m_top, m_bottom, DefaultNear, DefaultFar);

            ApplyCamera();
        }

        public override void Zoom(double delta, Vertex position)
        {
            var dw = (m_right - m_left) / 2 * delta * ZoomSpeed;
            var dh = (m_bottom - m_top) / 2 * delta * ZoomSpeed;

            m_left += dw * (position.X / RenderContext.Width);
            m_right -= dw * (1 - position.X / RenderContext.Width);
            m_top += dh * (position.Y / RenderContext.Height);
            m_bottom -= dh * (1 - position.Y / RenderContext.Height);

            m_Projection = Matrix.Ortho(m_left, m_right, m_top, m_bottom, DefaultNear, DefaultFar);

            ApplyCamera();
        }

        #endregion
    }
}
