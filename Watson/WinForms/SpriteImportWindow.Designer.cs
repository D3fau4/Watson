namespace Watson
{
    partial class SpriteImportWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.OldAssettextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.OpenOldFilebutton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.NewAssettextBox = new System.Windows.Forms.TextBox();
            this.OpenNewFilebutton = new System.Windows.Forms.Button();
            this.Submitbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Asset al que quieras importar los Sprites:";
            // 
            // OldAssettextBox
            // 
            this.OldAssettextBox.Location = new System.Drawing.Point(27, 42);
            this.OldAssettextBox.Name = "OldAssettextBox";
            this.OldAssettextBox.Size = new System.Drawing.Size(207, 23);
            this.OldAssettextBox.TabIndex = 2;
            this.OldAssettextBox.Text = "E:\\Games\\AI The Somnium Files\\AI_TheSomniumFiles_Data\\StreamingAssets\\AssetBundle" +
    "s\\StandaloneWindows64\\etc.bak";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // OpenOldFilebutton
            // 
            this.OpenOldFilebutton.Location = new System.Drawing.Point(240, 42);
            this.OpenOldFilebutton.Name = "OpenOldFilebutton";
            this.OpenOldFilebutton.Size = new System.Drawing.Size(75, 23);
            this.OpenOldFilebutton.TabIndex = 3;
            this.OpenOldFilebutton.Text = "Examinar";
            this.OpenOldFilebutton.UseVisualStyleBackColor = true;
            this.OpenOldFilebutton.Click += new System.EventHandler(this.OpenOldFilebutton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Asset con los sprites actualizados:";
            // 
            // NewAssettextBox
            // 
            this.NewAssettextBox.Location = new System.Drawing.Point(27, 90);
            this.NewAssettextBox.Name = "NewAssettextBox";
            this.NewAssettextBox.Size = new System.Drawing.Size(207, 23);
            this.NewAssettextBox.TabIndex = 5;
            this.NewAssettextBox.Text = "F:\\Unity\\AI\\Builds\\win64\\AI_Data\\sharedassets1.assets";
            // 
            // OpenNewFilebutton
            // 
            this.OpenNewFilebutton.Location = new System.Drawing.Point(240, 90);
            this.OpenNewFilebutton.Name = "OpenNewFilebutton";
            this.OpenNewFilebutton.Size = new System.Drawing.Size(75, 23);
            this.OpenNewFilebutton.TabIndex = 6;
            this.OpenNewFilebutton.Text = "Examinar";
            this.OpenNewFilebutton.UseVisualStyleBackColor = true;
            this.OpenNewFilebutton.Click += new System.EventHandler(this.OpenNewFilebutton_Click);
            // 
            // Submitbutton
            // 
            this.Submitbutton.Location = new System.Drawing.Point(124, 218);
            this.Submitbutton.Name = "Submitbutton";
            this.Submitbutton.Size = new System.Drawing.Size(75, 23);
            this.Submitbutton.TabIndex = 13;
            this.Submitbutton.Text = "Aceptar";
            this.Submitbutton.UseVisualStyleBackColor = true;
            this.Submitbutton.Click += new System.EventHandler(this.Submitbutton_Click);
            // 
            // SpriteImportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 257);
            this.Controls.Add(this.Submitbutton);
            this.Controls.Add(this.OpenNewFilebutton);
            this.Controls.Add(this.NewAssettextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OpenOldFilebutton);
            this.Controls.Add(this.OldAssettextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SpriteImportWindow";
            this.Text = "Watson - SpriteImportWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label1;
        private TextBox OldAssettextBox;
        private OpenFileDialog openFileDialog1;
        private Button OpenOldFilebutton;
        private Label label2;
        private TextBox NewAssettextBox;
        private Button OpenNewFilebutton;
        private Button Submitbutton;
    }
}