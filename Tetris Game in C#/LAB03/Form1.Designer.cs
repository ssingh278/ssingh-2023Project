namespace LAB03
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.UI_Timer = new System.Windows.Forms.Timer(this.components);
            this.UI_STart_Btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.UI_Hard_Rbn = new System.Windows.Forms.RadioButton();
            this.UI_Medium_Rbn = new System.Windows.Forms.RadioButton();
            this.UI_Easy_Rbn = new System.Windows.Forms.RadioButton();
            this.UI_Pause_btn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UI_Timer
            // 
            this.UI_Timer.Interval = 200;
            this.UI_Timer.Tick += new System.EventHandler(this.UI_Timer_Tick);
            // 
            // UI_STart_Btn
            // 
            this.UI_STart_Btn.Location = new System.Drawing.Point(12, 12);
            this.UI_STart_Btn.Name = "UI_STart_Btn";
            this.UI_STart_Btn.Size = new System.Drawing.Size(141, 37);
            this.UI_STart_Btn.TabIndex = 0;
            this.UI_STart_Btn.Text = "New Game";
            this.UI_STart_Btn.UseVisualStyleBackColor = true;
            this.UI_STart_Btn.Click += new System.EventHandler(this.UI_STart_Btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UI_Hard_Rbn);
            this.groupBox1.Controls.Add(this.UI_Medium_Rbn);
            this.groupBox1.Controls.Add(this.UI_Easy_Rbn);
            this.groupBox1.Location = new System.Drawing.Point(69, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Difficulty";
            // 
            // UI_Hard_Rbn
            // 
            this.UI_Hard_Rbn.AutoSize = true;
            this.UI_Hard_Rbn.Location = new System.Drawing.Point(7, 65);
            this.UI_Hard_Rbn.Name = "UI_Hard_Rbn";
            this.UI_Hard_Rbn.Size = new System.Drawing.Size(48, 17);
            this.UI_Hard_Rbn.TabIndex = 2;
            this.UI_Hard_Rbn.Text = "Hard";
            this.UI_Hard_Rbn.UseVisualStyleBackColor = true;
            // 
            // UI_Medium_Rbn
            // 
            this.UI_Medium_Rbn.AutoSize = true;
            this.UI_Medium_Rbn.Location = new System.Drawing.Point(7, 42);
            this.UI_Medium_Rbn.Name = "UI_Medium_Rbn";
            this.UI_Medium_Rbn.Size = new System.Drawing.Size(62, 17);
            this.UI_Medium_Rbn.TabIndex = 1;
            this.UI_Medium_Rbn.Text = "Medium";
            this.UI_Medium_Rbn.UseVisualStyleBackColor = true;
            // 
            // UI_Easy_Rbn
            // 
            this.UI_Easy_Rbn.AutoSize = true;
            this.UI_Easy_Rbn.Checked = true;
            this.UI_Easy_Rbn.Location = new System.Drawing.Point(7, 20);
            this.UI_Easy_Rbn.Name = "UI_Easy_Rbn";
            this.UI_Easy_Rbn.Size = new System.Drawing.Size(48, 17);
            this.UI_Easy_Rbn.TabIndex = 0;
            this.UI_Easy_Rbn.TabStop = true;
            this.UI_Easy_Rbn.Text = "Easy";
            this.UI_Easy_Rbn.UseVisualStyleBackColor = true;
            // 
            // UI_Pause_btn
            // 
            this.UI_Pause_btn.Enabled = false;
            this.UI_Pause_btn.Location = new System.Drawing.Point(159, 12);
            this.UI_Pause_btn.Name = "UI_Pause_btn";
            this.UI_Pause_btn.Size = new System.Drawing.Size(141, 37);
            this.UI_Pause_btn.TabIndex = 2;
            this.UI_Pause_btn.Text = "Pause";
            this.UI_Pause_btn.UseVisualStyleBackColor = true;
            this.UI_Pause_btn.Click += new System.EventHandler(this.UI_Pause_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 205);
            this.Controls.Add(this.UI_Pause_btn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.UI_STart_Btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MinimumSize = new System.Drawing.Size(323, 244);
            this.Name = "Form1";
            this.Text = "Tetris";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer UI_Timer;
        private System.Windows.Forms.Button UI_STart_Btn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton UI_Hard_Rbn;
        private System.Windows.Forms.RadioButton UI_Medium_Rbn;
        private System.Windows.Forms.RadioButton UI_Easy_Rbn;
        private System.Windows.Forms.Button UI_Pause_btn;
    }
}

