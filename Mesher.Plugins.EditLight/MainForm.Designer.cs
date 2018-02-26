using Mesher.Core.Components;
using Mesher.GraphicsCore;

namespace Mesher.Plugins.EditLight
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.sceneContext1 = new SceneContext(this.renderManager);
            // 
            // sceneContext1
            // 
            this.sceneContext1.Camera = null;
            this.sceneContext1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceneContext1.Location = new System.Drawing.Point(0, 24);
            this.sceneContext1.Name = "sceneContext1";
            this.sceneContext1.Size = new System.Drawing.Size(596, 314);
            this.sceneContext1.TabIndex = 0;
            this.sceneContext1.Paint += new System.Windows.Forms.PaintEventHandler(this.sceneContext1_Paint);
            this.sceneContext1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.sceneContext1_MouseMove);
            this.sceneContext1.Resize += new System.EventHandler(this.sceneContext1_Resize);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 338);
            this.Controls.Add(this.sceneContext1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Core.Components.SceneContext sceneContext1;
        private RenderManager renderManager;
    }
}

