using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Camera
{
    public class Camera
    {
        private const double Eps = 1e-9;

        public Mat4 ProjectionMatrix { get; set; }

        public Vec3 Position { get; set; }
        public Vec3 UpVector { get; set; }
        public Vec3 LookAtPoint { get; set; }

        public Camera(Mat4 projectionMatrix, Vec3 position, Vec3 upVector, Vec3 lookAtPoint)
        {
            ProjectionMatrix = projectionMatrix;
            Position = position;
            UpVector = upVector;
            LookAtPoint = lookAtPoint;
        }

        public void Apply()
        {
            Gl.MatrixMode(Gl.GL_PROJECTION);
            Gl.LoadMatrix(ProjectionMatrix.ToArray());
            Gl.MatrixMode(Gl.GL_MODELVIEW);            
            Gl.LoadMatrix(Mat4.LookAt(Position, LookAtPoint, UpVector).ToArray());
        }

        public void Move(Vec3 v)
        {
            if (!v.Valid())
                return;

            Position += v;
            LookAtPoint += v;
        }

        public void Rotate(Vec3 axis, double angle)
        {
            if (!axis.Valid() || (axis - Vec3.Zero).Length() < Eps)
                return;
            Position = new Vec3(Mat4.Rotate(angle, axis) * new Vec4(Position - LookAtPoint, 1)) + LookAtPoint;
            UpVector = new Vec3(Mat4.Rotate(angle, axis) * new Vec4(UpVector, 1));
            UpVector = (Position - LookAtPoint).Cross(UpVector).Cross(Position - LookAtPoint).Normalize();
        }

        public void Spin(double angle)
        {
            UpVector = new Vec3(Mat4.Rotate(angle, Position - LookAtPoint) * new Vec4(UpVector, 1));
        }

       // public abstract void Zoom(double zoom);
    }
}
