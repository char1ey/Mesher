using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.BufferObjects;
using Mesher.GraphicsCore.Camera;
using Mesher.GraphicsCore.Drawing;
using Mesher.GraphicsCore.Texture;
using Mesher.Mathematics;

namespace Mesher
{
    public partial class MainForm : Form
    {
        public MainForm()
        {       
            InitializeComponent();

            texture = new Texture((Bitmap)Image.FromFile(@"C:\Users\backsword\Desktop\other\51076.jpg"));
            sceneContext1.MouseWheel += SceneContext1_MouseWheel;
            mesh = new Mesh(new[]
                {new Vec3(0, -1, -1), new Vec3(0, 1, -1), new Vec3(0, 0, 1), new Vec3(0, 0, -3)}, new[]
            {
                new Vec2(0, 0), new Vec2(0, 1), new Vec2(1, 1), new Vec2(0.5, 0.5)
            }, null, new[] { 0, 1, 2, 0, 1, 3 }, texture);
        }

        private void SceneContext1_MouseWheel(object sender, MouseEventArgs e)
        {
            Render();
        }

        private Mesh mesh;
        private Texture texture;

        private void Render()
        {
            sceneContext1.BeginRender();
            mesh.Render();
            // TextureTriangle.Draw(new Mathematics.Triangle(new Vertex(-1, -1, 0), new Vertex(1, -1, 0), new Vertex(0, 1, 0)), 
            //    new Mathematics.Triangle(new Vertex(0, 0), new Vertex(0, 1), new Vertex(1, 1)), texture);
            Grid.Draw();
            sceneContext1.EndRender();
        }

        private void sceneContext1_MouseMove(object sender, MouseEventArgs e)
        {
            Render();
        }

        private void sceneContext1_Resize(object sender, EventArgs e)
        {
            Render();
        }
    }
}
