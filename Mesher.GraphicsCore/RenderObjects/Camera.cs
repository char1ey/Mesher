using System;
using Mesher.Components;
using Mesher.Mathematics;
using System.Windows.Forms;
using Mesher.OpenGLCore;

namespace Mesher.GraphicsCore
{
    public enum CameraType
    {
        Orthographic = 1,
        Perspective
    }

    public class Camera
    {
        private const double DefaultMouseRotationSpeed = 0.4;
        private const double DefaultMouseMovementSpeed = 1.0;

        private const double DefaultOrthographicNear = -100000;
        private const double DefaultOrthographicFar = 100000;

        private const double DefaultPerspectiveNear = 1;
        private const double DefaultPerspectiveFar = 100000;

        private Matrix m_Projection;

        private Vertex m_Center;
        private Vertex m_Eye;
        private Vertex m_Up;

        private Vertex m_PreviousCenter;
        private Vertex m_PreviousEye;
        private Vertex m_PreviousUp;

        private CameraType m_CameraType;

        private RenderContext m_RenderContext;

        private double m_MouseRotationSpeed;
        private double m_MouseMovementSpeed;

        private double m_Ratio;

        public Matrix ProjectionMatrix
        {
            get { return m_Projection; }
        }

        public Vertex Center
        {
            get { return m_Center; }
        }

        public Vertex Eye
        {
            get { return m_Eye; }
        }

        public Vertex Up
        {
            get { return m_Up; }
        }

        public RenderContext RenderContext { get => m_RenderContext; }

        public Camera(double ratio, Vertex eye, Vertex center, Vertex up, 
            double mouseRotationSpeed, double mouseMovementSpeed, RenderContext renderContext)
        {
            m_CameraType = CameraType.Orthographic;

            m_MouseMovementSpeed = mouseMovementSpeed;
            m_MouseRotationSpeed = mouseRotationSpeed;
            m_RenderContext = renderContext;

            m_Center = center;
            m_Eye = eye;
            m_Up = up;

            m_RenderContext.MouseMove += MouseMouveCameraControl;
            m_RenderContext.KeyDown += KeyDownCameraControl; 
            m_RenderContext.KeyUp += KeyUpCameraControl;
            m_RenderContext.ContextResize += UpdateCameraProjection;

            m_Ratio = ratio;

            m_Projection = Matrix.Ortho(-m_RenderContext.Width / 2.0 * m_Ratio,
                                         m_RenderContext.Width / 2.0 * m_Ratio,
                                        -m_RenderContext.Height / 2.0 * m_Ratio,
                                         m_RenderContext.Height / 2.0 * m_Ratio,
                                         DefaultOrthographicNear,
                                         DefaultOrthographicFar);
        }

        public Camera(double ratio, Vertex eye, Vertex center, Vertex up, RenderContext renderContext)
            :this(ratio, eye, center, up, DefaultMouseRotationSpeed, DefaultMouseMovementSpeed, renderContext) { }

        public Camera(double ratio, double mouseRotationSpeed, double mouseMovementSpeed, RenderContext renderContext)
            :this(ratio, new Vertex(0, 0, 1), new Vertex(0, 0, 0), new Vertex(0, 1, 0), mouseRotationSpeed, mouseMovementSpeed, renderContext) { }

        public Camera(double ratio, RenderContext renderContext)
            :this(ratio, DefaultMouseRotationSpeed, DefaultMouseMovementSpeed, renderContext) { }

        public Camera(CameraType cameraType, double mouseRotationSpeed, double mouseMovementSpeed)
        {

        }

        public Camera()
        {
            m_Projection = Matrix.Ortho(-10, 10, -10, 10, -100000, 100000);
            m_Center = new Vertex(0, 0, 0);
            m_Eye = new Vertex(0, 0, 1);
            m_Up = new Vertex(0, 1, 0);
            UpdatePrevious();
            m_CameraType = CameraType.Orthographic;
        }

        private Vertex m_PreviousMousePosition;

