using System;
using System.IO;
using System.Windows.Forms;

namespace Mesher.Core
{
    public partial class MainWindow : Form
    {
        private readonly MesherApplication m_mesherApplication;

        public MainWindow(MesherApplication mesherApplication)
        {
            m_mesherApplication = mesherApplication;
            InitializeComponent();
            
            DocumentView.CameraControler = new ArcBallCameraControler(DocumentView);
            DocumentView.MouseWheel += DocumentViewMouseWheel;
        }
        
        private void Render()
        {
            if (m_mesherApplication.CurrentDocument == null)
                return;

           m_mesherApplication.CurrentDocument.Render();
        }

        private void DocumentViewMouseWheel(Object sender, MouseEventArgs e)
        {
            Render();
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
                    m_mesherApplication.CurrentDocument = m_mesherApplication.LoadDocument(openFileDialog.FileName);
                    m_mesherApplication.CurrentDocument.Rebuild();
                    DocumentView.Document = m_mesherApplication.CurrentDocument;
                }
            }

            DocumentView.Update();
            Render();
        }
    }
}
