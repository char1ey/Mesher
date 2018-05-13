using System;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Camera
{
    public class PerspectiveRCamera : RCamera
    {
        private const Single ZNear = 0.01f;
        private const Single ZFar = 1000000;

        public PerspectiveRCamera(Single angle, Single aspect, Vec3 position, Vec3 upVector, Vec3 lookAtPoint) 
            : base(Mat4.Perspective(angle, aspect, ZNear, ZFar), position, upVector, lookAtPoint)
        {
            
        }

        public void Zoom(Single zoom)
        {
            Position = LookAtPoint + (Position - LookAtPoint) / zoom;
        }
    }
}
