namespace aisha3
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.MapPanel = new System.Windows.Forms.Panel();
            this.SortPrefPanel = new System.Windows.Forms.Panel();
            this.SortPanel = new System.Windows.Forms.Panel();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.CommentPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.Black;
            this.MainPanel.Location = new System.Drawing.Point(537, 34);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(380, 709);
            this.MainPanel.TabIndex = 0;
            // 
            // MapPanel
            // 
            this.MapPanel.BackColor = System.Drawing.Color.Black;
            this.MapPanel.Location = new System.Drawing.Point(935, 120);
            this.MapPanel.Name = "MapPanel";
            this.MapPanel.Size = new System.Drawing.Size(413, 623);
            this.MapPanel.TabIndex = 1;
            // 
            // SortPrefPanel
            // 
            this.SortPrefPanel.BackColor = System.Drawing.Color.Black;
            this.SortPrefPanel.Location = new System.Drawing.Point(389, 34);
            this.SortPrefPanel.Name = "SortPrefPanel";
            this.SortPrefPanel.Size = new System.Drawing.Size(131, 709);
            this.SortPrefPanel.TabIndex = 2;
            // 
            // SortPanel
            // 
            this.SortPanel.BackColor = System.Drawing.Color.Black;
            this.SortPanel.Location = new System.Drawing.Point(12, 34);
            this.SortPanel.Name = "SortPanel";
            this.SortPanel.Size = new System.Drawing.Size(361, 709);
            this.SortPanel.TabIndex = 3;
            // 
            // ControlPanel
            // 
            this.ControlPanel.BackColor = System.Drawing.Color.Black;
            this.ControlPanel.Location = new System.Drawing.Point(537, 0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(380, 28);
            this.ControlPanel.TabIndex = 3;
            // 
            // CommentPanel
            // 
            this.CommentPanel.BackColor = System.Drawing.Color.Black;
            this.CommentPanel.Location = new System.Drawing.Point(935, 34);
            this.CommentPanel.Name = "CommentPanel";
            this.CommentPanel.Size = new System.Drawing.Size(413, 80);
            this.CommentPanel.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Pink;
            this.ClientSize = new System.Drawing.Size(1360, 755);
            this.ControlBox = false;
            this.Controls.Add(this.CommentPanel);
            this.Controls.Add(this.ControlPanel);
            this.Controls.Add(this.SortPanel);
            this.Controls.Add(this.SortPrefPanel);
            this.Controls.Add(this.MapPanel);
            this.Controls.Add(this.MainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Aisha";
            this.TransparencyKey = System.Drawing.Color.Pink;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel MapPanel;
        private System.Windows.Forms.Panel SortPrefPanel;
        private System.Windows.Forms.Panel SortPanel;
        private System.Windows.Forms.Panel ControlPanel;
        private System.Windows.Forms.Panel CommentPanel;
    }
}

