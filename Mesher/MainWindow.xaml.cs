using Mesher.Mathematics;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using Mesher.GraphicsCore.BufferObjects;
using Mesher.GraphicsCore.Camera;
using Mesher.GraphicsCore.Drawing;
using Mesher.GraphicsCore.Texture;
using Triangle = Mesher.GraphicsCore.Drawing.Triangle;

namespace Mesher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Camera m_camera;

        public MainWindow()
        {
            InitializeComponent();

            m_camera = new OrthographicsCamera(RenderContext);
            RenderContext.MouseMove += RenderContext_MouseMove;
            RenderContext.MouseWheel += RenderContext_MouseWheel;

            mesh = new Mesh(new[]
                {new Mathematics.Triangle(new Vertex(-1, 0), new Vertex(0, 1), new Vertex(1, 0))}, null, null);

            texture = new Texture((Bitmap)Image.FromFile(@"C:\Users\backsword\Desktop\other\51076.jpg"));
        }

        private Mesh mesh;
        private Texture texture;

        private void RenderContext_MouseWheel(object sender, MouseEventArgs e)
        {
            Render();
        }

        private void Render()
        {
            RenderContext.BeginRender(true);
            
            mesh.Render();
          //  TextureTriangle.Draw(new Mathematics.Triangle(new Vertex(-1, -1, 0), new Vertex(1, -1, 0), new Vertex(0, 1, 0)), 
             //   new Mathematics.Triangle(new Vertex(0, 0), new Vertex(0, 1), new Vertex(1, 1)), texture);
            Grid.Draw(RenderContext);

            RenderContext.EndRender(true);
        }

        private void RenderContext_MouseMove(object sender, MouseEventArgs e)
        {
            Render();
        }

        private void DockPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Render();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (m_camera is OrthographicsCamera)
                return;
            m_camera.Dispose();
            m_camera = new OrthographicsCamera(RenderContext);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (m_camera is PerspectiveCamera)
                return;
            m_camera.Dispose();
            m_camera = new PerspectiveCamera(45, 0.5, 20, RenderContext);
        }
    }
}
