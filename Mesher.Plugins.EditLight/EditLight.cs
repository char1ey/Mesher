using System;
using Mesher.Core.Plugins;
using Mesher.Core.Objects.Scene;
using Mesher.Core.Renderers;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Data.OpenGL;

namespace Mesher.Plugins.EditLight
{
    public class EditLight : IPlugin
    {
        private MainForm m_form;

        public String Name { get; }

        public EditLight()
        {
            Name = @"Edit light";
        }

        public void Execute(GlDataContext context, Scene scene, SceneRendererBase sceneRenderer)
        {
            m_form = new MainForm(context, scene, sceneRenderer);
            m_form.Show();
        }
    }
}
