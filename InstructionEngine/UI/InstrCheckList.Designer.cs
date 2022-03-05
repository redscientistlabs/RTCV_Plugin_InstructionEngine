
namespace InstructionEngine.UI
{
    partial class InstrCheckList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstrCheckList));
            this.clbItems = new System.Windows.Forms.CheckedListBox();
            this.cbArchitecture = new System.Windows.Forms.ComboBox();
            this.bGear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clbItems
            // 
            this.clbItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.clbItems.CheckOnClick = true;
            this.clbItems.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbItems.ForeColor = System.Drawing.Color.White;
            this.clbItems.FormattingEnabled = true;
            this.clbItems.IntegralHeight = false;
            this.clbItems.Location = new System.Drawing.Point(0, 21);
            this.clbItems.Margin = new System.Windows.Forms.Padding(0);
            this.clbItems.Name = "clbItems";
            this.clbItems.Size = new System.Drawing.Size(150, 129);
            this.clbItems.TabIndex = 0;
            this.clbItems.Tag = "color:normal";
            // 
            // cbArchitecture
            // 
            this.cbArchitecture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbArchitecture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.cbArchitecture.DisplayMember = "Value";
            this.cbArchitecture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbArchitecture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbArchitecture.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbArchitecture.ForeColor = System.Drawing.Color.White;
            this.cbArchitecture.FormattingEnabled = true;
            this.cbArchitecture.IntegralHeight = false;
            this.cbArchitecture.Location = new System.Drawing.Point(0, 0);
            this.cbArchitecture.Margin = new System.Windows.Forms.Padding(0);
            this.cbArchitecture.MaxDropDownItems = 15;
            this.cbArchitecture.Name = "cbArchitecture";
            this.cbArchitecture.Size = new System.Drawing.Size(128, 21);
            this.cbArchitecture.TabIndex = 187;
            this.cbArchitecture.Tag = "color:normal";
            this.cbArchitecture.ValueMember = "Value";
            // 
            // bGear
            // 
            this.bGear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bGear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.bGear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bGear.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bGear.ForeColor = System.Drawing.Color.White;
            this.bGear.Image = ((System.Drawing.Image)(resources.GetObject("bGear.Image")));
            this.bGear.Location = new System.Drawing.Point(128, 0);
            this.bGear.Margin = new System.Windows.Forms.Padding(0);
            this.bGear.Name = "bGear";
            this.bGear.Size = new System.Drawing.Size(22, 21);
            this.bGear.TabIndex = 188;
            this.bGear.UseVisualStyleBackColor = false;
            this.bGear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bGear_MouseDown);
            // 
            // InstrCheckList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.bGear);
            this.Controls.Add(this.cbArchitecture);
            this.Controls.Add(this.clbItems);
            this.Name = "InstrCheckList";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbItems;
        public System.Windows.Forms.ComboBox cbArchitecture;
        private System.Windows.Forms.Button bGear;
    }
}
