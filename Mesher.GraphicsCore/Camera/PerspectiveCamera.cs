using System;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Camera
{
    public class PerspectiveCamera : Camera
    {
        #region constants

        private const double DefaultNear = 0;
        private const double DefaultFar = 100000;
        private const double DefaultAngle = 45;

        private const double ZoomSpeed = 0.01;

        #endregion

        #region variables

        private double m_angle;

        #endregion

        #region contructors

        private void Init(double angle)
        {
            m_angle = angle;
            ResizeRenderContext(null, null);
        }

        public PerspectiveCamera(double angle, Vertex eye, Vertex center, Vertex up, double mouseRotationSpeed, double mouseMovementSpeed, RenderContext renderContext)
            :base(eye, center, up, mouseRotationSpeed, mouseMovementSpeed, renderContext)
        {
            Init(angle);
        }

        public PerspectiveCamera(double angle, Vertex eye, Vertex center, Vertex up, RenderContext renderContext)
            :base(eye, center, up, renderContext)
        {
            Init(angle);
        }

        public PerspectiveCamera(double angle, double mouseRotationSpeed, double mouseMovementSpeed, RenderContext renderContext)
            :base(mouseRotationSpeed, mouseMovementSpeed, renderContext)
        {
            Init(angle);
        }

        public PerspectiveCamera(double angle, RenderContext renderContext)
            :base(renderContext)
        {
            Init(angle);
        }

        public PerspectiveCamera(RenderContext renderContext)
            :base(renderContext)
        {
            Init(DefaultAngle);
        }

        #endregion

        #region override methods
        
        protected override void ResizeRenderContext(object sender, EventArgs e)
        {
            m_Projection = Matrix.Perspective(m_angle, RenderContext.Width / RenderContext.Height, DefaultNear, DefaultFar);
            ApplyCamera();
        }

        public override void Zoom(double delta, Vertex position)
        {
            Move((Center - Eye).Normalize() * delta * ZoomSpeed);
            ApplyCamera();
        }

        #endregion
    }
}
