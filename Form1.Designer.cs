namespace FileCompare
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblAppName = new System.Windows.Forms.Label();
            this.btnCopyFromLeft = new System.Windows.Forms.Button();
            this.btnCopyFromRight = new System.Windows.Forms.Button();
            this.txtLeftDir = new System.Windows.Forms.TextBox();
            this.btnLeftDir = new System.Windows.Forms.Button();
            this.btnRightDir = new System.Windows.Forms.Button();
            this.txtRightDir = new System.Windows.Forms.TextBox();
            this.lvwLeftDir = new System.Windows.Forms.ListView();
            this.lvwRightDir = new System.Windows.Forms.ListView();
            this.name_left = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.size_left = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.수정일 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name_right = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.size_right = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel6);
            this.splitContainer1.Panel2.Controls.Add(this.panel5);
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.splitContainer1.Size = new System.Drawing.Size(1091, 569);
            this.splitContainer1.SplitterDistance = 536;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCopyFromLeft);
            this.panel1.Controls.Add(this.lblAppName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(526, 100);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.btnLeftDir);
            this.panel2.Controls.Add(this.txtLeftDir);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(5, 105);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(526, 69);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.lvwLeftDir);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(5, 174);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(526, 390);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.Controls.Add(this.btnCopyFromRight);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(5, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(541, 100);
            this.panel4.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Control;
            this.panel5.Controls.Add(this.btnRightDir);
            this.panel5.Controls.Add(this.txtRightDir);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(5, 105);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(541, 69);
            this.panel5.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Control;
            this.panel6.Controls.Add(this.lvwRightDir);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(5, 174);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(541, 390);
            this.panel6.TabIndex = 2;
            // 
            // lblAppName
            // 
            this.lblAppName.AutoSize = true;
            this.lblAppName.Font = new System.Drawing.Font("굴림", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblAppName.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.lblAppName.Location = new System.Drawing.Point(3, 4);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(307, 44);
            this.lblAppName.TabIndex = 0;
            this.lblAppName.Text = "File Compare";
            // 
            // btnCopyFromLeft
            // 
            this.btnCopyFromLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyFromLeft.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCopyFromLeft.Location = new System.Drawing.Point(400, 29);
            this.btnCopyFromLeft.Name = "btnCopyFromLeft";
            this.btnCopyFromLeft.Size = new System.Drawing.Size(101, 56);
            this.btnCopyFromLeft.TabIndex = 1;
            this.btnCopyFromLeft.Text = ">>>";
            this.btnCopyFromLeft.UseVisualStyleBackColor = true;
            // 
            // btnCopyFromRight
            // 
            this.btnCopyFromRight.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCopyFromRight.Location = new System.Drawing.Point(16, 29);
            this.btnCopyFromRight.Name = "btnCopyFromRight";
            this.btnCopyFromRight.Size = new System.Drawing.Size(101, 56);
            this.btnCopyFromRight.TabIndex = 2;
            this.btnCopyFromRight.Text = "<<<";
            this.btnCopyFromRight.UseVisualStyleBackColor = true;
            // 
            // txtLeftDir
            // 
            this.txtLeftDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLeftDir.Location = new System.Drawing.Point(15, 29);
            this.txtLeftDir.Name = "txtLeftDir";
            this.txtLeftDir.Size = new System.Drawing.Size(379, 25);
            this.txtLeftDir.TabIndex = 0;
            // 
            // btnLeftDir
            // 
            this.btnLeftDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLeftDir.Location = new System.Drawing.Point(400, 22);
            this.btnLeftDir.Name = "btnLeftDir";
            this.btnLeftDir.Size = new System.Drawing.Size(83, 32);
            this.btnLeftDir.TabIndex = 1;
            this.btnLeftDir.Text = "폴더선택";
            this.btnLeftDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLeftDir.UseVisualStyleBackColor = true;
            this.btnLeftDir.Click += new System.EventHandler(this.btnLeftDir_Click);
            // 
            // btnRightDir
            // 
            this.btnRightDir.Location = new System.Drawing.Point(431, 22);
            this.btnRightDir.Name = "btnRightDir";
            this.btnRightDir.Size = new System.Drawing.Size(83, 32);
            this.btnRightDir.TabIndex = 3;
            this.btnRightDir.Text = "폴더선택";
            this.btnRightDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRightDir.UseVisualStyleBackColor = true;
            this.btnRightDir.Click += new System.EventHandler(this.btnRightDir_Click);
            // 
            // txtRightDir
            // 
            this.txtRightDir.Location = new System.Drawing.Point(39, 29);
            this.txtRightDir.Name = "txtRightDir";
            this.txtRightDir.Size = new System.Drawing.Size(379, 25);
            this.txtRightDir.TabIndex = 2;
            // 
            // lvwLeftDir
            // 
            this.lvwLeftDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwLeftDir.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name_left,
            this.size_left,
            this.수정일});
            this.lvwLeftDir.FullRowSelect = true;
            this.lvwLeftDir.GridLines = true;
            this.lvwLeftDir.HideSelection = false;
            this.lvwLeftDir.Location = new System.Drawing.Point(8, 18);
            this.lvwLeftDir.Name = "lvwLeftDir";
            this.lvwLeftDir.Size = new System.Drawing.Size(475, 365);
            this.lvwLeftDir.TabIndex = 0;
            this.lvwLeftDir.UseCompatibleStateImageBehavior = false;
            this.lvwLeftDir.View = System.Windows.Forms.View.Details;
            // 
            // lvwRightDir
            // 
            this.lvwRightDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvwRightDir.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name_right,
            this.size_right,
            this.columnHeader1});
            this.lvwRightDir.FullRowSelect = true;
            this.lvwRightDir.GridLines = true;
            this.lvwRightDir.HideSelection = false;
            this.lvwRightDir.Location = new System.Drawing.Point(39, 18);
            this.lvwRightDir.Name = "lvwRightDir";
            this.lvwRightDir.Size = new System.Drawing.Size(475, 365);
            this.lvwRightDir.TabIndex = 1;
            this.lvwRightDir.UseCompatibleStateImageBehavior = false;
            this.lvwRightDir.View = System.Windows.Forms.View.Details;
            // 
            // name_left
            // 
            this.name_left.Text = "이름";
            this.name_left.Width = 300;
            // 
            // size_left
            // 
            this.size_left.Text = "크기";
            this.size_left.Width = 100;
            // 
            // 수정일
            // 
            this.수정일.Text = "수정일";
            this.수정일.Width = 160;
            // 
            // name_right
            // 
            this.name_right.Text = "이름";
            this.name_right.Width = 300;
            // 
            // size_right
            // 
            this.size_right.Text = "크기";
            this.size_right.Width = 100;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "수정일";
            this.columnHeader1.Width = 160;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 569);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "File Compare v1.0";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnLeftDir;
        private System.Windows.Forms.TextBox txtLeftDir;
        private System.Windows.Forms.Button btnCopyFromLeft;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Button btnRightDir;
        private System.Windows.Forms.TextBox txtRightDir;
        private System.Windows.Forms.Button btnCopyFromRight;
        private System.Windows.Forms.ListView lvwLeftDir;
        private System.Windows.Forms.ListView lvwRightDir;
        private System.Windows.Forms.ColumnHeader name_left;
        private System.Windows.Forms.ColumnHeader size_left;
        private System.Windows.Forms.ColumnHeader 수정일;
        private System.Windows.Forms.ColumnHeader name_right;
        private System.Windows.Forms.ColumnHeader size_right;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}

