using System;
using System.Windows.Forms;
using Mesher.Core.Objects.Scene;
using Mesher.Core.Renderers;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Data.OpenGL;

namespace Mesher.Plugins.EditLight
{
    public partial class MainForm : Form
    {
        public MainForm(GlDataContext context, Scene scene, SceneRendererBase sceneRenderer)
        { 
            InitializeComponent();
        }
    }
}
