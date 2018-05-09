using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Mesher.Core.Data;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Primitives;
using Mesher.GraphicsCore.RenderContexts;

namespace Mesher.Core.Test
{
    public partial class MainWindow : Form
    {
        //private RScene m_rScene;
        private RTriangles m_rTriangles;

        private GlWindowsGraphics m_graphics;
        //public SceneForm.SceneForm SceneForm;

        public MainWindow()
        {
            InitializeComponent();

            m_graphics = new GlWindowsGraphics((GlWindowsRenderContext) sceneContext1.RenderContext);

            m_rTriangles = m_graphics.CreateRTriangles();
            sceneContext1.RenderersFactory = m_graphics.RenderersFactory;
            //sceneContext1.Add(new Axises(sceneContext1));
            sceneContext1.CameraControler = new ArcBallCameraControler(sceneContext1);


            sceneContext1.MouseWheel += SceneContext1_MouseWheel;
        }

        private void SceneContext1_MouseWheel(Object sender, MouseEventArgs e)
        {
            Render();
        }

        private void Render()
        {
            if (m_rTriangles == null)
                return;

            sceneContext1.Render(m_rTriangles);
        }

        private void sceneContext1_MouseMove(Object sender, MouseEventArgs e)
        {
            Render();
        }

        private void sceneContext1_Resize(Object sender, EventArgs e)
        {
            Render();
        }

        private void sceneContext1_Paint(Object sender, PaintEventArgs e)
        {
            Render();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = @"OBJ (*.obj)|*.obj|3DS (*.3ds)|*.3ds";

                openFileDialog.ShowDialog();

                if (File.Exists(openFileDialog.FileName))
                {
                    m_rTriangles = DataLoader.LoadScene(openFileDialog.FileName, m_graphics);
                }
            }

            sceneContext1.Update();
            Render();
        }
    }
}
