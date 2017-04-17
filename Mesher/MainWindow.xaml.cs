using System;
using System.Windows;
using System.Windows.Forms;
using Mesher.GraphicsCore;

namespace Mesher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RenderContext renderContext;

        public MainWindow()
        {
            InitializeComponent();

            windowsFormsHostRenderContext.Child = new Control();
            renderContext = new RenderContext(windowsFormsHostRenderContext.Child.Handle);

            var timer = new Timer();
            timer.Interval = 1;
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Child_MouseClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
           // renderContext.Render();
        }

        private void WindowsFormsHostRenderContext_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            renderContext.ResizeContext((int)e.NewSize.Width, (int)e.NewSize.Height);
        }
    }
}
