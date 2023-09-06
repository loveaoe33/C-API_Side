namespace side
{
    partial class Requirement5
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
            this.LblMonth = new System.Windows.Forms.Label();
            this.BtnGetNewCaseId = new System.Windows.Forms.Button();
            this.TxtNewCaseId = new System.Windows.Forms.TextBox();
            this.BtnCopyCaseId = new System.Windows.Forms.Button();
            this.BtnQuery = new System.Windows.Forms.Button();
            this.TxtResult = new System.Windows.Forms.TextBox();
            this.LblAccount = new System.Windows.Forms.Label();
            this.TxtAccount = new System.Windows.Forms.TextBox();
            this.TxtMonth = new System.Windows.Forms.MaskedTextBox();
            this.BtnETL = new System.Windows.Forms.Button();
            this.LblCaseId = new System.Windows.Forms.Label();
            this.TxtCaseId = new System.Windows.Forms.TextBox();
            this.BtnQryByCaseId = new System.Windows.Forms.Button();
            this.BtnRegisterNum = new System.Windows.Forms.Button();
            this.NudRegisterNum = new System.Windows.Forms.NumericUpDown();
            this.TxtRegistDate = new System.Windows.Forms.MaskedTextBox();
            this.TxtConnectionString = new System.Windows.Forms.TextBox();
            this.LblConnectionString = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NudRegisterNum)).BeginInit();
            this.SuspendLayout();
            // 
            // LblMonth
            // 
            this.LblMonth.AutoSize = true;
            this.LblMonth.Location = new System.Drawing.Point(39, 58);
            this.LblMonth.Name = "LblMonth";
            this.LblMonth.Size = new System.Drawing.Size(29, 12);
            this.LblMonth.TabIndex = 1;
            this.LblMonth.Text = "月份";
            // 
            // BtnGetNewCaseId
            // 
            this.BtnGetNewCaseId.Location = new System.Drawing.Point(361, 54);
            this.BtnGetNewCaseId.Name = "BtnGetNewCaseId";
            this.BtnGetNewCaseId.Size = new System.Drawing.Size(75, 23);
            this.BtnGetNewCaseId.TabIndex = 2;
            this.BtnGetNewCaseId.Text = "取號";
            this.BtnGetNewCaseId.UseVisualStyleBackColor = true;
            this.BtnGetNewCaseId.Click += new System.EventHandler(this.BtnGetNewCaseId_Click);
            // 
            // TxtNewCaseId
            // 
            this.TxtNewCaseId.Location = new System.Drawing.Point(361, 83);
            this.TxtNewCaseId.Name = "TxtNewCaseId";
            this.TxtNewCaseId.ReadOnly = true;
            this.TxtNewCaseId.Size = new System.Drawing.Size(257, 22);
            this.TxtNewCaseId.TabIndex = 3;
            // 
            // BtnCopyCaseId
            // 
            this.BtnCopyCaseId.Location = new System.Drawing.Point(361, 111);
            this.BtnCopyCaseId.Name = "BtnCopyCaseId";
            this.BtnCopyCaseId.Size = new System.Drawing.Size(75, 23);
            this.BtnCopyCaseId.TabIndex = 4;
            this.BtnCopyCaseId.Text = "複製號碼";
            this.BtnCopyCaseId.UseVisualStyleBackColor = true;
            this.BtnCopyCaseId.Click += new System.EventHandler(this.BtnCopyCaseId_Click);
            // 
            // BtnQuery
            // 
            this.BtnQuery.Location = new System.Drawing.Point(243, 55);
            this.BtnQuery.Name = "BtnQuery";
            this.BtnQuery.Size = new System.Drawing.Size(75, 51);
            this.BtnQuery.TabIndex = 5;
            this.BtnQuery.Text = "查詢";
            this.BtnQuery.UseVisualStyleBackColor = true;
            this.BtnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // TxtResult
            // 
            this.TxtResult.Location = new System.Drawing.Point(41, 149);
            this.TxtResult.Multiline = true;
            this.TxtResult.Name = "TxtResult";
            this.TxtResult.ReadOnly = true;
            this.TxtResult.Size = new System.Drawing.Size(277, 286);
            this.TxtResult.TabIndex = 6;
            // 
            // LblAccount
            // 
            this.LblAccount.AutoSize = true;
            this.LblAccount.Location = new System.Drawing.Point(39, 88);
            this.LblAccount.Name = "LblAccount";
            this.LblAccount.Size = new System.Drawing.Size(29, 12);
            this.LblAccount.TabIndex = 7;
            this.LblAccount.Text = "帳號";
            // 
            // TxtAccount
            // 
            this.TxtAccount.Location = new System.Drawing.Point(74, 83);
            this.TxtAccount.Name = "TxtAccount";
            this.TxtAccount.Size = new System.Drawing.Size(100, 22);
            this.TxtAccount.TabIndex = 8;
            // 
            // TxtMonth
            // 
            this.TxtMonth.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.TxtMonth.Location = new System.Drawing.Point(74, 55);
            this.TxtMonth.Mask = "0000/00";
            this.TxtMonth.Name = "TxtMonth";
            this.TxtMonth.Size = new System.Drawing.Size(100, 22);
            this.TxtMonth.TabIndex = 9;
            // 
            // BtnETL
            // 
            this.BtnETL.Location = new System.Drawing.Point(361, 149);
            this.BtnETL.Name = "BtnETL";
            this.BtnETL.Size = new System.Drawing.Size(75, 23);
            this.BtnETL.TabIndex = 10;
            this.BtnETL.Text = "ETL";
            this.BtnETL.UseVisualStyleBackColor = true;
            this.BtnETL.Click += new System.EventHandler(this.BtnETL_Click);
            // 
            // LblCaseId
            // 
            this.LblCaseId.AutoSize = true;
            this.LblCaseId.Location = new System.Drawing.Point(39, 116);
            this.LblCaseId.Name = "LblCaseId";
            this.LblCaseId.Size = new System.Drawing.Size(29, 12);
            this.LblCaseId.TabIndex = 11;
            this.LblCaseId.Text = "單號";
            // 
            // TxtCaseId
            // 
            this.TxtCaseId.Location = new System.Drawing.Point(74, 112);
            this.TxtCaseId.Name = "TxtCaseId";
            this.TxtCaseId.Size = new System.Drawing.Size(138, 22);
            this.TxtCaseId.TabIndex = 12;
            // 
            // BtnQryByCaseId
            // 
            this.BtnQryByCaseId.Location = new System.Drawing.Point(243, 112);
            this.BtnQryByCaseId.Name = "BtnQryByCaseId";
            this.BtnQryByCaseId.Size = new System.Drawing.Size(75, 23);
            this.BtnQryByCaseId.TabIndex = 13;
            this.BtnQryByCaseId.Text = "以單號查詢";
            this.BtnQryByCaseId.UseVisualStyleBackColor = true;
            this.BtnQryByCaseId.Click += new System.EventHandler(this.BtnQryByCaseId_Click);
            // 
            // BtnRegisterNum
            // 
            this.BtnRegisterNum.Location = new System.Drawing.Point(361, 244);
            this.BtnRegisterNum.Name = "BtnRegisterNum";
            this.BtnRegisterNum.Size = new System.Drawing.Size(75, 23);
            this.BtnRegisterNum.TabIndex = 14;
            this.BtnRegisterNum.Text = "註冊人數";
            this.BtnRegisterNum.UseVisualStyleBackColor = true;
            this.BtnRegisterNum.Click += new System.EventHandler(this.BtnRegisterNum_Click);
            // 
            // NudRegisterNum
            // 
            this.NudRegisterNum.Location = new System.Drawing.Point(361, 273);
            this.NudRegisterNum.Maximum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.NudRegisterNum.Name = "NudRegisterNum";
            this.NudRegisterNum.ReadOnly = true;
            this.NudRegisterNum.Size = new System.Drawing.Size(120, 22);
            this.NudRegisterNum.TabIndex = 15;
            // 
            // TxtRegistDate
            // 
            this.TxtRegistDate.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.TxtRegistDate.Location = new System.Drawing.Point(361, 216);
            this.TxtRegistDate.Mask = "0000/00/00";
            this.TxtRegistDate.Name = "TxtRegistDate";
            this.TxtRegistDate.Size = new System.Drawing.Size(100, 22);
            this.TxtRegistDate.TabIndex = 16;
            // 
            // TxtConnectionString
            // 
            this.TxtConnectionString.Location = new System.Drawing.Point(96, 12);
            this.TxtConnectionString.Name = "TxtConnectionString";
            this.TxtConnectionString.Size = new System.Drawing.Size(544, 22);
            this.TxtConnectionString.TabIndex = 17;
            // 
            // LblConnectionString
            // 
            this.LblConnectionString.AutoSize = true;
            this.LblConnectionString.Location = new System.Drawing.Point(39, 15);
            this.LblConnectionString.Name = "LblConnectionString";
            this.LblConnectionString.Size = new System.Drawing.Size(53, 12);
            this.LblConnectionString.TabIndex = 18;
            this.LblConnectionString.Text = "連線字串";
            // 
            // Requirement5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 464);
            this.Controls.Add(this.LblConnectionString);
            this.Controls.Add(this.TxtConnectionString);
            this.Controls.Add(this.TxtRegistDate);
            this.Controls.Add(this.NudRegisterNum);
            this.Controls.Add(this.BtnRegisterNum);
            this.Controls.Add(this.BtnQryByCaseId);
            this.Controls.Add(this.TxtCaseId);
            this.Controls.Add(this.LblCaseId);
            this.Controls.Add(this.BtnETL);
            this.Controls.Add(this.TxtMonth);
            this.Controls.Add(this.TxtAccount);
            this.Controls.Add(this.LblAccount);
            this.Controls.Add(this.TxtResult);
            this.Controls.Add(this.BtnQuery);
            this.Controls.Add(this.BtnCopyCaseId);
            this.Controls.Add(this.TxtNewCaseId);
            this.Controls.Add(this.BtnGetNewCaseId);
            this.Controls.Add(this.LblMonth);
            this.Name = "Requirement5";
            this.Text = "Requirement5";
            ((System.ComponentModel.ISupportInitialize)(this.NudRegisterNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LblMonth;
        private System.Windows.Forms.Button BtnGetNewCaseId;
        private System.Windows.Forms.TextBox TxtNewCaseId;
        private System.Windows.Forms.Button BtnCopyCaseId;
        private System.Windows.Forms.Button BtnQuery;
        private System.Windows.Forms.TextBox TxtResult;
        private System.Windows.Forms.Label LblAccount;
        private System.Windows.Forms.TextBox TxtAccount;
        private System.Windows.Forms.MaskedTextBox TxtMonth;
        private System.Windows.Forms.Button BtnETL;
        private System.Windows.Forms.Label LblCaseId;
        private System.Windows.Forms.TextBox TxtCaseId;
        private System.Windows.Forms.Button BtnQryByCaseId;
        private System.Windows.Forms.Button BtnRegisterNum;
        private System.Windows.Forms.NumericUpDown NudRegisterNum;
        private System.Windows.Forms.MaskedTextBox TxtRegistDate;
        private System.Windows.Forms.TextBox TxtConnectionString;
        private System.Windows.Forms.Label LblConnectionString;
    }
}