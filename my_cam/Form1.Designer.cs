namespace my_cam
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.run_btn = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.GoToDir_Btn = new System.Windows.Forms.Button();
            this.isNeedAudio_Checkbox = new System.Windows.Forms.CheckBox();
            this.HelpMe_Btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // run_btn
            // 
            this.run_btn.Location = new System.Drawing.Point(12, 34);
            this.run_btn.Name = "run_btn";
            this.run_btn.Size = new System.Drawing.Size(106, 78);
            this.run_btn.TabIndex = 0;
            this.run_btn.Text = "開始錄影 (F2)";
            this.run_btn.UseVisualStyleBackColor = true;
            this.run_btn.Click += new System.EventHandler(this.Run_Btn_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseDoubleClick);
            // 
            // GoToDir_Btn
            // 
            this.GoToDir_Btn.Location = new System.Drawing.Point(124, 12);
            this.GoToDir_Btn.Name = "GoToDir_Btn";
            this.GoToDir_Btn.Size = new System.Drawing.Size(116, 101);
            this.GoToDir_Btn.TabIndex = 1;
            this.GoToDir_Btn.Text = "前往輸出目錄 (F3)";
            this.GoToDir_Btn.UseVisualStyleBackColor = true;
            this.GoToDir_Btn.Click += new System.EventHandler(this.GoToDir_Btn_Click);
            // 
            // isNeedAudio_Checkbox
            // 
            this.isNeedAudio_Checkbox.AutoSize = true;
            this.isNeedAudio_Checkbox.Checked = true;
            this.isNeedAudio_Checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isNeedAudio_Checkbox.Location = new System.Drawing.Point(12, 12);
            this.isNeedAudio_Checkbox.Name = "isNeedAudio_Checkbox";
            this.isNeedAudio_Checkbox.Size = new System.Drawing.Size(84, 16);
            this.isNeedAudio_Checkbox.TabIndex = 2;
            this.isNeedAudio_Checkbox.Text = "要錄聲音嗎";
            this.isNeedAudio_Checkbox.UseVisualStyleBackColor = true;
            // 
            // HelpMe_Btn
            // 
            this.HelpMe_Btn.Location = new System.Drawing.Point(247, 13);
            this.HelpMe_Btn.Name = "HelpMe_Btn";
            this.HelpMe_Btn.Size = new System.Drawing.Size(109, 100);
            this.HelpMe_Btn.TabIndex = 3;
            this.HelpMe_Btn.Text = "說明 (F8)";
            this.HelpMe_Btn.UseVisualStyleBackColor = true;
            this.HelpMe_Btn.Click += new System.EventHandler(this.HelpMe_Btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 121);
            this.Controls.Add(this.HelpMe_Btn);
            this.Controls.Add(this.isNeedAudio_Checkbox);
            this.Controls.Add(this.GoToDir_Btn);
            this.Controls.Add(this.run_btn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "螢幕錄影機 - 3WA";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button run_btn;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button GoToDir_Btn;
        private System.Windows.Forms.CheckBox isNeedAudio_Checkbox;
        private System.Windows.Forms.Button HelpMe_Btn;
    }
}

