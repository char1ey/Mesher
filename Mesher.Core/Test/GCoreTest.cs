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
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.Data.OpenGL;
using Mesher.GraphicsCore.Primitives;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.GraphicsCore.Renderers;
using Mesher.GraphicsCore.Renderers.OpenGL;

namespace Mesher.Core
{
    public partial class GCoreTest : Form
    {
        private RScene m_rScene;

        private RSceneRenderer m_sceneRenderer;

        private GlWindowsGraphics m_graphics;
        //public SceneForm.SceneForm SceneForm;

        public GCoreTest()
        {
            InitializeComponent();

            m_graphics = new GlWindowsGraphics((GlWindowsRenderContext) sceneContext1.RenderContext);

            
            m_rScene = m_graphics.CreateRScene();
            sceneContext1.Scene = m_rScene;

            m_sceneRenderer = m_graphics.GetRSceneRenderer();
            sceneContext1.SceneRenderer = m_sceneRenderer;
            //sceneContext1.Add(new Axises(sceneContext1));
            sceneContext1.CameraControler = new ArcBallCameraControler(sceneContext1);


            sceneContext1.MouseWheel += SceneContext1_MouseWheel;
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
            if (m_rScene == null)
                return;
            sceneContext1.Scene = m_rScene;
            sceneContext1.Render();
            sceneContext1.BeginRender();

            sceneContext1.Render();

            sceneContext1.EndRender();
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
                    m_rScene = DataLoader.LoadScene(openFileDialog.FileName, m_graphics);
                }
            }

            sceneContext1.Update();
            Render();
        }
    }
}
