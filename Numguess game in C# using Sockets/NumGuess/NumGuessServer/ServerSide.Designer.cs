namespace NumGuessServer
{
    partial class ServerSide
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
            this.UI_CorrectWords_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UI_CorrectWords_lbl
            // 
            this.UI_CorrectWords_lbl.AutoSize = true;
            this.UI_CorrectWords_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_CorrectWords_lbl.Location = new System.Drawing.Point(183, 119);
            this.UI_CorrectWords_lbl.Name = "UI_CorrectWords_lbl";
            this.UI_CorrectWords_lbl.Size = new System.Drawing.Size(92, 31);
            this.UI_CorrectWords_lbl.TabIndex = 0;
            this.UI_CorrectWords_lbl.Text = "label1";
            // 
            // ServerSide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 311);
            this.Controls.Add(this.UI_CorrectWords_lbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimumSize = new System.Drawing.Size(496, 350);
            this.Name = "ServerSide";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.ServerSide_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UI_CorrectWords_lbl;
    }
}

