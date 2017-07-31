using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Camera
{
    public class OrthographicCamera : Camera
    {
        private const double ZNear = -1000000;
        private const double ZFar = 1000000;

        private double m_left;
        private double m_right;
        private double m_top;
        private double m_bottom;

        public OrthographicCamera(double width, double height, Vec3 position, Vec3 upVector, Vec3 lookAtPoint) 
            : base(Mat4.Ortho(-width/2, width/2, height/2, -height/2, ZNear, ZFar), position, upVector, lookAtPoint)
        {
            m_left = -width / 2;
            m_right = width / 2;
            m_top = height / 2;
            m_bottom = -height / 2;
        }

        public override void Zoom(double zoom)
        {
            var newWidth = (m_right - m_left) * zoom;
            var newHeight = (m_top - m_bottom) * zoom;

            m_left = -newWidth / 2;
            m_right = newWidth / 2;
            m_top = newHeight / 2;
            m_bottom = -newHeight / 2;

            ProjectionMatrix = Mat4.Ortho(m_left, m_right, m_bottom, m_top, ZNear, ZFar);
        }
    }
}
