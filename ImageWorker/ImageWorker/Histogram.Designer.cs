namespace ImageWorker
{
    partial class Histogram
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
            this.picHistogram = new System.Windows.Forms.PictureBox();
            this.picShades = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picHistogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShades)).BeginInit();
            this.SuspendLayout();
            // 
            // picHistogram
            // 
            this.picHistogram.Location = new System.Drawing.Point(12, 12);
            this.picHistogram.Name = "picHistogram";
            this.picHistogram.Size = new System.Drawing.Size(256, 223);
            this.picHistogram.TabIndex = 0;
            this.picHistogram.TabStop = false;
            // 
            // picShades
            // 
            this.picShades.Location = new System.Drawing.Point(12, 241);
            this.picShades.Name = "picShades";
            this.picShades.Size = new System.Drawing.Size(256, 20);
            this.picShades.TabIndex = 1;
            this.picShades.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(193, 267);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Histogram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 302);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.picShades);
            this.Controls.Add(this.picHistogram);
            this.Name = "Histogram";
            this.Text = "Histogram";
            ((System.ComponentModel.ISupportInitialize)(this.picHistogram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShades)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picHistogram;
        private System.Windows.Forms.PictureBox picShades;
        private System.Windows.Forms.Button btnSave;
    }
}