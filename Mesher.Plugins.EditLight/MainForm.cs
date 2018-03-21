using System;
using System.Windows.Forms;
using Mesher.Core.Objects.Scene;
using Mesher.Core.Renderers;
using Mesher.GraphicsCore;

namespace Mesher.Plugins.EditLight
{
    public partial class MainForm : Form
    {
        public Scene m_scene;
        private RendererBase m_renderer;

        public MainForm(RenderContext context, Scene scene, RendererBase renderer)
        {
            m_scene = scene;
            m_renderContext = context;

            InitializeComponent();
            
            sceneContext1.MouseWheel += SceneContext1_MouseWheel;

            m_renderer = renderer;
        }

        private void SceneContext1_MouseWheel(Object sender, MouseEventArgs e)
        {
            Render();
        }

        private void Render()
        {
            if (m_scene == null)
                return;

            sceneContext1.BeginRender();

            sceneContext1.Render(m_scene, m_renderer);

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
    }
}
