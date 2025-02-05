namespace DiffBackupRestoreWF
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CloseConnButton = new System.Windows.Forms.Button();
            this.TestConnButton = new System.Windows.Forms.Button();
            this.Password = new System.Windows.Forms.TextBox();
            this.UserName = new System.Windows.Forms.TextBox();
            this.ServerName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FilePathTextBox = new System.Windows.Forms.TextBox();
            this.SelectFolderButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.ExecuteRecoveryButton = new System.Windows.Forms.Button();
            this.BackupInfoDataGridView = new System.Windows.Forms.DataGridView();
            this.DatabaseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BackupDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HelpButton = new System.Windows.Forms.Button();
            this.ProgressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.BottomProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BackupInfoDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CloseConnButton);
            this.groupBox1.Controls.Add(this.TestConnButton);
            this.groupBox1.Controls.Add(this.Password);
            this.groupBox1.Controls.Add(this.UserName);
            this.groupBox1.Controls.Add(this.ServerName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(563, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 376);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры для подключения";
            // 
            // CloseConnButton
            // 
            this.CloseConnButton.Location = new System.Drawing.Point(25, 236);
            this.CloseConnButton.Name = "CloseConnButton";
            this.CloseConnButton.Size = new System.Drawing.Size(178, 23);
            this.CloseConnButton.TabIndex = 2;
            this.CloseConnButton.Text = "Разорвать соединение";
            this.CloseConnButton.UseVisualStyleBackColor = true;
            this.CloseConnButton.Visible = false;
            this.CloseConnButton.Click += new System.EventHandler(this.CloseConnButton_Click);
            // 
            // TestConnButton
            // 
            this.TestConnButton.Location = new System.Drawing.Point(25, 207);
            this.TestConnButton.Name = "TestConnButton";
            this.TestConnButton.Size = new System.Drawing.Size(178, 23);
            this.TestConnButton.TabIndex = 2;
            this.TestConnButton.Text = "Установить соединение";
            this.TestConnButton.UseVisualStyleBackColor = true;
            this.TestConnButton.Click += new System.EventHandler(this.TestConnButton_Click);
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(25, 157);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(178, 20);
            this.Password.TabIndex = 1;
            this.Password.UseSystemPasswordChar = true;
            this.Password.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Password_KeyUp);
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(25, 111);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(178, 20);
            this.UserName.TabIndex = 1;
            // 
            // ServerName
            // 
            this.ServerName.Location = new System.Drawing.Point(25, 53);
            this.ServerName.Name = "ServerName";
            this.ServerName.Size = new System.Drawing.Size(178, 20);
            this.ServerName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Пароль";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Имя пользователя";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Сервер \\ экземпляр";
            // 
            // FilePathTextBox
            // 
            this.FilePathTextBox.Location = new System.Drawing.Point(12, 23);
            this.FilePathTextBox.Name = "FilePathTextBox";
            this.FilePathTextBox.ReadOnly = true;
            this.FilePathTextBox.Size = new System.Drawing.Size(489, 20);
            this.FilePathTextBox.TabIndex = 1;
            // 
            // SelectFolderButton
            // 
            this.SelectFolderButton.Enabled = false;
            this.SelectFolderButton.Location = new System.Drawing.Point(507, 23);
            this.SelectFolderButton.Name = "SelectFolderButton";
            this.SelectFolderButton.Size = new System.Drawing.Size(36, 20);
            this.SelectFolderButton.TabIndex = 2;
            this.SelectFolderButton.Text = "...";
            this.SelectFolderButton.UseVisualStyleBackColor = true;
            this.SelectFolderButton.Click += new System.EventHandler(this.SelectFolderButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.StatusMessage,
            this.ProgressLabel,
            this.BottomProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(380, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(77, 17);
            this.toolStripStatusLabel1.Text = "Соединение:";
            // 
            // StatusMessage
            // 
            this.StatusMessage.Name = "StatusMessage";
            this.StatusMessage.Size = new System.Drawing.Size(93, 17);
            this.StatusMessage.Text = "не установлено";
            // 
            // ExecuteRecoveryButton
            // 
            this.ExecuteRecoveryButton.Enabled = false;
            this.ExecuteRecoveryButton.Location = new System.Drawing.Point(563, 421);
            this.ExecuteRecoveryButton.Name = "ExecuteRecoveryButton";
            this.ExecuteRecoveryButton.Size = new System.Drawing.Size(225, 29);
            this.ExecuteRecoveryButton.TabIndex = 5;
            this.ExecuteRecoveryButton.Text = "Восстановить";
            this.ExecuteRecoveryButton.UseVisualStyleBackColor = true;
            this.ExecuteRecoveryButton.Click += new System.EventHandler(this.ExecuteRecoveryButton_Click);
            // 
            // BackupInfoDataGridView
            // 
            this.BackupInfoDataGridView.AllowUserToAddRows = false;
            this.BackupInfoDataGridView.AllowUserToDeleteRows = false;
            this.BackupInfoDataGridView.AllowUserToResizeRows = false;
            this.BackupInfoDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BackupInfoDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DatabaseName,
            this.FileName,
            this.BackupDate});
            this.BackupInfoDataGridView.Location = new System.Drawing.Point(12, 50);
            this.BackupInfoDataGridView.MultiSelect = false;
            this.BackupInfoDataGridView.Name = "BackupInfoDataGridView";
            this.BackupInfoDataGridView.ReadOnly = true;
            this.BackupInfoDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.BackupInfoDataGridView.RowHeadersVisible = false;
            this.BackupInfoDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.BackupInfoDataGridView.Size = new System.Drawing.Size(531, 372);
            this.BackupInfoDataGridView.TabIndex = 6;
            this.BackupInfoDataGridView.SelectionChanged += new System.EventHandler(this.BackupInfoDataGridView_SelectionChanged);
            // 
            // DatabaseName
            // 
            this.DatabaseName.DataPropertyName = "DatabaseName";
            this.DatabaseName.HeaderText = "База данных";
            this.DatabaseName.Name = "DatabaseName";
            this.DatabaseName.ReadOnly = true;
            // 
            // FileName
            // 
            this.FileName.DataPropertyName = "FileName";
            this.FileName.HeaderText = "Файл";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            // 
            // BackupDate
            // 
            this.BackupDate.DataPropertyName = "BackupDate";
            this.BackupDate.HeaderText = "Время";
            this.BackupDate.Name = "BackupDate";
            this.BackupDate.ReadOnly = true;
            // 
            // HelpButton
            // 
            this.HelpButton.Location = new System.Drawing.Point(456, 424);
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(87, 22);
            this.HelpButton.TabIndex = 7;
            this.HelpButton.Text = "Справка";
            this.HelpButton.UseVisualStyleBackColor = true;
            this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(60, 17);
            this.ProgressLabel.Text = "Прогресс";
            // 
            // BottomProgressBar
            // 
            this.BottomProgressBar.Name = "BottomProgressBar";
            this.BottomProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.HelpButton);
            this.Controls.Add(this.BackupInfoDataGridView);
            this.Controls.Add(this.ExecuteRecoveryButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.SelectFolderButton);
            this.Controls.Add(this.FilePathTextBox);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Восстановление бэкапа";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BackupInfoDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox FilePathTextBox;
        private System.Windows.Forms.Button SelectFolderButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel StatusMessage;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.TextBox ServerName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button TestConnButton;
        private System.Windows.Forms.Button ExecuteRecoveryButton;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button CloseConnButton;
        private System.Windows.Forms.DataGridView BackupInfoDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatabaseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BackupDate;
        private System.Windows.Forms.Button HelpButton;
        private System.Windows.Forms.ToolStripStatusLabel ProgressLabel;
        private System.Windows.Forms.ToolStripProgressBar BottomProgressBar;
    }
}

