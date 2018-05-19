using System;
using System.IO;
using System.Windows.Forms;
using Mesher.Core.Events.EventArgs;
using Mesher.Core.Plugins;

namespace Mesher.Core
{
    public partial class MainWindow : Form
    {
        private readonly MesherApplication m_mesherApplication;

        public MainWindow(MesherApplication mesherApplication)
        {
            m_mesherApplication = mesherApplication;
            InitializeComponent();

            foreach (var plugin in mesherApplication.PluginsSystem.Plugins)
            {
                var item = toolStripMenuItemPlugins.DropDownItems.Add(plugin.Name);
                item.Tag = plugin;
                item.Click += Item_Click;
            }

            DocumentView.CameraControler = new ArcBallCameraControler(DocumentView);
            DocumentView.MouseWheel += DocumentViewMouseWheel;
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem) sender;
            var plugin = (Plugin) menuItem.Tag;

            plugin.Execute();
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

        private void sceneContext1_MouseMove(Object sender, MouseMoveEventArgs e)
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
