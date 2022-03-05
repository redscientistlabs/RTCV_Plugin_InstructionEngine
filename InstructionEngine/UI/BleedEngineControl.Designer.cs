
namespace InstructionEngine.UI
{
    partial class BleedEngineControl
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
            this.cbOutputForward = new System.Windows.Forms.ComboBox();
            this.cbOutputBack = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbForwardTarg = new System.Windows.Forms.ComboBox();
            this.cbBackTarg = new System.Windows.Forms.ComboBox();
            this.cbExclusive = new System.Windows.Forms.CheckBox();
            this.cbUniqueRegisters = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nmBleedForward = new System.Windows.Forms.NumericUpDown();
            this.nmBleedBack = new System.Windows.Forms.NumericUpDown();
            this.cbOutputFuture = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.bFilter1 = new System.Windows.Forms.Button();
            this.bFilter2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nmBleedForward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmBleedBack)).BeginInit();
            this.SuspendLayout();
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
            this.cbOutputForward.Location = new System.Drawing.Point(151, 63);
            this.cbOutputForward.MaxDropDownItems = 15;
            this.cbOutputForward.Name = "cbOutputForward";
            this.cbOutputForward.Size = new System.Drawing.Size(73, 21);
            this.cbOutputForward.TabIndex = 190;
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
            this.cbOutputBack.Location = new System.Drawing.Point(151, 36);
            this.cbOutputBack.MaxDropDownItems = 15;
            this.cbOutputBack.Name = "cbOutputBack";
            this.cbOutputBack.Size = new System.Drawing.Size(73, 21);
            this.cbOutputBack.TabIndex = 189;
            this.cbOutputBack.Tag = "color:normal";
            this.cbOutputBack.ValueMember = "Value";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(7, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 26);
            this.label4.TabIndex = 188;
            this.label4.Text = "Forw.\r\nTarg";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(7, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 26);
            this.label3.TabIndex = 187;
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
            this.cbForwardTarg.Location = new System.Drawing.Point(42, 63);
            this.cbForwardTarg.MaxDropDownItems = 15;
            this.cbForwardTarg.Name = "cbForwardTarg";
            this.cbForwardTarg.Size = new System.Drawing.Size(73, 21);
            this.cbForwardTarg.TabIndex = 186;
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
            this.cbBackTarg.Location = new System.Drawing.Point(42, 36);
            this.cbBackTarg.MaxDropDownItems = 15;
            this.cbBackTarg.Name = "cbBackTarg";
            this.cbBackTarg.Size = new System.Drawing.Size(73, 21);
            this.cbBackTarg.TabIndex = 185;
            this.cbBackTarg.Tag = "color:normal";
            this.cbBackTarg.ValueMember = "Value";
            // 
            // cbExclusive
            // 
            this.cbExclusive.AutoSize = true;
            this.cbExclusive.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.cbExclusive.ForeColor = System.Drawing.Color.White;
            this.cbExclusive.Location = new System.Drawing.Point(236, 46);
            this.cbExclusive.Name = "cbExclusive";
            this.cbExclusive.Size = new System.Drawing.Size(67, 17);
            this.cbExclusive.TabIndex = 197;
            this.cbExclusive.Text = "Exclusive";
            this.cbExclusive.UseVisualStyleBackColor = true;
            this.cbExclusive.CheckedChanged += new System.EventHandler(this.cbExclusive_CheckedChanged);
            // 
            // cbUniqueRegisters
            // 
            this.cbUniqueRegisters.AutoSize = true;
            this.cbUniqueRegisters.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.cbUniqueRegisters.ForeColor = System.Drawing.Color.White;
            this.cbUniqueRegisters.Location = new System.Drawing.Point(236, 27);
            this.cbUniqueRegisters.Name = "cbUniqueRegisters";
            this.cbUniqueRegisters.Size = new System.Drawing.Size(60, 17);
            this.cbUniqueRegisters.TabIndex = 196;
            this.cbUniqueRegisters.Text = "Unique";
            this.cbUniqueRegisters.UseVisualStyleBackColor = true;
            this.cbUniqueRegisters.CheckedChanged += new System.EventHandler(this.cbUniqueRegisters_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 26);
            this.label2.TabIndex = 195;
            this.label2.Text = "Look\r\nForw.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(7, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 26);
            this.label1.TabIndex = 194;
            this.label1.Text = "Look\r\nBack";
            // 
            // nmBleedForward
            // 
            this.nmBleedForward.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.nmBleedForward.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.nmBleedForward.ForeColor = System.Drawing.Color.White;
            this.nmBleedForward.Location = new System.Drawing.Point(43, 121);
            this.nmBleedForward.Margin = new System.Windows.Forms.Padding(4);
            this.nmBleedForward.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nmBleedForward.Name = "nmBleedForward";
            this.nmBleedForward.Size = new System.Drawing.Size(73, 22);
            this.nmBleedForward.TabIndex = 193;
            this.nmBleedForward.Tag = "color:normal";
            this.nmBleedForward.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // nmBleedBack
            // 
            this.nmBleedBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.nmBleedBack.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.nmBleedBack.ForeColor = System.Drawing.Color.White;
            this.nmBleedBack.Location = new System.Drawing.Point(43, 93);
            this.nmBleedBack.Margin = new System.Windows.Forms.Padding(4);
            this.nmBleedBack.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nmBleedBack.Name = "nmBleedBack";
            this.nmBleedBack.Size = new System.Drawing.Size(73, 22);
            this.nmBleedBack.TabIndex = 192;
            this.nmBleedBack.Tag = "color:normal";
            this.nmBleedBack.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // cbOutputFuture
            // 
            this.cbOutputFuture.AutoSize = true;
            this.cbOutputFuture.Checked = true;
            this.cbOutputFuture.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOutputFuture.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.cbOutputFuture.ForeColor = System.Drawing.Color.White;
            this.cbOutputFuture.Location = new System.Drawing.Point(236, 8);
            this.cbOutputFuture.Name = "cbOutputFuture";
            this.cbOutputFuture.Size = new System.Drawing.Size(54, 17);
            this.cbOutputFuture.TabIndex = 191;
            this.cbOutputFuture.Text = "Smart";
            this.cbOutputFuture.UseVisualStyleBackColor = true;
            this.cbOutputFuture.CheckedChanged += new System.EventHandler(this.cbOutputFuture_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(148, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 26);
            this.label5.TabIndex = 198;
            this.label5.Text = "BlastUnit\r\nRegister Targets";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(122, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 199;
            this.label6.Text = "=>";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(122, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 200;
            this.label7.Text = "=>";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(68, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 26);
            this.label8.TabIndex = 201;
            this.label8.Text = "Donor\r\nRegisters";
            // 
            // bFilter1
            // 
            this.bFilter1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bFilter1.BackColor = System.Drawing.Color.Gray;
            this.bFilter1.FlatAppearance.BorderSize = 0;
            this.bFilter1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFilter1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.bFilter1.ForeColor = System.Drawing.Color.White;
            this.bFilter1.Location = new System.Drawing.Point(231, 93);
            this.bFilter1.Name = "bFilter1";
            this.bFilter1.Size = new System.Drawing.Size(65, 50);
            this.bFilter1.TabIndex = 202;
            this.bFilter1.TabStop = false;
            this.bFilter1.Tag = "color:light1";
            this.bFilter1.Text = "Target Filter";
            this.bFilter1.UseVisualStyleBackColor = false;
            this.bFilter1.Click += new System.EventHandler(this.bFilter1_Click);
            // 
            // bFilter2
            // 
            this.bFilter2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bFilter2.BackColor = System.Drawing.Color.Gray;
            this.bFilter2.FlatAppearance.BorderSize = 0;
            this.bFilter2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFilter2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.bFilter2.ForeColor = System.Drawing.Color.White;
            this.bFilter2.Location = new System.Drawing.Point(151, 93);
            this.bFilter2.Name = "bFilter2";
            this.bFilter2.Size = new System.Drawing.Size(73, 50);
            this.bFilter2.TabIndex = 203;
            this.bFilter2.TabStop = false;
            this.bFilter2.Tag = "color:light1";
            this.bFilter2.Text = "Donor\r\nFilter";
            this.bFilter2.UseVisualStyleBackColor = false;
            this.bFilter2.Click += new System.EventHandler(this.bFilter2_Click);
            // 
            // BleedEngineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.bFilter2);
            this.Controls.Add(this.bFilter1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbExclusive);
            this.Controls.Add(this.cbUniqueRegisters);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nmBleedForward);
            this.Controls.Add(this.nmBleedBack);
            this.Controls.Add(this.cbOutputFuture);
            this.Controls.Add(this.cbOutputForward);
            this.Controls.Add(this.cbOutputBack);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbForwardTarg);
            this.Controls.Add(this.cbBackTarg);
            this.Name = "BleedEngineControl";
            this.Size = new System.Drawing.Size(302, 151);
            this.Tag = "color:dark1";
            ((System.ComponentModel.ISupportInitialize)(this.nmBleedForward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmBleedBack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cbOutputForward;
        public System.Windows.Forms.ComboBox cbOutputBack;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cbForwardTarg;
        public System.Windows.Forms.ComboBox cbBackTarg;
        public System.Windows.Forms.CheckBox cbExclusive;
        public System.Windows.Forms.CheckBox cbUniqueRegisters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmBleedForward;
        private System.Windows.Forms.NumericUpDown nmBleedBack;
        public System.Windows.Forms.CheckBox cbOutputFuture;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.Button bFilter1;
        public System.Windows.Forms.Button bFilter2;
    }
}
