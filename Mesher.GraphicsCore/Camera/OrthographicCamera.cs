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
        public OrthographicCamera(Mat4 projectionMatrix, Vec3 position, Vec3 upVector, Vec3 lookAtPoint) 
            : base(projectionMatrix, position, upVector, lookAtPoint)
        {
        }
    }
}
