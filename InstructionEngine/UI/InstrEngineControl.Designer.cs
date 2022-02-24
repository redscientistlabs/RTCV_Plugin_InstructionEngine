namespace InstructionEngine.UI
{
    partial class InstrEngineControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clbFilters2 = new System.Windows.Forms.CheckedListBox();
            this.cbArchitecture = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbOutputForward = new System.Windows.Forms.ComboBox();
            this.cbOutputBack = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbForwardTarg = new System.Windows.Forms.ComboBox();
            this.cbBackTarg = new System.Windows.Forms.ComboBox();
            this.cbExclusive = new System.Windows.Forms.CheckBox();
            this.clbFilters = new System.Windows.Forms.CheckedListBox();
            this.cbUniqueRegisters = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bDebugPush = new System.Windows.Forms.Button();
            this.nmBleedForward = new System.Windows.Forms.NumericUpDown();
            this.nmBleedBack = new System.Windows.Forms.NumericUpDown();
            this.cbOutputFuture = new System.Windows.Forms.CheckBox();
            this.pnLimiterList = new System.Windows.Forms.Panel();
            this.cbInstrMethod = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmBleedForward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmBleedBack)).BeginInit();
            this.pnLimiterList.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBox1.Controls.Add(this.clbFilters2);
            this.groupBox1.Controls.Add(this.cbArchitecture);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbOutputForward);
            this.groupBox1.Controls.Add(this.cbOutputBack);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbForwardTarg);
            this.groupBox1.Controls.Add(this.cbBackTarg);
            this.groupBox1.Controls.Add(this.cbExclusive);
            this.groupBox1.Controls.Add(this.clbFilters);
            this.groupBox1.Controls.Add(this.cbUniqueRegisters);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.bDebugPush);
            this.groupBox1.Controls.Add(this.nmBleedForward);
            this.groupBox1.Controls.Add(this.nmBleedBack);
            this.groupBox1.Controls.Add(this.cbOutputFuture);
            this.groupBox1.Controls.Add(this.pnLimiterList);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 151);
            this.groupBox1.TabIndex = 168;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "color:dark1";
            // 
            // clbFilters2
            // 
            this.clbFilters2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clbFilters2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.clbFilters2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbFilters2.ForeColor = System.Drawing.Color.White;
            this.clbFilters2.FormattingEnabled = true;
            this.clbFilters2.IntegralHeight = false;
            this.clbFilters2.Location = new System.Drawing.Point(288, 91);
            this.clbFilters2.Margin = new System.Windows.Forms.Padding(0);
            this.clbFilters2.Name = "clbFilters2";
            this.clbFilters2.Size = new System.Drawing.Size(126, 57);
            this.clbFilters2.TabIndex = 187;
            this.clbFilters2.Tag = "color:dark2";
            // 
            // cbArchitecture
            // 
            this.cbArchitecture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.cbArchitecture.DisplayMember = "Value";
            this.cbArchitecture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbArchitecture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbArchitecture.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbArchitecture.ForeColor = System.Drawing.Color.White;
            this.cbArchitecture.FormattingEnabled = true;
            this.cbArchitecture.IntegralHeight = false;
            this.cbArchitecture.Location = new System.Drawing.Point(288, 9);
            this.cbArchitecture.MaxDropDownItems = 15;
            this.cbArchitecture.Name = "cbArchitecture";
            this.cbArchitecture.Size = new System.Drawing.Size(126, 21);
            this.cbArchitecture.TabIndex = 186;
            this.cbArchitecture.Tag = "color:normal";
            this.cbArchitecture.ValueMember = "Value";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(125, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 185;
            this.label5.Text = "Output";
            // 
            // cbOutputForward
            // 
            this.cbOutputForward.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.cbOutputForward.DisplayMember = "Value";
            this.cbOutputForward.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOutputForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbOutputForward.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbOutputForward.ForeColor = System.Drawing.Color.White;
            this.cbOutputForward.FormattingEnabled = true;
            this.cbOutputForward.IntegralHeight = false;
            this.cbOutputForward.Location = new System.Drawing.Point(125, 119);
            this.cbOutputForward.MaxDropDownItems = 15;
            this.cbOutputForward.Name = "cbOutputForward";
            this.cbOutputForward.Size = new System.Drawing.Size(73, 21);
            this.cbOutputForward.TabIndex = 184;
            this.cbOutputForward.Tag = "color:normal";
            this.cbOutputForward.ValueMember = "Value";
            // 
            // cbOutputBack
            // 
            this.cbOutputBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.cbOutputBack.DisplayMember = "Value";
            this.cbOutputBack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOutputBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbOutputBack.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbOutputBack.ForeColor = System.Drawing.Color.White;
            this.cbOutputBack.FormattingEnabled = true;
            this.cbOutputBack.IntegralHeight = false;
            this.cbOutputBack.Location = new System.Drawing.Point(125, 90);
            this.cbOutputBack.MaxDropDownItems = 15;
            this.cbOutputBack.Name = "cbOutputBack";
            this.cbOutputBack.Size = new System.Drawing.Size(73, 21);
            this.cbOutputBack.TabIndex = 183;
            this.cbOutputBack.Tag = "color:normal";
            this.cbOutputBack.ValueMember = "Value";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(11, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 26);
            this.label4.TabIndex = 182;
            this.label4.Text = "Forw.\r\nTarg";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(11, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 26);
            this.label3.TabIndex = 181;
            this.label3.Text = "Back\r\nTarg";
            // 
            // cbForwardTarg
            // 
            this.cbForwardTarg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.cbForwardTarg.DisplayMember = "Value";
            this.cbForwardTarg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbForwardTarg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbForwardTarg.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbForwardTarg.ForeColor = System.Drawing.Color.White;
            this.cbForwardTarg.FormattingEnabled = true;
            this.cbForwardTarg.IntegralHeight = false;
            this.cbForwardTarg.Location = new System.Drawing.Point(46, 119);
            this.cbForwardTarg.MaxDropDownItems = 15;
            this.cbForwardTarg.Name = "cbForwardTarg";
            this.cbForwardTarg.Size = new System.Drawing.Size(73, 21);
            this.cbForwardTarg.TabIndex = 180;
            this.cbForwardTarg.Tag = "color:normal";
            this.cbForwardTarg.ValueMember = "Value";
            // 
            // cbBackTarg
            // 
            this.cbBackTarg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.cbBackTarg.DisplayMember = "Value";
            this.cbBackTarg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBackTarg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbBackTarg.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbBackTarg.ForeColor = System.Drawing.Color.White;
            this.cbBackTarg.FormattingEnabled = true;
            this.cbBackTarg.IntegralHeight = false;
            this.cbBackTarg.Location = new System.Drawing.Point(46, 90);
            this.cbBackTarg.MaxDropDownItems = 15;
            this.cbBackTarg.Name = "cbBackTarg";
            this.cbBackTarg.Size = new System.Drawing.Size(73, 21);
            this.cbBackTarg.TabIndex = 80;
            this.cbBackTarg.Tag = "color:normal";
            this.cbBackTarg.ValueMember = "Value";
            // 
            // cbExclusive
            // 
            this.cbExclusive.AutoSize = true;
            this.cbExclusive.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.cbExclusive.ForeColor = System.Drawing.Color.White;
            this.cbExclusive.Location = new System.Drawing.Point(190, 58);
            this.cbExclusive.Name = "cbExclusive";
            this.cbExclusive.Size = new System.Drawing.Size(67, 17);
            this.cbExclusive.TabIndex = 179;
            this.cbExclusive.Text = "Exclusive";
            this.cbExclusive.UseVisualStyleBackColor = true;
            this.cbExclusive.CheckedChanged += new System.EventHandler(this.cbExclusive_CheckedChanged);
            // 
            // clbFilters
            // 
            this.clbFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clbFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.clbFilters.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbFilters.ForeColor = System.Drawing.Color.White;
            this.clbFilters.FormattingEnabled = true;
            this.clbFilters.IntegralHeight = false;
            this.clbFilters.Location = new System.Drawing.Point(288, 33);
            this.clbFilters.Margin = new System.Windows.Forms.Padding(0);
            this.clbFilters.Name = "clbFilters";
            this.clbFilters.Size = new System.Drawing.Size(126, 57);
            this.clbFilters.TabIndex = 178;
            this.clbFilters.Tag = "color:dark2";
            // 
            // cbUniqueRegisters
            // 
            this.cbUniqueRegisters.AutoSize = true;
            this.cbUniqueRegisters.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.cbUniqueRegisters.ForeColor = System.Drawing.Color.White;
            this.cbUniqueRegisters.Location = new System.Drawing.Point(190, 43);
            this.cbUniqueRegisters.Name = "cbUniqueRegisters";
            this.cbUniqueRegisters.Size = new System.Drawing.Size(60, 17);
            this.cbUniqueRegisters.TabIndex = 177;
            this.cbUniqueRegisters.Text = "Unique";
            this.cbUniqueRegisters.UseVisualStyleBackColor = true;
            this.cbUniqueRegisters.CheckedChanged += new System.EventHandler(this.cbUniqueRegisters_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(209, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 26);
            this.label2.TabIndex = 176;
            this.label2.Text = "Look\r\nForw.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(209, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 26);
            this.label1.TabIndex = 175;
            this.label1.Text = "Look\r\nBack";
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
            this.bDebugPush.Location = new System.Drawing.Point(129, 38);
            this.bDebugPush.Margin = new System.Windows.Forms.Padding(4);
            this.bDebugPush.Name = "bDebugPush";
            this.bDebugPush.Size = new System.Drawing.Size(54, 22);
            this.bDebugPush.TabIndex = 174;
            this.bDebugPush.TabStop = false;
            this.bDebugPush.Tag = "color:dark2";
            this.bDebugPush.Text = "DEBUG";
            this.bDebugPush.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bDebugPush.UseVisualStyleBackColor = false;
            this.bDebugPush.Click += new System.EventHandler(this.bDebugPush_Click);
            // 
            // nmBleedForward
            // 
            this.nmBleedForward.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.nmBleedForward.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.nmBleedForward.ForeColor = System.Drawing.Color.White;
            this.nmBleedForward.Location = new System.Drawing.Point(245, 115);
            this.nmBleedForward.Margin = new System.Windows.Forms.Padding(4);
            this.nmBleedForward.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nmBleedForward.Name = "nmBleedForward";
            this.nmBleedForward.Size = new System.Drawing.Size(37, 22);
            this.nmBleedForward.TabIndex = 172;
            this.nmBleedForward.Tag = "color:normal";
            this.nmBleedForward.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmBleedForward.ValueChanged += new System.EventHandler(this.nmBleedForward_ValueChanged);
            // 
            // nmBleedBack
            // 
            this.nmBleedBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.nmBleedBack.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.nmBleedBack.ForeColor = System.Drawing.Color.White;
            this.nmBleedBack.Location = new System.Drawing.Point(245, 86);
            this.nmBleedBack.Margin = new System.Windows.Forms.Padding(4);
            this.nmBleedBack.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nmBleedBack.Name = "nmBleedBack";
            this.nmBleedBack.Size = new System.Drawing.Size(37, 22);
            this.nmBleedBack.TabIndex = 171;
            this.nmBleedBack.Tag = "color:normal";
            this.nmBleedBack.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmBleedBack.ValueChanged += new System.EventHandler(this.nmBleedBack_ValueChanged);
            // 
            // cbOutputFuture
            // 
            this.cbOutputFuture.AutoSize = true;
            this.cbOutputFuture.Checked = true;
            this.cbOutputFuture.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOutputFuture.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.cbOutputFuture.ForeColor = System.Drawing.Color.White;
            this.cbOutputFuture.Location = new System.Drawing.Point(190, 28);
            this.cbOutputFuture.Name = "cbOutputFuture";
            this.cbOutputFuture.Size = new System.Drawing.Size(54, 17);
            this.cbOutputFuture.TabIndex = 170;
            this.cbOutputFuture.Text = "Smart";
            this.cbOutputFuture.UseVisualStyleBackColor = true;
            this.cbOutputFuture.CheckedChanged += new System.EventHandler(this.cbSubtract_CheckedChanged);
            // 
            // pnLimiterList
            // 
            this.pnLimiterList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pnLimiterList.Controls.Add(this.cbInstrMethod);
            this.pnLimiterList.Controls.Add(this.label13);
            this.pnLimiterList.Location = new System.Drawing.Point(6, 39);
            this.pnLimiterList.Name = "pnLimiterList";
            this.pnLimiterList.Size = new System.Drawing.Size(116, 47);
            this.pnLimiterList.TabIndex = 168;
            this.pnLimiterList.Tag = "color:dark2";
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
            this.cbInstrMethod.Location = new System.Drawing.Point(8, 19);
            this.cbInstrMethod.MaxDropDownItems = 15;
            this.cbInstrMethod.Name = "cbInstrMethod";
            this.cbInstrMethod.Size = new System.Drawing.Size(105, 21);
            this.cbInstrMethod.TabIndex = 78;
            this.cbInstrMethod.Tag = "color:normal";
            this.cbInstrMethod.ValueMember = "Value";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(6, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 13);
            this.label13.TabIndex = 79;
            this.label13.Text = "Method";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(168, 12);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 13);
            this.label14.TabIndex = 167;
            this.label14.Text = "Instruction Stuff";
            // 
            // InstrEngineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 151);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(420, 151);
            this.Name = "InstrEngineControl";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmBleedForward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmBleedBack)).EndInit();
            this.pnLimiterList.ResumeLayout(false);
            this.pnLimiterList.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel pnLimiterList;
        public System.Windows.Forms.ComboBox cbInstrMethod;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.CheckBox cbOutputFuture;
        private System.Windows.Forms.NumericUpDown nmBleedBack;
        private System.Windows.Forms.NumericUpDown nmBleedForward;
        public System.Windows.Forms.Button bDebugPush;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox cbUniqueRegisters;
        private System.Windows.Forms.CheckedListBox clbFilters;
        public System.Windows.Forms.CheckBox cbExclusive;
        public System.Windows.Forms.ComboBox cbForwardTarg;
        public System.Windows.Forms.ComboBox cbBackTarg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox cbOutputForward;
        public System.Windows.Forms.ComboBox cbOutputBack;
        public System.Windows.Forms.ComboBox cbArchitecture;
        private System.Windows.Forms.CheckedListBox clbFilters2;
    }
}
