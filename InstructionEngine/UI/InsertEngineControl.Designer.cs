
namespace InstructionEngine.UI
{
    partial class InsertEngineControl
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
            this.nmBleedBack = new System.Windows.Forms.NumericUpDown();
            this.cbShuffle = new System.Windows.Forms.CheckBox();
            this.filter1 = new InstructionEngine.UI.InstrCheckList();
            this.filter2 = new InstructionEngine.UI.InstrCheckList();
            ((System.ComponentModel.ISupportInitialize)(this.nmBleedBack)).BeginInit();
            this.SuspendLayout();
            // 
            // nmBleedBack
            // 
            this.nmBleedBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.nmBleedBack.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.nmBleedBack.ForeColor = System.Drawing.Color.White;
            this.nmBleedBack.Location = new System.Drawing.Point(10, 125);
            this.nmBleedBack.Margin = new System.Windows.Forms.Padding(4);
            this.nmBleedBack.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nmBleedBack.Name = "nmBleedBack";
            this.nmBleedBack.Size = new System.Drawing.Size(76, 22);
            this.nmBleedBack.TabIndex = 172;
            this.nmBleedBack.Tag = "color:normal";
            // 
            // cbShuffle
            // 
            this.cbShuffle.AutoSize = true;
            this.cbShuffle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShuffle.ForeColor = System.Drawing.Color.White;
            this.cbShuffle.Location = new System.Drawing.Point(10, 101);
            this.cbShuffle.Name = "cbShuffle";
            this.cbShuffle.Size = new System.Drawing.Size(63, 17);
            this.cbShuffle.TabIndex = 173;
            this.cbShuffle.Text = "Shuffle";
            this.cbShuffle.UseVisualStyleBackColor = true;
            this.cbShuffle.CheckedChanged += new System.EventHandler(this.cbShuffle_CheckedChanged);
            // 
            // filter1
            // 
            this.filter1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.filter1.Location = new System.Drawing.Point(116, 0);
            this.filter1.Name = "filter1";
            this.filter1.Size = new System.Drawing.Size(93, 151);
            this.filter1.TabIndex = 1;
            // 
            // filter2
            // 
            this.filter2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.filter2.Location = new System.Drawing.Point(209, 0);
            this.filter2.Name = "filter2";
            this.filter2.Size = new System.Drawing.Size(93, 151);
            this.filter2.TabIndex = 0;
            // 
            // InsertEngineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.cbShuffle);
            this.Controls.Add(this.nmBleedBack);
            this.Controls.Add(this.filter1);
            this.Controls.Add(this.filter2);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "InsertEngineControl";
            this.Size = new System.Drawing.Size(302, 151);
            this.Tag = "color:dark1";
            ((System.ComponentModel.ISupportInitialize)(this.nmBleedBack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown nmBleedBack;
        private InstrCheckList filter2;
        private InstrCheckList filter1;
        public System.Windows.Forms.CheckBox cbShuffle;
    }
}
