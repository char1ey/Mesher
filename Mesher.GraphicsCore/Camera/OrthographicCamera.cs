using System;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Camera
{
    public class OrthographicCamera : Camera
    {
        private const Double ZNear = -1000000;
        private const Double ZFar = 1000000;

        public OrthographicCamera() { }

        public OrthographicCamera(Double width, Double height, Vec3 position, Vec3 upVector, Vec3 lookAtPoint) 
            : base(Mat4.Ortho(-width/2, width/2, -height/2, height/2, ZNear, ZFar), position, upVector, lookAtPoint)
        {
        }

        public void UpdateSize(Single width, Single height)
        {
            ProjectionMatrix = Mat4.Ortho(-width / 2, width / 2, -height / 2, height / 2, ZNear, ZFar);
        }

        public override void Zoom(Double zoom)
        {
            ProjectionMatrix *= Mat4.Scale(zoom);
        }
    }
}
