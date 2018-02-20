using Mesher.Core.Components;
using Mesher.GraphicsCore;

namespace Mesher.Core
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
            this.renderManager = new RenderManager(Handle);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPlugins = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            this.sceneContext1 = new SceneContext(this.renderManager);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItemFile,
            this.toolStripMenuItemPlugins});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(596, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItemFile
            // 
            this.fileToolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItemFile.Name = "fileToolStripMenuItemFile";
            this.fileToolStripMenuItemFile.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItemFile.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
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
            // toolStripMenuItemPlugins
            // 
            this.toolStripMenuItemPlugins.Name = "toolStripMenuItemPlugins";
            this.toolStripMenuItemPlugins.Size = new System.Drawing.Size(58, 20);
            this.toolStripMenuItemPlugins.Text = "Plugins";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 338);
            this.Controls.Add(this.sceneContext1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Components.SceneContext sceneContext1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private RenderManager renderManager;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlugins;
    }
}

