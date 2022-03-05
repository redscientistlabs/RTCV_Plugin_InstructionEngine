using InstructionEngine.Data;
using InstructionEngine.Engines;
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
    public partial class BleedEngineControl : UserControl, IInstrEngineSettings//, IColorize
    {
        InstrCheckList filter1 = new InstrCheckList();
        InstrCheckList filter2 = new InstrCheckList();
        FilterSelectForm filterForm1 = null;
        FilterSelectForm filterForm2 = null;
        public BleedEngineControl()
        {
            InitializeComponent();
            filter1.Init();
            filter1.ItemCheck += (f) => { InstrEngine.FilterInstructions = f.GetCheckedInstructions(); };
            filter2.Init();
            filter2.ItemCheck += (f) => { InstrEngine.BleedFilterInstructions = f.GetCheckedInstructions(); };

            var regTargs = Enum.GetNames(typeof(RegisterTarget));
            foreach (var regTarg in regTargs)
            {
                cbBackTarg.Items.Add(regTarg);
                cbForwardTarg.Items.Add(regTarg);
                cbOutputForward.Items.Add(regTarg);
                cbOutputBack.Items.Add(regTarg);
            }
            cbBackTarg.SelectedIndex = 2;
            cbForwardTarg.SelectedIndex = 1;
            cbOutputBack.SelectedIndex = 1;
            cbOutputForward.SelectedIndex = 2;

            cbBackTarg.SelectedIndexChanged += CbBackTarg_SelectedIndexChanged;
            cbForwardTarg.SelectedIndexChanged += CbForwardTarg_SelectedIndexChanged;
            cbOutputBack.SelectedIndexChanged += CbOutputBack_SelectedIndexChanged;
            cbOutputForward.SelectedIndexChanged += CbOutputForward_SelectedIndexChanged;

            //this.ControlRemoved += Colz_ControlRemoved;
            //this.ControlAdded += Colz_ControlAdded;
        }

        //private void Colz_ControlAdded(object sender, ControlEventArgs e)
        //{
        //    RTCV.Common.S.RegisterColorizable(this);
        //    Recolor();
        //}

        //private void Colz_ControlRemoved(object sender, ControlEventArgs e)
        //{
        //    RTCV.Common.S.DeregisterColorizable(this);
        //}

        //public void Recolor()
        //{
        //    Colors.SetRTCColor(Colors.GeneralColor, this);
        //}

        private void CbOutputForward_SelectedIndexChanged(object sender, EventArgs e)
        {
            InstrEngine.ForwardResTarget = RTFromString(cbOutputForward.SelectedItem.ToString());
        }

        private void CbOutputBack_SelectedIndexChanged(object sender, EventArgs e)
        {
            InstrEngine.BackResTarget = RTFromString(cbOutputBack.SelectedItem.ToString());
        }

        private void CbForwardTarg_SelectedIndexChanged(object sender, EventArgs e)
        {
            InstrEngine.ForwardTarget = RTFromString(cbForwardTarg.SelectedItem.ToString());
        }

        private void CbBackTarg_SelectedIndexChanged(object sender, EventArgs e)
        {
            InstrEngine.BackTarget = RTFromString(cbBackTarg.SelectedItem.ToString());
        }

        private RegisterTarget RTFromString(string str)
        {
            switch (str)
            {
                case "Output":
                    return RegisterTarget.Output;
                case "Inputs":
                    return RegisterTarget.Inputs;
                default:
                    return RegisterTarget.All;
            }
        }

        public void Resync()
        {
            InstrEngine.BleedForwards = (int)nmBleedForward.Value;
            InstrEngine.BleedBackwards = (int)nmBleedBack.Value;
            InstrEngine.Smart = cbOutputFuture.Checked;
            InstrEngine.UseUniqueRegisters = cbUniqueRegisters.Checked;
            InstrEngine.ExcludeMatchedRegs = cbExclusive.Checked;

            InstrEngine.ForwardTarget = RTFromString(cbForwardTarg.SelectedItem.ToString());
            InstrEngine.BackTarget = RTFromString(cbBackTarg.SelectedItem.ToString());
            InstrEngine.ForwardResTarget = RTFromString(cbOutputForward.SelectedItem.ToString());
            InstrEngine.BackResTarget = RTFromString(cbOutputBack.SelectedItem.ToString());

            InstrEngine.FilterInstructions = filter1.GetCheckedInstructions();
            InstrEngine.BleedFilterInstructions = filter2.GetCheckedInstructions();

        }

        private void bFilter2_Click(object sender, EventArgs e)
        {
            if(filterForm2 == null || filterForm2.IsDisposed)
            {
                filterForm2 = new FilterSelectForm(filter2, "DONORS");
                filterForm2.FormClosed += (o,f) => { filterForm2 = null; };
            }
            filterForm2.Show();
        }

        private void bFilter1_Click(object sender, EventArgs e)
        {
            if (filterForm1 == null || filterForm1.IsDisposed)
            {
                filterForm1 = new FilterSelectForm(filter1, "TARGET");
                filterForm1.FormClosed += (o, f) => { filterForm1 = null; };
                filterForm1.Show();
            }
            else
            {
                filterForm1.BringToFront();
            }
        }

        private void cbOutputFuture_CheckedChanged(object sender, EventArgs e)
        {
            InstrEngine.Smart = cbOutputFuture.Checked;
        }

        private void cbUniqueRegisters_CheckedChanged(object sender, EventArgs e)
        {
            InstrEngine.UseUniqueRegisters = cbUniqueRegisters.Checked;
        }

        private void cbExclusive_CheckedChanged(object sender, EventArgs e)
        {
            InstrEngine.ExcludeMatchedRegs = cbExclusive.Checked;
        }

    }
}
