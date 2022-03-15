namespace Watson
{
    partial class TMPFontImportWindow
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
            this.label3 = new System.Windows.Forms.Label();
            this.OldDataFoldertextBox = new System.Windows.Forms.TextBox();
            this.OpenOldDatabutton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.NewDataFoldertextBox = new System.Windows.Forms.TextBox();
            this.OpenNewDatabutton = new System.Windows.Forms.Button();
            this.Submitbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Asset al que quieras importar las fuentes:";
            // 
            // OldAssettextBox
            // 
            this.OldAssettextBox.Location = new System.Drawing.Point(27, 42);
            this.OldAssettextBox.Name = "OldAssettextBox";
            this.OldAssettextBox.Size = new System.Drawing.Size(207, 23);
            this.OldAssettextBox.TabIndex = 2;
            this.OldAssettextBox.Text = "E:\\Games\\AI The Somnium Files\\AI_TheSomniumFiles_Data\\StreamingAssets\\AssetBundle" +
    "s\\StandaloneWindows64\\fonts";
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
            this.label2.Location = new System.Drawing.Point(12, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Asset con las fuentes actualizadas:";
            // 
            // NewAssettextBox
            // 
            this.NewAssettextBox.Location = new System.Drawing.Point(27, 132);
            this.NewAssettextBox.Name = "NewAssettextBox";
            this.NewAssettextBox.Size = new System.Drawing.Size(207, 23);
            this.NewAssettextBox.TabIndex = 5;
            this.NewAssettextBox.Text = "F:\\Unity\\AI\\Builds\\AI_Data\\sharedassets0.assets";
            // 
            // OpenNewFilebutton
            // 
            this.OpenNewFilebutton.Location = new System.Drawing.Point(240, 132);
            this.OpenNewFilebutton.Name = "OpenNewFilebutton";
            this.OpenNewFilebutton.Size = new System.Drawing.Size(75, 23);
            this.OpenNewFilebutton.TabIndex = 6;
            this.OpenNewFilebutton.Text = "Examinar";
            this.OpenNewFilebutton.UseVisualStyleBackColor = true;
            this.OpenNewFilebutton.Click += new System.EventHandler(this.OpenNewFilebutton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Carpeta Original Data:";
            // 
            // OldDataFoldertextBox
            // 
            this.OldDataFoldertextBox.Location = new System.Drawing.Point(27, 86);
            this.OldDataFoldertextBox.Name = "OldDataFoldertextBox";
            this.OldDataFoldertextBox.Size = new System.Drawing.Size(207, 23);
            this.OldDataFoldertextBox.TabIndex = 8;
            this.OldDataFoldertextBox.Text = "E:\\Games\\AI The Somnium Files\\AI_TheSomniumFiles_Data";
            // 
            // OpenOldDatabutton
            // 
            this.OpenOldDatabutton.Location = new System.Drawing.Point(240, 86);
            this.OpenOldDatabutton.Name = "OpenOldDatabutton";
            this.OpenOldDatabutton.Size = new System.Drawing.Size(75, 23);
            this.OpenOldDatabutton.TabIndex = 9;
            this.OpenOldDatabutton.Text = "Examinar";
            this.OpenOldDatabutton.UseVisualStyleBackColor = true;
            this.OpenOldDatabutton.Click += new System.EventHandler(this.OpenOldDatabutton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Carpeta Nueva Original";
            // 
            // NewDataFoldertextBox
            // 
            this.NewDataFoldertextBox.Location = new System.Drawing.Point(27, 176);
            this.NewDataFoldertextBox.Name = "NewDataFoldertextBox";
            this.NewDataFoldertextBox.Size = new System.Drawing.Size(207, 23);
            this.NewDataFoldertextBox.TabIndex = 11;
            this.NewDataFoldertextBox.Text = "F:\\Unity\\AI\\Builds\\AI_Data";
            // 
            // OpenNewDatabutton
            // 
            this.OpenNewDatabutton.Location = new System.Drawing.Point(240, 176);
            this.OpenNewDatabutton.Name = "OpenNewDatabutton";
            this.OpenNewDatabutton.Size = new System.Drawing.Size(75, 23);
            this.OpenNewDatabutton.TabIndex = 12;
            this.OpenNewDatabutton.Text = "Examinar";
            this.OpenNewDatabutton.UseVisualStyleBackColor = true;
            this.OpenNewDatabutton.Click += new System.EventHandler(this.OpenNewDatabutton_Click);
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
            // TMPFontImportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 257);
            this.Controls.Add(this.Submitbutton);
            this.Controls.Add(this.OpenNewDatabutton);
            this.Controls.Add(this.NewDataFoldertextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.OpenOldDatabutton);
            this.Controls.Add(this.OldDataFoldertextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OpenNewFilebutton);
            this.Controls.Add(this.NewAssettextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OpenOldFilebutton);
            this.Controls.Add(this.OldAssettextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TMPFontImportWindow";
            this.Text = "Watson - TMPFontImportWindow";
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
        private Label label3;
        private TextBox OldDataFoldertextBox;
        private Button OpenOldDatabutton;
        private Label label4;
        private TextBox NewDataFoldertextBox;
        private Button OpenNewDatabutton;
        private Button Submitbutton;
    }
}