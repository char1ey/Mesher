﻿using System;
using System.IO;
using System.Windows.Forms;
using Mesher.Core.Objects;
using Mesher.Core.Objects.Scene;
using Mesher.GraphicsCore;
using DataLoader = Mesher.Core.Data.DataLoader;

namespace Mesher.Plugins.EditLight
{
    public partial class MainForm : Form
    {
        public Scene m_scene;

        public MainForm(RenderContext context, Scene scene)
        {
            m_scene = scene;
            m_renderContext = context;

            InitializeComponent();
            
            sceneContext1.MouseWheel += SceneContext1_MouseWheel;
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

            sceneContext1.Render(m_scene);

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