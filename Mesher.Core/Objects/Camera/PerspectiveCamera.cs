using System;
using Mesher.Mathematics;

namespace Mesher.Core.Objects.Camera
{
    public class PerspectiveCamera : Camera
    {
        private const Single ZNear = 0.01f;
        private const Single ZFar = 1000000;

        public PerspectiveCamera(Single angle, Single aspect, Vec3 position, Vec3 upVector, Vec3 lookAtPoint) 
            : base(Mat4.Perspective(angle, aspect, ZNear, ZFar), position, upVector, lookAtPoint)
        {
            
        }

        public override void Zoom(Single zoom)
        {
            Position = LookAtPoint + (Position - LookAtPoint) / zoom;
        }
    }
}
