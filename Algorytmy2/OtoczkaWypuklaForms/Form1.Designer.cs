namespace OtoczkaWypuklaForms
{
    partial class Form1
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
            this.wyczyscButton = new System.Windows.Forms.Button();
            this.otoczkaButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // wyczyscButton
            // 
            this.wyczyscButton.Location = new System.Drawing.Point(13, 406);
            this.wyczyscButton.Name = "wyczyscButton";
            this.wyczyscButton.Size = new System.Drawing.Size(75, 23);
            this.wyczyscButton.TabIndex = 0;
            this.wyczyscButton.Text = "Wyczysc";
            this.wyczyscButton.UseVisualStyleBackColor = true;
            this.wyczyscButton.Click += new System.EventHandler(this.wyczyscButton_Click);
            // 
            // otoczkaButton
            // 
            this.otoczkaButton.Location = new System.Drawing.Point(95, 405);
            this.otoczkaButton.Name = "otoczkaButton";
            this.otoczkaButton.Size = new System.Drawing.Size(75, 23);
            this.otoczkaButton.TabIndex = 1;
            this.otoczkaButton.Text = "Otoczka";
            this.otoczkaButton.UseVisualStyleBackColor = true;
            this.otoczkaButton.Click += new System.EventHandler(this.otoczkaButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 441);
            this.Controls.Add(this.otoczkaButton);
            this.Controls.Add(this.wyczyscButton);
            this.Name = "Form1";
            this.Text = "Otoczka wypukla";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button wyczyscButton;
        private System.Windows.Forms.Button otoczkaButton;
    }
}

