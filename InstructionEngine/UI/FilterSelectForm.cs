using InstructionEngine.Data;
using RTCV.Common;
using RTCV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstructionEngine.UI
{
    public partial class FilterSelectForm : Form
    {
        public InstrCheckList FilterBox { get; private set; }
        public List<InstructionDef> Selected => FilterBox.GetCheckedInstructions();
        public FilterSelectForm()
        {
            InitializeComponent();
            FormClosing += FilterSelectForm_FormClosing;
        }

        public FilterSelectForm(InstrCheckList checkList, string name)
        {
            InitializeComponent();
            FilterBox = checkList ?? new InstrCheckList();
            pMain.SuspendLayout();
            pMain.Controls.Add(FilterBox);
            FilterBox.Dock = DockStyle.Fill;
            pMain.ResumeLayout();
            FormClosing += FilterSelectForm_FormClosing;
            Text = name;
        }


        public InstrCheckList ShowDialog(InstrCheckList checkList)
        {
            FilterBox = checkList ?? new InstrCheckList();
            pMain.SuspendLayout();
            pMain.Controls.Add(FilterBox);
            FilterBox.Dock = DockStyle.Fill;
            pMain.ResumeLayout();
            this.ShowDialog();
            return FilterBox;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FilterSelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            pMain.Controls.Clear();
        }

        private void cbOnTop_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = cbOnTop.Checked;
        }
    }
}
