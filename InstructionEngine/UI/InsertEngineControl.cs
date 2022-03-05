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
    public partial class InsertEngineControl : UserControl, IInstrEngineSettings, IColorize
    {
        public InsertEngineControl()
        {
            InitializeComponent();
            filter1.Init();
            filter1.ItemCheck += (o) => { InsertEngine.Filter = o.GetCheckedInstructions(); };
            filter2.Init();
            filter2.ItemCheck += (o) => { InsertEngine.Filter2 = o.GetCheckedInstructions(); };


            this.ControlRemoved += Colz_ControlRemoved;
            this.ControlAdded += Colz_ControlAdded;
        }

        private void Colz_ControlAdded(object sender, ControlEventArgs e)
        {
            RTCV.Common.S.RegisterColorizable(this);
            Recolor();
        }

        private void Colz_ControlRemoved(object sender, ControlEventArgs e)
        {
            RTCV.Common.S.DeregisterColorizable(this);
        }

        public void Recolor()
        {
            Colors.SetRTCColor(Colors.GeneralColor, this);
        }



        public void Resync()
        {
            InsertEngine.Filter = filter1.GetCheckedInstructions();
            InsertEngine.Filter2 = filter2.GetCheckedInstructions();
            InsertEngine.Shuffle = cbShuffle.Checked;
        }

        private void cbShuffle_CheckedChanged(object sender, EventArgs e)
        {
            InsertEngine.Shuffle = cbShuffle.Checked;
        }
    }
}
