using System;
using System.Windows.Forms;
using System.Drawing;

namespace Mesher.GraphicsCore
{
    public partial class RenderContextPrototype : IDisposable
    {
        private readonly RenderContext m_renderContext;

        public event EventHandler ContextResize;

        public new event MouseEventHandler MouseMove;
        public new event MouseEventHandler MouseWheel;

        public new event System.Windows.Input.KeyEventHandler KeyUp;
        public new event System.Windows.Input.KeyEventHandler KeyDown;

        public Color ClearColor { get; set; }

        public RenderContextPrototype(Color color)
        {
            InitializeComponent();

            WindowsFormsHost.Child = new Control();

            m_renderContext = new RenderContext(WindowsFormsHost.Child.Handle);

            WindowsFormsHost.Child.MouseWheel += Child_MouseWheel;
            WindowsFormsHost.Child.MouseMove += Child_MouseMove;

            WindowsFormsHost.KeyDown += WindowsFormsHost_KeyDown;
            WindowsFormsHost.KeyUp += WindowsFormsHost_KeyUp;

            WindowsFormsHost.Child.Resize += Child_Resize;

            m_renderContext.Begin();
            Gl.ClearColor(color.R / 256f, color.G / 256f, color.B / 256f, 1.0f);
            m_renderContext.End();
        }

        public RenderContextPrototype() : this(Color.DimGray) { }

        private void Child_MouseWheel(object sender, MouseEventArgs e)
        {
            MouseWheel?.Invoke(sender, e);
        }

        private void WindowsFormsHost_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyUp?.Invoke(sender, e);
        }

        private void WindowsFormsHost_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyDown?.Invoke(sender, e);
        }

        private void Child_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMove?.Invoke(sender, e);
        }

        private void Child_Resize(object sender, EventArgs e)
        {
            var control = (Control)sender;
            Width = control.Width;
            Height = control.Height;
            m_renderContext.ResizeWindow(control.Width, control.Height);
            ContextResize?.Invoke(sender, e);
        }

        public void BeginRender(bool clear = false)
        {
            m_renderContext.Begin();
            if (clear)
                Gl.Clear(Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_COLOR_BUFFER_BIT);
        }

        public void EndRender(bool swapBuffers = false)
        {
            if (swapBuffers)
                m_renderContext.SwapBuffers();
            m_renderContext.End();
        }

        public void Dispose()
        {
            m_renderContext.Dispose();
        }
    }
}
