namespace DiffBackupRestoreWF
{
    partial class BusyForm
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
            this.BusyLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BusyLabel
            // 
            this.BusyLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BusyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BusyLabel.Location = new System.Drawing.Point(0, 0);
            this.BusyLabel.Margin = new System.Windows.Forms.Padding(5);
            this.BusyLabel.Name = "BusyLabel";
            this.BusyLabel.Size = new System.Drawing.Size(389, 131);
            this.BusyLabel.TabIndex = 0;
            this.BusyLabel.Text = "BusyText";
            this.BusyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BusyLabel.UseWaitCursor = true;
            // 
            // BusyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 131);
            this.Controls.Add(this.BusyLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BusyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BusyForm";
            this.UseWaitCursor = true;
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label BusyLabel;
    }
}