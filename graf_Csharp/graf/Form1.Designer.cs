namespace graf
{
    partial class grafForm
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
            this.grafPanel = new System.Windows.Forms.Panel();
            this.btnAddGraph = new System.Windows.Forms.Button();
            this.txtNodes = new System.Windows.Forms.TextBox();
            this.btnKruskal = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblN = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPrim = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblGraphCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.colorNode = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.colorLine = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.grpTest = new System.Windows.Forms.GroupBox();
            this.lblPainting = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblMem = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // grafPanel
            // 
            this.grafPanel.BackColor = System.Drawing.Color.White;
            this.grafPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.grafPanel.Location = new System.Drawing.Point(2, 35);
            this.grafPanel.Name = "grafPanel";
            this.grafPanel.Size = new System.Drawing.Size(500, 500);
            this.grafPanel.TabIndex = 0;
            this.grafPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.grafPanel_Paint);
            // 
            // btnAddGraph
            // 
            this.btnAddGraph.Location = new System.Drawing.Point(183, 90);
            this.btnAddGraph.Name = "btnAddGraph";
            this.btnAddGraph.Size = new System.Drawing.Size(67, 23);
            this.btnAddGraph.TabIndex = 2;
            this.btnAddGraph.Text = "Dodaj ";
            this.btnAddGraph.UseVisualStyleBackColor = true;
            this.btnAddGraph.Click += new System.EventHandler(this.btnAddGraph_Click);
            // 
            // txtNodes
            // 
            this.txtNodes.Location = new System.Drawing.Point(114, 16);
            this.txtNodes.Name = "txtNodes";
            this.txtNodes.Size = new System.Drawing.Size(136, 20);
            this.txtNodes.TabIndex = 3;
            this.txtNodes.Text = "3";
            this.txtNodes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNodes_KeyPress);
            // 
            // btnKruskal
            // 
            this.btnKruskal.Enabled = false;
            this.btnKruskal.Location = new System.Drawing.Point(7, 19);
            this.btnKruskal.Name = "btnKruskal";
            this.btnKruskal.Size = new System.Drawing.Size(124, 23);
            this.btnKruskal.TabIndex = 4;
            this.btnKruskal.Text = " Kruskala";
            this.btnKruskal.UseVisualStyleBackColor = true;
            this.btnKruskal.Click += new System.EventHandler(this.btnKruskal_Click);
            // 
            // lblError
            // 
            this.lblError.Location = new System.Drawing.Point(-2, 1);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(823, 31);
            this.lblError.TabIndex = 5;
            this.lblError.Text = "Witaj w Programie!";
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(518, 35);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(131, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "  Wyczyść";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblN
            // 
            this.lblN.Location = new System.Drawing.Point(8, 19);
            this.lblN.Name = "lblN";
            this.lblN.Size = new System.Drawing.Size(100, 17);
            this.lblN.TabIndex = 7;
            this.lblN.Text = "ilość wierzchołków:";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(655, 35);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(133, 23);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "Wyjdź";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPrim);
            this.groupBox1.Controls.Add(this.btnKruskal);
            this.groupBox1.Location = new System.Drawing.Point(518, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 65);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Wyznacz MST Algorytmem:";
            // 
            // btnPrim
            // 
            this.btnPrim.Enabled = false;
            this.btnPrim.Location = new System.Drawing.Point(137, 19);
            this.btnPrim.Name = "btnPrim";
            this.btnPrim.Size = new System.Drawing.Size(118, 23);
            this.btnPrim.TabIndex = 5;
            this.btnPrim.Text = "Prima-Djikstry";
            this.btnPrim.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblGraphCount);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.colorNode);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.colorLine);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblN);
            this.groupBox2.Controls.Add(this.btnAddGraph);
            this.groupBox2.Controls.Add(this.txtNodes);
            this.groupBox2.Location = new System.Drawing.Point(518, 165);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 149);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Nowy Graf:";
            // 
            // lblGraphCount
            // 
            this.lblGraphCount.AutoSize = true;
            this.lblGraphCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblGraphCount.Location = new System.Drawing.Point(132, 124);
            this.lblGraphCount.Name = "lblGraphCount";
            this.lblGraphCount.Size = new System.Drawing.Size(16, 16);
            this.lblGraphCount.TabIndex = 13;
            this.lblGraphCount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Obecna ilość grafów:";
            // 
            // colorNode
            // 
            this.colorNode.BackColor = System.Drawing.Color.Red;
            this.colorNode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorNode.Location = new System.Drawing.Point(114, 68);
            this.colorNode.Name = "colorNode";
            this.colorNode.Size = new System.Drawing.Size(136, 16);
            this.colorNode.TabIndex = 11;
            this.colorNode.Click += new System.EventHandler(this.colorNode_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "kolor wierzchołków:";
            // 
            // colorLine
            // 
            this.colorLine.BackColor = System.Drawing.Color.Navy;
            this.colorLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorLine.Location = new System.Drawing.Point(114, 46);
            this.colorLine.Name = "colorLine";
            this.colorLine.Size = new System.Drawing.Size(136, 16);
            this.colorLine.TabIndex = 9;
            this.colorLine.Click += new System.EventHandler(this.colorLine_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "kolor krawędzi:";
            // 
            // grpTest
            // 
            this.grpTest.Controls.Add(this.lblPainting);
            this.grpTest.Controls.Add(this.label9);
            this.grpTest.Controls.Add(this.label8);
            this.grpTest.Controls.Add(this.label7);
            this.grpTest.Controls.Add(this.lblMem);
            this.grpTest.Controls.Add(this.label6);
            this.grpTest.Controls.Add(this.lblTime);
            this.grpTest.Controls.Add(this.label5);
            this.grpTest.Controls.Add(this.label4);
            this.grpTest.Location = new System.Drawing.Point(518, 349);
            this.grpTest.Name = "grpTest";
            this.grpTest.Size = new System.Drawing.Size(270, 135);
            this.grpTest.TabIndex = 11;
            this.grpTest.TabStop = false;
            this.grpTest.Text = "Testy";
            // 
            // lblPainting
            // 
            this.lblPainting.AutoSize = true;
            this.lblPainting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPainting.Location = new System.Drawing.Point(133, 56);
            this.lblPainting.Name = "lblPainting";
            this.lblPainting.Size = new System.Drawing.Size(15, 15);
            this.lblPainting.TabIndex = 8;
            this.lblPainting.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(238, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "s";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(44, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Czas rysowania:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(238, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "KB";
            // 
            // lblMem
            // 
            this.lblMem.AutoSize = true;
            this.lblMem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblMem.Location = new System.Drawing.Point(133, 91);
            this.lblMem.Name = "lblMem";
            this.lblMem.Size = new System.Drawing.Size(15, 15);
            this.lblMem.TabIndex = 4;
            this.lblMem.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(238, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "s";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTime.Location = new System.Drawing.Point(133, 27);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(15, 15);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Wielkość zajętej pamięci:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Czas generacji MST:";
            // 
            // grafForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 547);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.grpTest);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.grafPanel);
            this.Name = "grafForm";
            this.Text = "Analiza algorytmu Kruskala";
            this.Load += new System.EventHandler(this.grafForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.grafForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpTest.ResumeLayout(false);
            this.grpTest.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel grafPanel;
        private System.Windows.Forms.Button btnAddGraph;
        private System.Windows.Forms.TextBox txtNodes;
        private System.Windows.Forms.Button btnKruskal;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblN;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPrim;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel colorLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel colorNode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblGraphCount;
        private System.Windows.Forms.GroupBox grpTest;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblMem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPainting;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
    }
}

