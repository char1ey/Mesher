using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Mesher.Core.Objects.Scene;
using Mesher.Core.Plugins;
using Mesher.Core.Renderers;
using DataLoader = Mesher.Core.Data.DataLoader;

namespace Mesher.Core
{
    public partial class MainForm : Form
    {
        private RendererBase m_renderer;

        public Scene Scene;

        public SceneForm.SceneForm SceneForm;
        
        public MainForm()
        {
            InitializeComponent();
            
            sceneContext1.MouseWheel += SceneContext1_MouseWheel;

            var plugins = PluginSystem.GetPlugins(Path.Combine(Environment.CurrentDirectory));

            foreach (var plugin in plugins)
            {
                var item = toolStripMenuItemPlugins.DropDownItems.Add(plugin.Name);
                item.Tag = plugin;
                item.Click += Item_Click;
            }

            m_renderer = new RendererDefault(m_dataContext, GetShaderSource(Properties.Resources.DefaultVertexShaderProgramSource), 
                                                              GetShaderSource(Properties.Resources.DefaultFragmentShaderProgramSource));

            SceneForm = new SceneForm.SceneForm(sceneContext1, m_renderer);
            SceneForm.Scene = Scene;
        }

        private String GetShaderSource(Byte[] bytes)
        {
            return new String(bytes.Select(t => (Char)t).ToArray());
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var plugin = (IPlugin) ((ToolStripItem) sender).Tag;

            plugin.Execute(m_dataContext, Scene, m_renderer);
        }

        private void SceneContext1_MouseWheel(Object sender, MouseEventArgs e)
        {
            Render();
        }

        private void Render()
        {
            if (Scene == null)
                return;
            SceneForm.Scene = Scene;
            SceneForm.Render();
            /*sceneContext1.BeginRender();

            sceneContext1.Render(Scene, m_renderer);

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
