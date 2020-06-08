namespace ELA
{
    partial class Form
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.startButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Kontrast = new System.Windows.Forms.Label();
            this.contrastTextBox = new System.Windows.Forms.TextBox();
            this.saveTxtCheckBox = new System.Windows.Forms.CheckBox();
            this.saveDecodedImagecheckBox = new System.Windows.Forms.CheckBox();
            this.qualityTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chooseImageButton = new System.Windows.Forms.Button();
            this.expandButton = new System.Windows.Forms.Button();
            this.SetDecodedAsInputButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.deleteInputButton = new System.Windows.Forms.Button();
            this.deleteOutputButton = new System.Windows.Forms.Button();
            this.expandImageButton = new System.Windows.Forms.Button();
            this.loadingPicture = new System.Windows.Forms.PictureBox();
            this.decodedPictureBox = new System.Windows.Forms.PictureBox();
            this.loadedPictureBox = new System.Windows.Forms.PictureBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.saveELACheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.loadingPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.decodedPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadedPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(20)))));
            this.startButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.startButton.FlatAppearance.BorderSize = 0;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.ForeColor = System.Drawing.Color.White;
            this.startButton.Location = new System.Drawing.Point(58, 354);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(101, 30);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Starten";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Qualität";
            // 
            // Kontrast
            // 
            this.Kontrast.AutoSize = true;
            this.Kontrast.ForeColor = System.Drawing.Color.White;
            this.Kontrast.Location = new System.Drawing.Point(12, 128);
            this.Kontrast.Name = "Kontrast";
            this.Kontrast.Size = new System.Drawing.Size(72, 13);
            this.Kontrast.TabIndex = 4;
            this.Kontrast.Text = "Muliplikator";
            // 
            // contrastTextBox
            // 
            this.contrastTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(20)))));
            this.contrastTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.contrastTextBox.ForeColor = System.Drawing.Color.MediumAquamarine;
            this.contrastTextBox.Location = new System.Drawing.Point(90, 128);
            this.contrastTextBox.Name = "contrastTextBox";
            this.contrastTextBox.Size = new System.Drawing.Size(118, 14);
            this.contrastTextBox.TabIndex = 5;
            this.contrastTextBox.Text = "30";
            this.contrastTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // saveTxtCheckBox
            // 
            this.saveTxtCheckBox.AutoSize = true;
            this.saveTxtCheckBox.ForeColor = System.Drawing.Color.White;
            this.saveTxtCheckBox.Location = new System.Drawing.Point(15, 169);
            this.saveTxtCheckBox.Name = "saveTxtCheckBox";
            this.saveTxtCheckBox.Size = new System.Drawing.Size(170, 17);
            this.saveTxtCheckBox.TabIndex = 6;
            this.saveTxtCheckBox.Text = "Zwischenwerte speichern";
            this.saveTxtCheckBox.UseVisualStyleBackColor = true;
            // 
            // saveDecodedImagecheckBox
            // 
            this.saveDecodedImagecheckBox.AutoSize = true;
            this.saveDecodedImagecheckBox.ForeColor = System.Drawing.Color.White;
            this.saveDecodedImagecheckBox.Location = new System.Drawing.Point(15, 192);
            this.saveDecodedImagecheckBox.Name = "saveDecodedImagecheckBox";
            this.saveDecodedImagecheckBox.Size = new System.Drawing.Size(200, 17);
            this.saveDecodedImagecheckBox.TabIndex = 9;
            this.saveDecodedImagecheckBox.Text = "Umgewandeltes Bild speichern";
            this.saveDecodedImagecheckBox.UseVisualStyleBackColor = true;
            // 
            // qualityTextBox
            // 
            this.qualityTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(20)))));
            this.qualityTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.qualityTextBox.ForeColor = System.Drawing.Color.MediumAquamarine;
            this.qualityTextBox.Location = new System.Drawing.Point(90, 93);
            this.qualityTextBox.Name = "qualityTextBox";
            this.qualityTextBox.Size = new System.Drawing.Size(118, 14);
            this.qualityTextBox.TabIndex = 7;
            this.qualityTextBox.Text = "75";
            this.qualityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(236, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Original Bild";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(714, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Encodiertes/Decodiertes Bild";
            // 
            // chooseImageButton
            // 
            this.chooseImageButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(20)))));
            this.chooseImageButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.chooseImageButton.FlatAppearance.BorderSize = 0;
            this.chooseImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chooseImageButton.ForeColor = System.Drawing.Color.White;
            this.chooseImageButton.Location = new System.Drawing.Point(588, 57);
            this.chooseImageButton.Name = "chooseImageButton";
            this.chooseImageButton.Size = new System.Drawing.Size(101, 30);
            this.chooseImageButton.TabIndex = 9;
            this.chooseImageButton.Text = "Bild auswählen";
            this.chooseImageButton.UseVisualStyleBackColor = false;
            this.chooseImageButton.Click += new System.EventHandler(this.chooseImageButton_Click);
            // 
            // expandButton
            // 
            this.expandButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(20)))));
            this.expandButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.expandButton.FlatAppearance.BorderSize = 0;
            this.expandButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.expandButton.ForeColor = System.Drawing.Color.White;
            this.expandButton.Location = new System.Drawing.Point(695, 210);
            this.expandButton.Name = "expandButton";
            this.expandButton.Size = new System.Drawing.Size(16, 67);
            this.expandButton.TabIndex = 10;
            this.expandButton.Text = ">";
            this.expandButton.UseVisualStyleBackColor = false;
            this.expandButton.Click += new System.EventHandler(this.expandButton_Click);
            // 
            // SetDecodedAsInputButton
            // 
            this.SetDecodedAsInputButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(20)))));
            this.SetDecodedAsInputButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.SetDecodedAsInputButton.FlatAppearance.BorderSize = 0;
            this.SetDecodedAsInputButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SetDecodedAsInputButton.ForeColor = System.Drawing.Color.White;
            this.SetDecodedAsInputButton.Location = new System.Drawing.Point(1056, 57);
            this.SetDecodedAsInputButton.Name = "SetDecodedAsInputButton";
            this.SetDecodedAsInputButton.Size = new System.Drawing.Size(111, 30);
            this.SetDecodedAsInputButton.TabIndex = 11;
            this.SetDecodedAsInputButton.Text = "Als Input setzen";
            this.SetDecodedAsInputButton.UseVisualStyleBackColor = false;
            this.SetDecodedAsInputButton.Click += new System.EventHandler(this.SetDecodedAsInputButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label4.Location = new System.Drawing.Point(11, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 23);
            this.label4.TabIndex = 12;
            this.label4.Text = "Error Level Analyse";
            // 
            // deleteInputButton
            // 
            this.deleteInputButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(20)))));
            this.deleteInputButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.deleteInputButton.FlatAppearance.BorderSize = 0;
            this.deleteInputButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteInputButton.ForeColor = System.Drawing.Color.White;
            this.deleteInputButton.Location = new System.Drawing.Point(521, 57);
            this.deleteInputButton.Name = "deleteInputButton";
            this.deleteInputButton.Size = new System.Drawing.Size(61, 30);
            this.deleteInputButton.TabIndex = 13;
            this.deleteInputButton.Text = "löschen";
            this.deleteInputButton.UseVisualStyleBackColor = false;
            this.deleteInputButton.Click += new System.EventHandler(this.deleteInputButton_Click);
            // 
            // deleteOutputButton
            // 
            this.deleteOutputButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(20)))));
            this.deleteOutputButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.deleteOutputButton.FlatAppearance.BorderSize = 0;
            this.deleteOutputButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteOutputButton.ForeColor = System.Drawing.Color.White;
            this.deleteOutputButton.Location = new System.Drawing.Point(989, 57);
            this.deleteOutputButton.Name = "deleteOutputButton";
            this.deleteOutputButton.Size = new System.Drawing.Size(61, 30);
            this.deleteOutputButton.TabIndex = 14;
            this.deleteOutputButton.Text = "löschen";
            this.deleteOutputButton.UseVisualStyleBackColor = false;
            this.deleteOutputButton.Click += new System.EventHandler(this.deleteOutputButton_Click);
            // 
            // expandImageButton
            // 
            this.expandImageButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(20)))));
            this.expandImageButton.Enabled = false;
            this.expandImageButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.expandImageButton.FlatAppearance.BorderSize = 0;
            this.expandImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.expandImageButton.ForeColor = System.Drawing.Color.White;
            this.expandImageButton.Location = new System.Drawing.Point(1086, 394);
            this.expandImageButton.Name = "expandImageButton";
            this.expandImageButton.Size = new System.Drawing.Size(81, 19);
            this.expandImageButton.TabIndex = 15;
            this.expandImageButton.Text = "Fullscreen";
            this.expandImageButton.UseVisualStyleBackColor = false;
            this.expandImageButton.Click += new System.EventHandler(this.expandImageButton_Click);
            // 
            // loadingPicture
            // 
            this.loadingPicture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.loadingPicture.Image = global::ELA.Properties.Resources.PuGg;
            this.loadingPicture.Location = new System.Drawing.Point(845, 169);
            this.loadingPicture.Name = "loadingPicture";
            this.loadingPicture.Size = new System.Drawing.Size(205, 158);
            this.loadingPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.loadingPicture.TabIndex = 10;
            this.loadingPicture.TabStop = false;
            // 
            // decodedPictureBox
            // 
            this.decodedPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.decodedPictureBox.Location = new System.Drawing.Point(717, 93);
            this.decodedPictureBox.Name = "decodedPictureBox";
            this.decodedPictureBox.Size = new System.Drawing.Size(450, 300);
            this.decodedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.decodedPictureBox.TabIndex = 7;
            this.decodedPictureBox.TabStop = false;
            // 
            // loadedPictureBox
            // 
            this.loadedPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.loadedPictureBox.Location = new System.Drawing.Point(239, 93);
            this.loadedPictureBox.Name = "loadedPictureBox";
            this.loadedPictureBox.Size = new System.Drawing.Size(450, 300);
            this.loadedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loadedPictureBox.TabIndex = 0;
            this.loadedPictureBox.TabStop = false;
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.closeButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(695, 3);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(20, 20);
            this.closeButton.TabIndex = 16;
            this.closeButton.Text = "x";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // saveELACheckbox
            // 
            this.saveELACheckbox.AutoSize = true;
            this.saveELACheckbox.ForeColor = System.Drawing.Color.White;
            this.saveELACheckbox.Location = new System.Drawing.Point(15, 215);
            this.saveELACheckbox.Name = "saveELACheckbox";
            this.saveELACheckbox.Size = new System.Drawing.Size(131, 17);
            this.saveELACheckbox.TabIndex = 17;
            this.saveELACheckbox.Text = "ELA Bild speichern";
            this.saveELACheckbox.UseVisualStyleBackColor = true;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(36)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1197, 425);
            this.Controls.Add(this.saveELACheckbox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.expandImageButton);
            this.Controls.Add(this.deleteOutputButton);
            this.Controls.Add(this.deleteInputButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.saveDecodedImagecheckBox);
            this.Controls.Add(this.SetDecodedAsInputButton);
            this.Controls.Add(this.qualityTextBox);
            this.Controls.Add(this.expandButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.loadingPicture);
            this.Controls.Add(this.saveTxtCheckBox);
            this.Controls.Add(this.chooseImageButton);
            this.Controls.Add(this.contrastTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Kontrast);
            this.Controls.Add(this.decodedPictureBox);
            this.Controls.Add(this.loadedPictureBox);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Turquoise;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form";
            this.Opacity = 0.9D;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.loadingPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.decodedPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadedPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox loadedPictureBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Kontrast;
        private System.Windows.Forms.TextBox contrastTextBox;
        private System.Windows.Forms.CheckBox saveTxtCheckBox;
        private System.Windows.Forms.PictureBox decodedPictureBox;
        private System.Windows.Forms.TextBox qualityTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button chooseImageButton;
        private System.Windows.Forms.CheckBox saveDecodedImagecheckBox;
        private System.Windows.Forms.PictureBox loadingPicture;
        private System.Windows.Forms.Button expandButton;
        private System.Windows.Forms.Button SetDecodedAsInputButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button deleteInputButton;
        private System.Windows.Forms.Button deleteOutputButton;
        private System.Windows.Forms.Button expandImageButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.CheckBox saveELACheckbox;
    }
}

