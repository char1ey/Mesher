using System;
using System.Collections.Generic;
using Mesher.Mathematics;

namespace Mesher.Graphics.Camera
{
    public class RCamera : IDisposable
    {
	    private const Double EPS = 1e-9;

        private Stack<Vec3> m_positionsStack;
        private Stack<Vec3> m_upVectorsStack;
        private Stack<Vec3> m_lookAtPointsStack;

        public Int32 Id { get; set; }

        public Mat4 ProjectionMatrix { get; set; }

	    public Mat4 ViewMatrix
	    {
		    get { return Mat4.LookAt(Position, LookAtPoint, UpVector); }
	    }

	    public Vec3 Position { get; set; }
        public Vec3 UpVector { get; set; }
        public Vec3 LookAtPoint { get; set; }

        internal RCamera()
        {
            m_positionsStack = new Stack<Vec3>();
            m_upVectorsStack = new Stack<Vec3>();
            m_lookAtPointsStack = new Stack<Vec3>();
        }

        internal RCamera(Mat4 projectionMatrix, Vec3 position, Vec3 upVector, Vec3 lookAtPoint) : this()
        {
            ProjectionMatrix = projectionMatrix;
            Position = position;
            UpVector = upVector;
            LookAtPoint = lookAtPoint;
        }

        public void ClearStack()
        {
            m_positionsStack.Clear();
            m_upVectorsStack.Clear();
            m_lookAtPointsStack.Clear();
        }

        public void Push()
        {
            m_positionsStack.Push(Position.Clone());
            m_upVectorsStack.Push(UpVector.Clone());
            m_lookAtPointsStack.Push(LookAtPoint.Clone());
        }

        public void Pop()
        {
            if (m_positionsStack.Count == 0)
                return;
            Position = m_positionsStack.Pop();
            UpVector = m_upVectorsStack.Pop();
            LookAtPoint = m_lookAtPointsStack.Pop();
        }

        public void Move(Vec3 v)
        {
            if (!v.Valid())
                return;

            Position += v;
            LookAtPoint += v;
        }

        public void Rotate(Vec3 axis, Single angle)
        {
            if (!axis.Valid() || (axis - Vec3.Zero).Length() < EPS)
                return;
            Position = new Vec3(Mat4.Rotate(angle, axis) * new Vec4(Position - LookAtPoint, 1)) + LookAtPoint;
            UpVector = new Vec3(Mat4.Rotate(angle, axis) * new Vec4(UpVector, 1));
            UpVector = (Position - LookAtPoint).Cross(UpVector).Cross(Position - LookAtPoint).Normalize();
        }

        public void Spin(Single angle)
        {
            UpVector = new Vec3(Mat4.Rotate(angle, Position - LookAtPoint) * new Vec4(UpVector, 1));
        }

        public Vec3 UnProject(Vec3 v, Single windowWidth, Single windowHeight)
        {
            return Mat4.UnProject(v, ViewMatrix, ProjectionMatrix, new Vec4(0, 0, windowWidth, windowHeight));
        }

        public Vec3 UnProject(Single x, Single y, Single z, Single windowWidth, Single windowHeight)
        {
            return UnProject(new Vec3(x, y, z), windowWidth, windowHeight);
        }

        public void Dispose()
        {
        }
    }
}
