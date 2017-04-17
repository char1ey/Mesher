using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore
{
    public enum CameraType
    {
        Orthographic = 1,
        Perspective
    }

    public class Camera
    {
        private Matrix m_Projection;

        private Vertex m_Center;
        private Vertex m_Eye;
        private Vertex m_Up;

        private Vertex m_PreviousCenter;
        private Vertex m_PreviousEye;
        private Vertex m_PreviousUp;

        private CameraType m_CameraType;


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

        public Camera()
        {
            m_Projection = Matrix.Ortho(-10, 10, -10, 10, -100000, 100000);
            m_PreviousCenter = m_Center = new Vertex(0, 0, 0);
            m_PreviousEye = m_Eye = new Vertex(0, 0, 1);
            m_PreviousUp = m_Up = new Vertex(0, 1, 0);
            m_CameraType = CameraType.Orthographic;
        }

        public Camera(CameraType cameraType)
        {
            if(cameraType == CameraType.Orthographic)
            {
                m_Projection = Matrix.Ortho(-10, 10, -10, 10, -10000, 10000);

            }
            else if (cameraType == CameraType.Perspective)
            {
                m_Projection = Matrix.Perspective(60, 1, 0, 1);
                

            }
            m_CameraType = cameraType;
        }

        public void Move(Vertex v, bool replaceLastChange = false)
        {
            if (replaceLastChange)
            {
                m_Center = m_PreviousCenter + v;
                m_Eye = m_PreviousEye + v;
            }
            else
            {
                m_PreviousCenter = new Vertex(m_Center);
                m_PreviousEye = new Vertex(m_Eye);
                m_Center += v;
                m_Eye += v;
            }
        }

        public void Rotate(Vertex v, double angle, bool replaceLastChange = false)
        {
           if(replaceLastChange)
           {
                m_Eye = Matrix.Rotate(angle, v) * (m_PreviousEye - m_PreviousCenter) + m_PreviousCenter;
                m_Up = (m_Eye - m_Center).Cross((m_Eye - m_Center).Cross(m_Up));
           }
           else
           {
                m_PreviousEye = new Vertex(m_Eye);
                m_PreviousUp = new Vertex(m_Up);
                m_Eye = Matrix.Rotate(angle, v) * (m_Eye - m_Center) + m_Center;
                m_Up = (m_Eye - m_Center).Cross((m_Eye - m_Center).Cross(m_Up));
            }
        }

        public void Fix()
        {  

        }

        public void ApplyCamera(RenderContext renderContex)
        {
            IntPtr hdc = Win32.wglGetCurrentDC();
            IntPtr hglrc = Win32.wglGetCurrentContext();

            Win32.wglMakeCurrent(renderContex.HDC, renderContex.Hglrc);

            gl.MatrixMode(gl.GL_PROJECTION);
            gl.LoadMatrix(m_Projection.ToArray());
            gl.MatrixMode(gl.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.LookAt(m_Eye.x, m_Eye.y, m_Eye.z, m_Center.x, m_Center.y, m_Center.z, m_Up.x, m_Up.y, m_Up.z);

            Win32.wglMakeCurrent(hdc, hglrc);
        }
    }

    /*abstract class Camera
    {
        protected const double ZoomSpeed = 120.0;
        protected RenderContext m_renderContext;
        private Vec3 m_eye, m_center, m_up;        
        private IntPtr p_hglrc, p_hDC;
        private Vec3 m_previouslyEye;
        private Vec3 m_previoslyCenter;
        private Vec3 m_previoslyUp;

        public Vec3 Center { get { return m_center; } }
        public Vec3 Eye { get { return m_eye; } }
        public Vec3 Up { get { return m_up; } }

        protected Camera(RenderContext renderContext)
        {
            Init(new Vec3(0, 0, 1), new Vec3(0, 0, 0), new Vec3(0, 1, 0), renderContext);
        }

        protected Camera(Vec3 eye, Vec3 center, Vec3 up, RenderContext renderContext)
        {
            Init(eye, center, up, renderContext);
        }

        private void Init(Vec3 eye, Vec3 center, Vec3 up, RenderContext renderContext)
        {
            m_renderContext = renderContext;
            p_hglrc = p_hDC = IntPtr.Zero;
            m_center = center;
            m_eye = eye;            
            m_up = up;
            m_previouslyEye = m_eye;
            m_previoslyCenter = m_center;
            m_previoslyUp = m_up;
        }

        protected void BeginUpdate()
        {
            p_hDC = Win32.wglGetCurrentDC();
            p_hglrc = Win32.wglGetCurrentContext();

            if(p_hDC != m_renderContext.HDC || p_hglrc != m_renderContext.Hglrc)
                Win32.wglMakeCurrent(m_renderContext.HDC, m_renderContext.Hglrc);
        }

        protected void EndUpdate()
        {
            if (p_hDC != m_renderContext.HDC || p_hglrc != m_renderContext.Hglrc)
                Win32.wglMakeCurrent(p_hDC, p_hglrc);
        }

        protected abstract void Update();
        public abstract void Zoom(double delta, Vec2 pos);

        public void Move(Vec3 v)
        {
            if (!v.valid())
                return;
            ClearPosition();
            m_eye.x += v.x;
            m_eye.y += v.y;
            m_eye.z += v.z;
            m_center.x += v.x;
            m_center.y += v.y;
            m_center.z += v.z;
            Update();
        }

        public void ClearPosition()
        {
            m_previoslyUp = m_up;
            m_previouslyEye = m_eye;
            m_previoslyCenter = m_center;
        }

        public void RevertPosition()
        {
            m_up = m_previoslyUp;
            m_eye = m_previouslyEye;
            m_center = m_previoslyCenter;
            Update();
        }

        public void Rotate(Vec3 l1, Vec3 l2, float angle)
        {
            if (!l1.valid() || !l2.valid())
                return;

            ClearPosition();
            m_eye = new Vec3(Mat4.rotate(angle, l2 - l1) * new Vec4(m_eye - Center, 1)) + Center;
            m_up = Vec3.cross(Vec3.cross(m_eye - m_center, m_up), m_eye - m_center);
            Update();  
        }
    }

    class OrthographicCamera : Camera
    {
        private double m_left, m_right, m_bottom, m_top, m_near, m_far;

        public OrthographicCamera(double left, double right, double bottom, double top, double near, double far, RenderContext renderContext)
            :base(renderContext)
        {
            m_left = left;
            m_right = right;
            m_bottom = bottom;
            m_top = top;
            m_near = near;
            m_far = far;
            Update();
        }

        public OrthographicCamera(double left, double right, double bottom, double top, double near, double far, Vec3 eye, Vec3 center, Vec3 up, RenderContext renderContext)
            :base(eye, center, up, renderContext)
        {
            m_left = left;
            m_right = right;
            m_bottom = bottom;
            m_top = top;
            m_near = near;
            m_far = far;
            Update();
        }

        public double Left { get { return m_left; } }
        public double Right { get { return m_right; } }
        public double Bottom { get { return m_bottom; } }
        public double Top { get { return m_top; } }
        public double Near { get { return m_near; } }
        public double Far { get { return m_far; } }

        protected override void Update()
        {
            BeginUpdate();

            gl.MatrixMode(gl.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Ortho(m_left, m_right, m_bottom, m_top, m_near, m_far);
            gl.MatrixMode(gl.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.LookAt(Eye.x, Eye.y, Eye.z, Center.x, Center.y, Center.z, Up.x, Up.y, Up.z);

            EndUpdate();
        }

        public override void Zoom(double delta, Vec2 pos)
        {
            double dw = (Right - Left) * delta / ZoomSpeed;
            double dh = (Top - Bottom) * delta / ZoomSpeed;

            m_left += dw / 2.0;
            m_right -= dw / 2.0;
            m_top -= dh / 2.0;
            m_bottom += dh / 2.0;

            var x = -dw * (0.5 - pos.x / m_renderContext.ContextControl.Width);
            var y = -dh * (pos.y / m_renderContext.ContextControl.Height - 0.5);

            var m1 = Mat4.rotate(new Vec3(0, 1, 0).angle(Vec3.normalize(Up)), Vec3.cross(new Vec3(0, 1, 0), Vec3.normalize(Up)));
            var m2 = Mat4.rotate(new Vec3(0, 0, 1).angle(Vec3.normalize(new Vec3(m1 * new Vec4(Eye - Center, 1)))), Vec3.cross(new Vec3(0, 0, 1), new Vec3(m1 * new Vec4(Eye - Center, 1))));

            Vec4 v = new Vec4((float)x, (float)y, 0, 1);
            if (m2.valid()) v = m2.inverse() * v;
            if (m1.valid()) v = m1.inverse() * v;

            Move(new Vec3(v));
        }
    }*/
}