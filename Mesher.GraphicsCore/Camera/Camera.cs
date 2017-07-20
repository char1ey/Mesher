using System;
using System.Windows.Forms;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore.Camera
{
    public abstract class Camera: IDisposable
    {
        #region constants

        private const double DefaultMouseRotationSpeed = 0.5;
        private const double DefaultMouseMovementSpeed = 1.0;

        #endregion

        #region variables

        protected Matrix m_Projection;

        protected Vertex m_Center;
        protected Vertex m_Eye;
        protected Vertex m_Up;

        private Vertex m_PreviousCenter;
        private Vertex m_PreviousEye;
        private Vertex m_PreviousUp;

        private RenderContext m_RenderContext;

        private double m_MouseRotationSpeed;
        private double m_MouseMovementSpeed;

        private bool m_IsShiftPressed;

        private Vertex m_PreviousMousePosition;

        private MouseButtons m_PreviousMouseButton;

        #endregion

        #region properties

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

        public RenderContext RenderContext
        {
            get { return m_RenderContext; }
        }

        #endregion

        #region constructors

        protected Camera(Vertex eye, Vertex center, Vertex up, 
            double mouseRotationSpeed, double mouseMovementSpeed, RenderContext renderContext)
        {
            m_MouseMovementSpeed = mouseMovementSpeed;
            m_MouseRotationSpeed = mouseRotationSpeed;
            m_RenderContext = renderContext;

            m_Center = center;
            m_Eye = eye;
            m_Up = up;

            m_RenderContext.MouseWheel += MouseWheelCameraControl;
            m_RenderContext.ContextResize += ResizeRenderContext; 
            m_RenderContext.MouseMove += MouseMouveCameraControl;
            m_RenderContext.KeyDown += KeyDownCameraControl; 
            m_RenderContext.KeyUp += KeyUpCameraControl;
        }

        protected Camera(Vertex eye, Vertex center, Vertex up, RenderContext renderContext)
            : this(eye, center, up, DefaultMouseRotationSpeed, DefaultMouseMovementSpeed, renderContext) { }

        protected Camera(double mouseRotationSpeed, double mouseMovementSpeed, RenderContext renderContext)
            : this(new Vertex(0, 0, 1), new Vertex(0, 0, 0), new Vertex(0, 1, 0), mouseRotationSpeed, mouseMovementSpeed, renderContext) { }

        protected Camera(RenderContext renderContext)
            : this(DefaultMouseRotationSpeed, DefaultMouseMovementSpeed, renderContext) { }

        #endregion

        #region camera mouse control
   
        private void MouseWheelCameraControl(object sender, MouseEventArgs e)
        {
            Zoom(e.Delta, new Vertex(e.X, m_RenderContext.Height - e.Y - 1));
        }

        private void KeyUpCameraControl(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.LeftShift)
            {
                m_IsShiftPressed = false;
                m_PreviousMouseButton = MouseButtons.None;
            }
        }

        private void KeyDownCameraControl(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.LeftShift)
            {
                m_IsShiftPressed = true;
                m_PreviousMouseButton = MouseButtons.None;
            }
        }

        private void MouseMouveCameraControl(object sender, MouseEventArgs e)
        { 
            if (e.Button == MouseButtons.None || m_PreviousMouseButton != e.Button)
            {
                m_PreviousMousePosition = new Vertex(e.X, e.Y);
                m_PreviousCenter = m_Center;
                m_PreviousEye = m_Eye;
                m_PreviousUp = m_Up;
            }
            else if (e.Button == MouseButtons.Left)
            {
                Move((Gl.UnProject(m_PreviousMousePosition.X, (sender as Control).Height - m_PreviousMousePosition.Y, 0)
                   - Gl.UnProject(e.X, (sender as Control).Height - e.Y, 0)) * m_MouseMovementSpeed, true);
            }
            else if (e.Button == MouseButtons.Right)
            {
                var va = GetArcballVector(m_PreviousMousePosition.X, m_PreviousMousePosition.Y,
                                        (sender as Control).Width, (sender as Control).Height);

                var vb = GetArcballVector(e.X, e.Y, (sender as Control).Width, (sender as Control).Height);

                if (m_IsShiftPressed)
                {
                    double a1 = Math.Atan2(va.X, va.Y);
                    double a2 = Math.Atan2(vb.X, vb.Y);

                    double dir;
                    if (a1 < a2)
                        dir = 1;
                    else dir = -1;

                    Rotate(va.Angle(vb) * dir);
                }
                else
                {
                    var m = Matrix.LookAt(m_PreviousEye - m_PreviousCenter, new Vertex(0, 0, 0), m_PreviousUp).Inverse();
                    Rotate((m * va).Cross(m * vb).Normalize(), va.Angle(vb) * m_MouseRotationSpeed);
                }
            }
            m_PreviousMouseButton = e.Button;
            ApplyCamera();
        }

        #endregion

        #region abstract methods

        protected abstract void ResizeRenderContext(object sender, EventArgs e);

        public abstract void Zoom(double delta, Vertex position);

        #endregion

        #region camera transformations

        public void Move(Vertex v, bool movePrevious = false)
        {
            if (!v.Valid())
                return;

            if (movePrevious)
            {
                m_Center = m_PreviousCenter + v;
                m_Eye = m_PreviousEye + v;
            }
            else
            {
                m_Center += v;
                m_Eye += v;
            }
        }

        public void Rotate(double angle)
        {
            m_Up = Matrix.Rotate(angle, m_Eye - m_Center) * m_PreviousUp;
        }

        public void Rotate(Vertex v, double angle)
        {
            if (!v.Valid())
                return;

            m_Eye = (Matrix.Rotate(angle, v) * (m_PreviousEye - m_PreviousCenter)) + m_PreviousCenter;
            m_Up = (m_Eye - m_Center).Cross(m_Up).Cross(m_Eye - m_Center);
        }

        #endregion

        #region state functions

        public void ApplyCamera()
        {
            m_RenderContext.BeginRender();
            Gl.MatrixMode(Gl.GL_PROJECTION);
            Gl.LoadMatrix(m_Projection.ToArrayDouble());
            Gl.MatrixMode(Gl.GL_MODELVIEW);
            Gl.LoadIdentity();
            Gl.LookAt(m_Eye, m_Center, m_Up);
            m_RenderContext.EndRender();
        }

        #endregion

        #region secondary functions

        private Vertex GetArcballVector(double x, double y, double width, double height)
        {
            var ret = new Vertex(x / width * 2 - 1.0, -(y / height * 2 - 1.0));
            double square = ret.X * ret.X + ret.Y * ret.Y;
            if (square <= 1)
                ret.Z = Math.Sqrt(1 - square);
            ret = ret.Normalize();
            return ret;
        }

        #endregion

        #region interface methods

        public void Dispose()
        {
            m_RenderContext.MouseWheel -= MouseWheelCameraControl;
            m_RenderContext.ContextResize -= ResizeRenderContext;
            m_RenderContext.MouseMove -= MouseMouveCameraControl;
            m_RenderContext.KeyDown -= KeyDownCameraControl;
            m_RenderContext.KeyUp -= KeyUpCameraControl;            
        }

        #endregion
    }
}