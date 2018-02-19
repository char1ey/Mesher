using System;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Camera
{
    public class PerspectiveCamera : Camera
    {
        private const Double ZNear = 0.01;
        private const Double ZFar = 1000000;

        public PerspectiveCamera(Double angle, Double aspect, Vec3 position, Vec3 upVector, Vec3 lookAtPoint) 
            : base(Mat4.Perspective(angle, aspect, ZNear, ZFar), position, upVector, lookAtPoint)
        {
            
        }

        public override void Zoom(Double zoom)
        {
            Position = LookAtPoint + (Position - LookAtPoint) / zoom;
        }
    }
}