        private bool m_IsShiftPressed;

        private MouseButtons m_PreviousMouseButton;

        private void KeyUpCameraControl(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.LeftShift) m_IsShiftPressed = false;
        }

        private void KeyDownCameraControl(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.LeftShift) m_IsShiftPressed = true;
        }

        private void MouseMouveCameraControl(object sender, MouseEventArgs e)
        { 
            if (e.Button == MouseButtons.None || m_PreviousMouseButton != e.Button)
            {
                m_PreviousMousePosition = new Vertex(e.X, e.Y);
                UpdatePrevious();
            }
            else if (e.Button == MouseButtons.Left)
            {
                Move(gl.UnProject(m_PreviousMousePosition.x, (sender as Control).Height - m_PreviousMousePosition.y, 0)
                   - gl.UnProject(e.X, (sender as Control).Height - e.Y, 0));
            }
            else if (e.Button == MouseButtons.Right)
            {
                var va = GetArcballVector(m_PreviousMousePosition.x, m_PreviousMousePosition.y,
                                        (sender as Control).Width, (sender as Control).Height);

                var vb = GetArcballVector(e.X, e.Y, (sender as Control).Width, (sender as Control).Height);

                if (m_IsShiftPressed)
                {
                    Rotate(va.Angle(vb));
                }
                else
                {
                    var m = Matrix.LookAt(m_PreviousEye - m_PreviousCenter, new Vertex(0, 0, 0), m_PreviousUp).Inverse();
                    Rotate((m * va).Cross(m * vb).Normalize(), va.Angle(vb) * 0.4);
                }
            }
            m_PreviousMouseButton = e.Button;
        }

        private void UpdateCameraProjection(object sender, EventArgs e)
        {
            if (m_CameraType == CameraType.Orthographic)
            {
                m_Projection = Matrix.Ortho(-m_RenderContext.Width / 2.0 * m_Ratio,
                                             m_RenderContext.Width / 2.0 * m_Ratio,
                                            -m_RenderContext.Height / 2.0 * m_Ratio,
                                             m_RenderContext.Height / 2.0 * m_Ratio,
                                             DefaultOrthographicNear,
                                             DefaultOrthographicFar);
            }
            else if(m_CameraType == CameraType.Perspective)
            {

            }

            ApplyCamera();
        }

        public void Move(Vertex v)
        {
            m_Center = m_PreviousCenter + v;
            m_Eye = m_PreviousEye + v;
        }

        public void Rotate(double angle)
        {
            m_Up = Matrix.Rotate(angle, m_Eye - m_Center) * m_PreviousUp;
        }

        public void Rotate(Vertex v, double angle)
        {
            m_Eye = (Matrix.Rotate(angle, v) * (m_PreviousEye - m_PreviousCenter)) + m_PreviousCenter;
            m_Up = (m_Eye - m_Center).Cross(m_Up).Cross(m_Eye - m_Center);
        }

        public void ApplyCamera()
        {
            m_RenderContext.BeginRender();

            gl.MatrixMode(gl.GL_PROJECTION);
            gl.LoadMatrix(m_Projection.ToArray());
            gl.MatrixMode(gl.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.LookAt(m_Eye.x, m_Eye.y, m_Eye.z, m_Center.x, m_Center.y, m_Center.z, m_Up.x, m_Up.y, m_Up.z);

            m_RenderContext.EndRender();
        }

        private Vertex GetArcballVector(double x, double y, double width, double height)
        {
            var ret = new Vertex(x / width * 2 - 1.0, -(y / height * 2 - 1.0));
            double square = ret.x * ret.x + ret.y * ret.y;
            if (square <= 1)
                ret.z = Math.Sqrt(1 - square);
            ret = ret.Normalize();
            return ret;
        }

        private void UpdatePrevious()
        {
            m_PreviousCenter = new Vertex(m_Center);
            m_PreviousEye = new Vertex(m_Eye);
            m_PreviousUp = new Vertex(m_Up);
        }
    }
}