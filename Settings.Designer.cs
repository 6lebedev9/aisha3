namespace aisha3
{
    partial class Settings
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.FormBorderColorNumR = new System.Windows.Forms.NumericUpDown();
            this.FormBorderColorNumG = new System.Windows.Forms.NumericUpDown();
            this.FormBorderColorNumB = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormBorderColorNumR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormBorderColorNumG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormBorderColorNumB)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.FormBorderColorNumB);
            this.panel1.Controls.Add(this.FormBorderColorNumG);
            this.panel1.Controls.Add(this.FormBorderColorNumR);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(909, 495);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "FormBorderColor = R         G         B";
            // 
            // FormBorderColorNumR
            // 
            this.FormBorderColorNumR.Location = new System.Drawing.Point(143, 3);
            this.FormBorderColorNumR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.FormBorderColorNumR.Name = "FormBorderColorNumR";
            this.FormBorderColorNumR.Size = new System.Drawing.Size(41, 22);
            this.FormBorderColorNumR.TabIndex = 1;
            this.FormBorderColorNumR.ValueChanged += new System.EventHandler(this.FormBorderColorNumR_ValueChanged);
            // 
            // FormBorderColorNumG
            // 
            this.FormBorderColorNumG.Location = new System.Drawing.Point(203, 3);
            this.FormBorderColorNumG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.FormBorderColorNumG.Name = "FormBorderColorNumG";
            this.FormBorderColorNumG.Size = new System.Drawing.Size(41, 22);
            this.FormBorderColorNumG.TabIndex = 2;
            this.FormBorderColorNumG.ValueChanged += new System.EventHandler(this.FormBorderColorNumG_ValueChanged);
            // 
            // FormBorderColorNumB
            // 
            this.FormBorderColorNumB.Location = new System.Drawing.Point(267, 3);
            this.FormBorderColorNumB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.FormBorderColorNumB.Name = "FormBorderColorNumB";
            this.FormBorderColorNumB.Size = new System.Drawing.Size(41, 22);
            this.FormBorderColorNumB.TabIndex = 3;
            this.FormBorderColorNumB.ValueChanged += new System.EventHandler(this.FormBorderColorNumB_ValueChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("GOST type B", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Settings";
            this.ShowIcon = false;
            this.Text = "Aisha v3 - Настройки";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormBorderColorNumR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormBorderColorNumG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormBorderColorNumB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown FormBorderColorNumB;
        private System.Windows.Forms.NumericUpDown FormBorderColorNumG;
        private System.Windows.Forms.NumericUpDown FormBorderColorNumR;
    }
}