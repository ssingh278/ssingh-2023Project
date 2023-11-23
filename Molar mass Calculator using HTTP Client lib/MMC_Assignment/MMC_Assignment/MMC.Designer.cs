namespace MMC_Assignment
{
    partial class MMC
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
            this.UI_DataGridView = new System.Windows.Forms.DataGridView();
            this.UI_SortByName_Btn = new System.Windows.Forms.Button();
            this.UI_SingleCharacterSymbols_Btn = new System.Windows.Forms.Button();
            this.UI_SortByAtomic_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UI_ChemicalFormula_tbx = new System.Windows.Forms.TextBox();
            this.UI_MolarMass_Tbx = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.UI_DataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // UI_DataGridView
            // 
            this.UI_DataGridView.AllowUserToResizeColumns = false;
            this.UI_DataGridView.AllowUserToResizeRows = false;
            this.UI_DataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UI_DataGridView.Location = new System.Drawing.Point(12, 12);
            this.UI_DataGridView.Name = "UI_DataGridView";
            this.UI_DataGridView.Size = new System.Drawing.Size(613, 366);
            this.UI_DataGridView.TabIndex = 0;
            // 
            // UI_SortByName_Btn
            // 
            this.UI_SortByName_Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_SortByName_Btn.Location = new System.Drawing.Point(647, 12);
            this.UI_SortByName_Btn.Name = "UI_SortByName_Btn";
            this.UI_SortByName_Btn.Size = new System.Drawing.Size(141, 28);
            this.UI_SortByName_Btn.TabIndex = 1;
            this.UI_SortByName_Btn.Text = "Sort By Name";
            this.UI_SortByName_Btn.UseVisualStyleBackColor = true;
            this.UI_SortByName_Btn.Click += new System.EventHandler(this.UI_SortByName_Btn_Click);
            // 
            // UI_SingleCharacterSymbols_Btn
            // 
            this.UI_SingleCharacterSymbols_Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_SingleCharacterSymbols_Btn.Location = new System.Drawing.Point(647, 58);
            this.UI_SingleCharacterSymbols_Btn.Name = "UI_SingleCharacterSymbols_Btn";
            this.UI_SingleCharacterSymbols_Btn.Size = new System.Drawing.Size(141, 28);
            this.UI_SingleCharacterSymbols_Btn.TabIndex = 2;
            this.UI_SingleCharacterSymbols_Btn.Text = "Single Character Symbols";
            this.UI_SingleCharacterSymbols_Btn.UseVisualStyleBackColor = true;
            this.UI_SingleCharacterSymbols_Btn.Click += new System.EventHandler(this.UI_SingleCharacterSymbols_Btn_Click);
            // 
            // UI_SortByAtomic_btn
            // 
            this.UI_SortByAtomic_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_SortByAtomic_btn.Location = new System.Drawing.Point(647, 104);
            this.UI_SortByAtomic_btn.Name = "UI_SortByAtomic_btn";
            this.UI_SortByAtomic_btn.Size = new System.Drawing.Size(141, 28);
            this.UI_SortByAtomic_btn.TabIndex = 3;
            this.UI_SortByAtomic_btn.Text = "Sort By Atomic #";
            this.UI_SortByAtomic_btn.UseVisualStyleBackColor = true;
            this.UI_SortByAtomic_btn.Click += new System.EventHandler(this.UI_SortByAtomic_btn_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 410);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Chemical Formula : ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(519, 410);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Approx. Molar Mass :";
            // 
            // UI_ChemicalFormula_tbx
            // 
            this.UI_ChemicalFormula_tbx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_ChemicalFormula_tbx.Location = new System.Drawing.Point(117, 407);
            this.UI_ChemicalFormula_tbx.Name = "UI_ChemicalFormula_tbx";
            this.UI_ChemicalFormula_tbx.Size = new System.Drawing.Size(339, 20);
            this.UI_ChemicalFormula_tbx.TabIndex = 6;
            this.UI_ChemicalFormula_tbx.TextChanged += new System.EventHandler(this.UI_ChemicalFormula_tbx_TextChanged);
            // 
            // UI_MolarMass_Tbx
            // 
            this.UI_MolarMass_Tbx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_MolarMass_Tbx.Location = new System.Drawing.Point(647, 407);
            this.UI_MolarMass_Tbx.Name = "UI_MolarMass_Tbx";
            this.UI_MolarMass_Tbx.ReadOnly = true;
            this.UI_MolarMass_Tbx.Size = new System.Drawing.Size(141, 20);
            this.UI_MolarMass_Tbx.TabIndex = 7;
            // 
            // MMC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.UI_MolarMass_Tbx);
            this.Controls.Add(this.UI_ChemicalFormula_tbx);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UI_SortByAtomic_btn);
            this.Controls.Add(this.UI_SingleCharacterSymbols_Btn);
            this.Controls.Add(this.UI_SortByName_Btn);
            this.Controls.Add(this.UI_DataGridView);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "MMC";
            this.Text = "MMC";
            this.Load += new System.EventHandler(this.MMC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UI_DataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView UI_DataGridView;
        private System.Windows.Forms.Button UI_SortByName_Btn;
        private System.Windows.Forms.Button UI_SingleCharacterSymbols_Btn;
        private System.Windows.Forms.Button UI_SortByAtomic_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox UI_ChemicalFormula_tbx;
        private System.Windows.Forms.TextBox UI_MolarMass_Tbx;
    }
}

