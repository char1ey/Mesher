namespace Mesher
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
            this.sceneContext1 = new Mesher.Components.SceneContext();
            this.SuspendLayout();
            // 
            // sceneContext1
            // 
            this.sceneContext1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sceneContext1.Location = new System.Drawing.Point(0, 0);
            this.sceneContext1.Name = "sceneContext1";
            this.sceneContext1.Size = new System.Drawing.Size(918, 490);
            this.sceneContext1.TabIndex = 0;
            this.sceneContext1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.sceneContext1_MouseMove);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 490);
            this.Controls.Add(this.sceneContext1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Components.SceneContext sceneContext1;
    }
}

