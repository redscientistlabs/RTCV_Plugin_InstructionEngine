namespace InstructionEngine.UI
{
    partial class InstrEngineHolder
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbInstrMethod = new System.Windows.Forms.ComboBox();
            this.lblDescribe = new System.Windows.Forms.Label();
            this.pSettings = new System.Windows.Forms.Panel();
            this.bDebugPush = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbInstrMethod
            // 
            this.cbInstrMethod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.cbInstrMethod.DisplayMember = "Value";
            this.cbInstrMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInstrMethod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbInstrMethod.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbInstrMethod.ForeColor = System.Drawing.Color.White;
            this.cbInstrMethod.FormattingEnabled = true;
            this.cbInstrMethod.IntegralHeight = false;
            this.cbInstrMethod.Location = new System.Drawing.Point(6, 62);
            this.cbInstrMethod.MaxDropDownItems = 15;
            this.cbInstrMethod.Name = "cbInstrMethod";
            this.cbInstrMethod.Size = new System.Drawing.Size(105, 21);
            this.cbInstrMethod.TabIndex = 78;
            this.cbInstrMethod.Tag = "color:normal";
            this.cbInstrMethod.ValueMember = "Value";
            // 
            // lblDescribe
            // 
            this.lblDescribe.AutoSize = true;
            this.lblDescribe.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescribe.ForeColor = System.Drawing.Color.White;
            this.lblDescribe.Location = new System.Drawing.Point(3, 46);
            this.lblDescribe.Name = "lblDescribe";
            this.lblDescribe.Size = new System.Drawing.Size(51, 13);
            this.lblDescribe.TabIndex = 79;
            this.lblDescribe.Text = "Method:";
            // 
            // pSettings
            // 
            this.pSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pSettings.Location = new System.Drawing.Point(117, 0);
            this.pSettings.Margin = new System.Windows.Forms.Padding(0);
            this.pSettings.Name = "pSettings";
            this.pSettings.Size = new System.Drawing.Size(302, 151);
            this.pSettings.TabIndex = 80;
            // 
            // bDebugPush
            // 
            this.bDebugPush.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.bDebugPush.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.bDebugPush.FlatAppearance.BorderSize = 0;
            this.bDebugPush.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDebugPush.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bDebugPush.ForeColor = System.Drawing.Color.OrangeRed;
            this.bDebugPush.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bDebugPush.Location = new System.Drawing.Point(6, 90);
            this.bDebugPush.Margin = new System.Windows.Forms.Padding(4);
            this.bDebugPush.Name = "bDebugPush";
            this.bDebugPush.Size = new System.Drawing.Size(105, 48);
            this.bDebugPush.TabIndex = 175;
            this.bDebugPush.TabStop = false;
            this.bDebugPush.Tag = "color:dark2";
            this.bDebugPush.Text = "RESYNC";
            this.bDebugPush.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bDebugPush.UseVisualStyleBackColor = false;
            this.bDebugPush.Click += new System.EventHandler(this.bDebugPush_Click);
            // 
            // InstrEngineHolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(420, 151);
            this.Controls.Add(this.bDebugPush);
            this.Controls.Add(this.pSettings);
            this.Controls.Add(this.lblDescribe);
            this.Controls.Add(this.cbInstrMethod);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(420, 151);
            this.Name = "InstrEngineHolder";
            this.Tag = "color:dark1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ComboBox cbInstrMethod;
        private System.Windows.Forms.Label lblDescribe;
        private System.Windows.Forms.Panel pSettings;
        public System.Windows.Forms.Button bDebugPush;
    }
}
