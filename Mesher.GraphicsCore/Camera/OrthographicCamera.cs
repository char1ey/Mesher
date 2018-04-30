using System;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Camera
{
    public class OrthographicCamera : Camera
    {
        private const Single ZNear = -1000000;
        private const Single ZFar = 1000000;

        public OrthographicCamera() { }

        public OrthographicCamera(Single width, Single height, Vec3 position, Vec3 upVector, Vec3 lookAtPoint) 
            : base(Mat4.Ortho(-width/2, width/2, -height/2, height/2, ZNear, ZFar), position, upVector, lookAtPoint)
        {
        }

        public void UpdateSize(Single width, Single height)
        {
            ProjectionMatrix = Mat4.Ortho(-width / 2, width / 2, -height / 2, height / 2, ZNear, ZFar);
        }

        public override void Zoom(Single zoom)
        {
            ProjectionMatrix *= Mat4.Scale(zoom);
        }
    }
}
