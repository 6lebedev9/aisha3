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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainPanel = new System.Windows.Forms.Panel();
            this.MapPanel = new System.Windows.Forms.Panel();
            this.SortPrefPanel = new System.Windows.Forms.Panel();
            this.SortPanel = new System.Windows.Forms.Panel();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.CommentPanel = new System.Windows.Forms.Panel();
            this.LblProgName = new System.Windows.Forms.Label();
            this.BtnSettings = new System.Windows.Forms.Button();
            this.BtnCollapse = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.ControlPanel.SuspendLayout();
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
            this.ControlPanel.Controls.Add(this.LblProgName);
            this.ControlPanel.Controls.Add(this.BtnSettings);
            this.ControlPanel.Controls.Add(this.BtnCollapse);
            this.ControlPanel.Controls.Add(this.BtnClose);
            this.ControlPanel.Location = new System.Drawing.Point(537, 0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(380, 28);
            this.ControlPanel.TabIndex = 3;
            this.ControlPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlPanel_MouseDown);
            this.ControlPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ControlPanel_MouseMove);
            // 
            // CommentPanel
            // 
            this.CommentPanel.BackColor = System.Drawing.Color.Black;
            this.CommentPanel.Location = new System.Drawing.Point(935, 34);
            this.CommentPanel.Name = "CommentPanel";
            this.CommentPanel.Size = new System.Drawing.Size(413, 80);
            this.CommentPanel.TabIndex = 2;
            // 
            // LblProgName
            // 
            this.LblProgName.AutoSize = true;
            this.LblProgName.Dock = System.Windows.Forms.DockStyle.Left;
            this.LblProgName.Font = new System.Drawing.Font("ГОСТ тип А", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LblProgName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.LblProgName.Location = new System.Drawing.Point(0, 0);
            this.LblProgName.Name = "LblProgName";
            this.LblProgName.Size = new System.Drawing.Size(85, 23);
            this.LblProgName.TabIndex = 3;
            this.LblProgName.Text = "Aisha v3";
            // 
            // BtnSettings
            // 
            this.BtnSettings.AutoEllipsis = true;
            this.BtnSettings.BackgroundImage = global::aisha3.Properties.Resources.settings_x80;
            this.BtnSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSettings.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSettings.Location = new System.Drawing.Point(296, 0);
            this.BtnSettings.Name = "BtnSettings";
            this.BtnSettings.Size = new System.Drawing.Size(28, 28);
            this.BtnSettings.TabIndex = 2;
            this.BtnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnSettings.UseVisualStyleBackColor = true;
            this.BtnSettings.Click += new System.EventHandler(this.BtnSettings_Click);
            // 
            // BtnCollapse
            // 
            this.BtnCollapse.AutoEllipsis = true;
            this.BtnCollapse.BackgroundImage = global::aisha3.Properties.Resources.collapse_x80;
            this.BtnCollapse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCollapse.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnCollapse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCollapse.Location = new System.Drawing.Point(324, 0);
            this.BtnCollapse.Name = "BtnCollapse";
            this.BtnCollapse.Size = new System.Drawing.Size(28, 28);
            this.BtnCollapse.TabIndex = 1;
            this.BtnCollapse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnCollapse.UseVisualStyleBackColor = true;
            this.BtnCollapse.Click += new System.EventHandler(this.BtnCollapse_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.AutoEllipsis = true;
            this.BtnClose.BackgroundImage = global::aisha3.Properties.Resources.close_x80;
            this.BtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Location = new System.Drawing.Point(352, 0);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(28, 28);
            this.BtnClose.TabIndex = 0;
            this.BtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Pink;
            this.ClientSize = new System.Drawing.Size(1352, 747);
            this.ControlBox = false;
            this.Controls.Add(this.CommentPanel);
            this.Controls.Add(this.ControlPanel);
            this.Controls.Add(this.SortPanel);
            this.Controls.Add(this.SortPrefPanel);
            this.Controls.Add(this.MapPanel);
            this.Controls.Add(this.MainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Aisha";
            this.TransparencyKey = System.Drawing.Color.Pink;
            this.ControlPanel.ResumeLayout(false);
            this.ControlPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel MapPanel;
        private System.Windows.Forms.Panel SortPrefPanel;
        private System.Windows.Forms.Panel SortPanel;
        private System.Windows.Forms.Panel ControlPanel;
        private System.Windows.Forms.Panel CommentPanel;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Button BtnCollapse;
        private System.Windows.Forms.Button BtnSettings;
        private System.Windows.Forms.Label LblProgName;
    }
}

