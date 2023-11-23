namespace NumGuess
{
    partial class ClientSide
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
            this.Connect_Button = new System.Windows.Forms.Button();
            this.Guess_Button = new System.Windows.Forms.Button();
            this.Message_Textbox = new System.Windows.Forms.TextBox();
            this.Current_Textbox = new System.Windows.Forms.TextBox();
            this.Upper_Textbox = new System.Windows.Forms.TextBox();
            this.Lower_Textbox = new System.Windows.Forms.TextBox();
            this.Guess_Trackbar = new System.Windows.Forms.TrackBar();
            this.UI_ConnectSharry_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Guess_Trackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // Connect_Button
            // 
            this.Connect_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Connect_Button.Location = new System.Drawing.Point(12, 12);
            this.Connect_Button.Name = "Connect_Button";
            this.Connect_Button.Size = new System.Drawing.Size(182, 44);
            this.Connect_Button.TabIndex = 13;
            this.Connect_Button.Text = "Connect";
            this.Connect_Button.UseVisualStyleBackColor = true;
            this.Connect_Button.Click += new System.EventHandler(this.Connect_Button_Click);
            // 
            // Guess_Button
            // 
            this.Guess_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Guess_Button.Enabled = false;
            this.Guess_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Guess_Button.Location = new System.Drawing.Point(335, 318);
            this.Guess_Button.Name = "Guess_Button";
            this.Guess_Button.Size = new System.Drawing.Size(128, 44);
            this.Guess_Button.TabIndex = 12;
            this.Guess_Button.Text = "Guess";
            this.Guess_Button.UseVisualStyleBackColor = true;
            this.Guess_Button.Click += new System.EventHandler(this.Guess_Button_Click);
            // 
            // Message_Textbox
            // 
            this.Message_Textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Message_Textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Message_Textbox.Location = new System.Drawing.Point(12, 381);
            this.Message_Textbox.Name = "Message_Textbox";
            this.Message_Textbox.ReadOnly = true;
            this.Message_Textbox.Size = new System.Drawing.Size(776, 31);
            this.Message_Textbox.TabIndex = 11;
            this.Message_Textbox.Text = "Connect to the server to start the game!";
            // 
            // Current_Textbox
            // 
            this.Current_Textbox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Current_Textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Current_Textbox.Location = new System.Drawing.Point(350, 236);
            this.Current_Textbox.Name = "Current_Textbox";
            this.Current_Textbox.ReadOnly = true;
            this.Current_Textbox.Size = new System.Drawing.Size(100, 31);
            this.Current_Textbox.TabIndex = 10;
            this.Current_Textbox.Text = "500";
            this.Current_Textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Upper_Textbox
            // 
            this.Upper_Textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Upper_Textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Upper_Textbox.Location = new System.Drawing.Point(688, 236);
            this.Upper_Textbox.Name = "Upper_Textbox";
            this.Upper_Textbox.ReadOnly = true;
            this.Upper_Textbox.Size = new System.Drawing.Size(100, 31);
            this.Upper_Textbox.TabIndex = 9;
            this.Upper_Textbox.Text = "1000";
            this.Upper_Textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Lower_Textbox
            // 
            this.Lower_Textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Lower_Textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lower_Textbox.Location = new System.Drawing.Point(12, 236);
            this.Lower_Textbox.Name = "Lower_Textbox";
            this.Lower_Textbox.ReadOnly = true;
            this.Lower_Textbox.Size = new System.Drawing.Size(100, 31);
            this.Lower_Textbox.TabIndex = 8;
            this.Lower_Textbox.Text = "1";
            this.Lower_Textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Guess_Trackbar
            // 
            this.Guess_Trackbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Guess_Trackbar.Location = new System.Drawing.Point(12, 185);
            this.Guess_Trackbar.Maximum = 1000;
            this.Guess_Trackbar.Minimum = 1;
            this.Guess_Trackbar.Name = "Guess_Trackbar";
            this.Guess_Trackbar.Size = new System.Drawing.Size(776, 45);
            this.Guess_Trackbar.TabIndex = 7;
            this.Guess_Trackbar.TickFrequency = 50;
            this.Guess_Trackbar.Value = 500;
            this.Guess_Trackbar.Scroll += new System.EventHandler(this.Guess_Trackbar_Scroll);
            // 
            // UI_ConnectSharry_btn
            // 
            this.UI_ConnectSharry_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UI_ConnectSharry_btn.Location = new System.Drawing.Point(200, 12);
            this.UI_ConnectSharry_btn.Name = "UI_ConnectSharry_btn";
            this.UI_ConnectSharry_btn.Size = new System.Drawing.Size(182, 44);
            this.UI_ConnectSharry_btn.TabIndex = 14;
            this.UI_ConnectSharry_btn.Text = "Connect";
            this.UI_ConnectSharry_btn.UseVisualStyleBackColor = true;
            this.UI_ConnectSharry_btn.Click += new System.EventHandler(this.UI_ConnectSharry_btn_Click_1);
            // 
            // ClientSide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.UI_ConnectSharry_btn);
            this.Controls.Add(this.Connect_Button);
            this.Controls.Add(this.Guess_Button);
            this.Controls.Add(this.Message_Textbox);
            this.Controls.Add(this.Current_Textbox);
            this.Controls.Add(this.Upper_Textbox);
            this.Controls.Add(this.Lower_Textbox);
            this.Controls.Add(this.Guess_Trackbar);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "ClientSide";
            this.Text = "Client";
            ((System.ComponentModel.ISupportInitialize)(this.Guess_Trackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Connect_Button;
        private System.Windows.Forms.Button Guess_Button;
        private System.Windows.Forms.TextBox Message_Textbox;
        private System.Windows.Forms.TextBox Current_Textbox;
        private System.Windows.Forms.TextBox Upper_Textbox;
        private System.Windows.Forms.TextBox Lower_Textbox;
        private System.Windows.Forms.TrackBar Guess_Trackbar;
        private System.Windows.Forms.Button UI_ConnectSharry_btn;
    }
}

