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


        public OrthographicCamera(double width, double height, Vec3 position, Vec3 upVector, Vec3 lookAtPoint) 
            : base(Mat4.Ortho(-width/2, width/2, height/2, -height/2, ZNear, ZFar), position, upVector, lookAtPoint)
        {
        }

        public override void Zoom(double zoom)
        {
            ProjectionMatrix *= Mat4.Scale(zoom);
        }
    }
}
