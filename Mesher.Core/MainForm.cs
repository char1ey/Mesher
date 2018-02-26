using System;
using System.IO;
using System.Windows.Forms;
using Mesher.Core.Plugins;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Objects;
using DataLoader = Mesher.Core.Data.DataLoader;

namespace Mesher.Core
{
    public partial class MainForm : Form
    {
        public Scene Scene;

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
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var plugin = (IPlugin) ((ToolStripItem) sender).Tag;

            plugin.Execute(renderManager, Scene);
        }

        private void SceneContext1_MouseWheel(Object sender, MouseEventArgs e)
        {
            Render();
        }

        private void Render()
        {
            if (Scene == null)
                return;

            sceneContext1.BeginRender();

            sceneContext1.Render(Scene);

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
                    Scene = DataLoader.LoadScene(openFileDialog.FileName, renderManager);
                }
            }

            sceneContext1.Update();
            Render();
        }
    }
}
