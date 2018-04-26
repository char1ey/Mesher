using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mesher.Core.Data;
using Mesher.Core.Objects.Scene;
using Mesher.Core.Plugins;
using Mesher.Core.Renderers;
using Mesher.Core.SceneContexts.Components;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.Primitives;

namespace Mesher.Core
{
    public partial class GCoreTest : Form
    {
        public Scene Scene;

        private RScene m_rScene;

        //public SceneForm.SceneForm SceneForm;

        public GCoreTest()
        {
            InitializeComponent();

            sceneContext1.MouseWheel += SceneContext1_MouseWheel;

            var plugins = PluginSystem.GetPlugins(Path.Combine(Environment.CurrentDirectory));

            foreach (var plugin in plugins)
            {
                var item = toolStripMenuItemPlugins.DropDownItems.Add(plugin.Name);
                item.Tag = plugin;
            }

  
            //SceneForm = new SceneForm.SceneForm(sceneContext1, m_sceneRenderer);
            sceneContext1.Scene = Scene;
            //sceneContext1.SceneRenderer = m_sceneRenderer;
            sceneContext1.Add(new Axises(sceneContext1));
            sceneContext1.CameraControler = new ArcBallCameraControler(sceneContext1);
        }

        private String GetShaderSource(Byte[] bytes)
        {
            return new String(bytes.Select(t => (Char)t).ToArray());
        }

        private void SceneContext1_MouseWheel(Object sender, MouseEventArgs e)
        {
            Render();
        }

        private void Render()
        {
            if (Scene == null)
                return;
            sceneContext1.Scene = Scene;
            sceneContext1.Render();
            /*sceneContext1.BeginRender();

            sceneContext1.RenderTriangles(Scene, m_sceneRenderer);

            sceneContext1.EndRender();*/
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
                    Scene = DataLoader.LoadScene(openFileDialog.FileName, m_dataContext);
                }
            }

            sceneContext1.Update();
            Render();
        }
    }
}
