namespace Gitlost_bot
{
    partial class BotMaNiceGuyer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if maNiceGuyed resources should be disposed; otherwise, false.</param>
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
            this.btnStartGitLostBot = new System.Windows.Forms.Button();
            this.txtGitLostLog = new System.Windows.Forms.TextBox();
            this.btnStopGitLostBot = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbPageScraper = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtNiceGuyLog = new System.Windows.Forms.TextBox();
            this.btnStopNiceGuyBot = new System.Windows.Forms.Button();
            this.btnStartNiceGuyBot = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tbPageScraper.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartGitLostBot
            // 
            this.btnStartGitLostBot.Location = new System.Drawing.Point(240, 6);
            this.btnStartGitLostBot.Name = "btnStartGitLostBot";
            this.btnStartGitLostBot.Size = new System.Drawing.Size(100, 23);
            this.btnStartGitLostBot.TabIndex = 0;
            this.btnStartGitLostBot.Text = "Start bot";
            this.btnStartGitLostBot.UseVisualStyleBackColor = true;
            // 
            // txtGitLostLog
            // 
            this.txtGitLostLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGitLostLog.BackColor = System.Drawing.Color.White;
            this.txtGitLostLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGitLostLog.Location = new System.Drawing.Point(6, 35);
            this.txtGitLostLog.Multiline = true;
            this.txtGitLostLog.Name = "txtGitLostLog";
            this.txtGitLostLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGitLostLog.Size = new System.Drawing.Size(660, 350);
            this.txtGitLostLog.TabIndex = 1;
            // 
            // btnStopGitLostBot
            // 
            this.btnStopGitLostBot.Location = new System.Drawing.Point(346, 6);
            this.btnStopGitLostBot.Name = "btnStopGitLostBot";
            this.btnStopGitLostBot.Size = new System.Drawing.Size(100, 23);
            this.btnStopGitLostBot.TabIndex = 0;
            this.btnStopGitLostBot.Text = "Stop bot";
            this.btnStopGitLostBot.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbPageScraper);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(680, 417);
            this.tabControl1.TabIndex = 2;
            // 
            // tbPageScraper
            // 
            this.tbPageScraper.Controls.Add(this.txtGitLostLog);
            this.tbPageScraper.Controls.Add(this.btnStopGitLostBot);
            this.tbPageScraper.Controls.Add(this.btnStartGitLostBot);
            this.tbPageScraper.Location = new System.Drawing.Point(4, 22);
            this.tbPageScraper.Name = "tbPageScraper";
            this.tbPageScraper.Padding = new System.Windows.Forms.Padding(3);
            this.tbPageScraper.Size = new System.Drawing.Size(672, 391);
            this.tbPageScraper.TabIndex = 0;
            this.tbPageScraper.Text = "Gitlost bot";
            this.tbPageScraper.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtNiceGuyLog);
            this.tabPage1.Controls.Add(this.btnStopNiceGuyBot);
            this.tabPage1.Controls.Add(this.btnStartNiceGuyBot);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(672, 391);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "NiceGuy bot";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtNiceGuyLog
            // 
            this.txtNiceGuyLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNiceGuyLog.BackColor = System.Drawing.Color.White;
            this.txtNiceGuyLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNiceGuyLog.Location = new System.Drawing.Point(6, 35);
            this.txtNiceGuyLog.Multiline = true;
            this.txtNiceGuyLog.Name = "txtNiceGuyLog";
            this.txtNiceGuyLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNiceGuyLog.Size = new System.Drawing.Size(660, 350);
            this.txtNiceGuyLog.TabIndex = 4;
            // 
            // btnStopNiceGuyBot
            // 
            this.btnStopNiceGuyBot.Location = new System.Drawing.Point(346, 6);
            this.btnStopNiceGuyBot.Name = "btnStopNiceGuyBot";
            this.btnStopNiceGuyBot.Size = new System.Drawing.Size(100, 23);
            this.btnStopNiceGuyBot.TabIndex = 2;
            this.btnStopNiceGuyBot.Text = "Stop bot";
            this.btnStopNiceGuyBot.UseVisualStyleBackColor = true;
            // 
            // btnStartNiceGuyBot
            // 
            this.btnStartNiceGuyBot.Location = new System.Drawing.Point(240, 6);
            this.btnStartNiceGuyBot.Name = "btnStartNiceGuyBot";
            this.btnStartNiceGuyBot.Size = new System.Drawing.Size(100, 23);
            this.btnStartNiceGuyBot.TabIndex = 3;
            this.btnStartNiceGuyBot.Text = "Start bot";
            this.btnStartNiceGuyBot.UseVisualStyleBackColor = true;
            // 
            // BotMaNiceGuyer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 441);
            this.Controls.Add(this.tabControl1);
            this.Name = "BotMaNiceGuyer";
            this.Text = "BotMaNiceGuyer";
            this.tabControl1.ResumeLayout(false);
            this.tbPageScraper.ResumeLayout(false);
            this.tbPageScraper.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartGitLostBot;
        private System.Windows.Forms.TextBox txtGitLostLog;
        private System.Windows.Forms.Button btnStopGitLostBot;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbPageScraper;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtNiceGuyLog;
        private System.Windows.Forms.Button btnStopNiceGuyBot;
        private System.Windows.Forms.Button btnStartNiceGuyBot;
    }
}

