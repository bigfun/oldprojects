namespace ImageWorker
{
    partial class NormInput
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
            this.numA = new System.Windows.Forms.NumericUpDown();
            this.numB = new System.Windows.Forms.NumericUpDown();
            this.numD = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.numMaxVal = new System.Windows.Forms.NumericUpDown();
            this.numMinVal = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinVal)).BeginInit();
            this.SuspendLayout();
            // 
            // numA
            // 
            this.numA.DecimalPlaces = 2;
            this.numA.Location = new System.Drawing.Point(95, 27);
            this.numA.Name = "numA";
            this.numA.Size = new System.Drawing.Size(120, 20);
            this.numA.TabIndex = 0;
            this.numA.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // numB
            // 
            this.numB.Location = new System.Drawing.Point(95, 63);
            this.numB.Name = "numB";
            this.numB.Size = new System.Drawing.Size(120, 20);
            this.numB.TabIndex = 1;
            // 
            // numD
            // 
            this.numD.DecimalPlaces = 2;
            this.numD.Location = new System.Drawing.Point(95, 101);
            this.numD.Name = "numD";
            this.numD.Size = new System.Drawing.Size(120, 20);
            this.numD.TabIndex = 2;
            this.numD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "a:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "b:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "d:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Cout = a*Cin^d + b";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(199, 213);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numMaxVal
            // 
            this.numMaxVal.Location = new System.Drawing.Point(95, 138);
            this.numMaxVal.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numMaxVal.Name = "numMaxVal";
            this.numMaxVal.Size = new System.Drawing.Size(120, 20);
            this.numMaxVal.TabIndex = 8;
            this.numMaxVal.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // numMinVal
            // 
            this.numMinVal.Location = new System.Drawing.Point(95, 174);
            this.numMinVal.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numMinVal.Name = "numMinVal";
            this.numMinVal.Size = new System.Drawing.Size(120, 20);
            this.numMinVal.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Max Value:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Min Value:";
            // 
            // NormInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 266);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numMinVal);
            this.Controls.Add(this.numMaxVal);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numD);
            this.Controls.Add(this.numB);
            this.Controls.Add(this.numA);
            this.Name = "NormInput";
            this.Text = "NormInput";
            ((System.ComponentModel.ISupportInitialize)(this.numA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinVal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numA;
        private System.Windows.Forms.NumericUpDown numB;
        private System.Windows.Forms.NumericUpDown numD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numMaxVal;
        private System.Windows.Forms.NumericUpDown numMinVal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}