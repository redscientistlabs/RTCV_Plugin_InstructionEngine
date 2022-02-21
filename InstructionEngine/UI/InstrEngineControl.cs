namespace InstructionEngine.UI
{
    using System;
    using System.Reflection;
    using System.Drawing;
    using System.Windows.Forms;
    using InstructionEngine.Engines;
    using RTCV.Common;
    using RTCV.CorruptCore;
    using RTCV.UI;
    using RTCV.UI.Components.EngineConfig;
    using RTCV.UI.Modular;
    using InstructionEngine.Data;
    using System.IO;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class InstrEngineControl : ComponentForm, IColorize
    {

        //BindingList<ComboBoxItem<InstructionDef>> instructions = new BindingList<ComboBoxItem<InstructionDef>>();

        public InstrEngineControl() 
        {
            InitializeComponent();

            //cbVectorValueList.DataSource = null;
            //cbVectorLimiterList.DataSource = null;

            //cbVectorValueList.DisplayMember = "Name";
            //cbVectorLimiterList.DisplayMember = "Name";
            //cbVectorValueList.ValueMember = "Value";
            //cbVectorLimiterList.ValueMember = "Value";
            cbInstrMethod.DataSource = null;
            cbInstrMethod.Items.Add(EngineMethod.Bleed.ToString());
            cbInstrMethod.Items.Add(EngineMethod.ReggieRotate.ToString());
            cbInstrMethod.SelectedIndex = 0;


            cbInstrMethod.SelectedIndexChanged += CbInstrMethod_SelectedIndexChanged;

            InstrEngine.Method = EngineMethod.Bleed;
            InstrEngine.Smart = true;


            LoadInstructions();
            clbFilters.ItemCheck += ClbFilters_ItemCheck;
            clbFilters.CheckOnClick = true;

            cbBackTarg.Items.Add(RegisterTarget.All.ToString());
            cbForwardTarg.Items.Add(RegisterTarget.All.ToString());
            cbOutputForward.Items.Add(RegisterTarget.All.ToString());
            cbOutputBack.Items.Add(RegisterTarget.All.ToString());

            cbBackTarg.Items.Add(RegisterTarget.Inputs.ToString());
            cbForwardTarg.Items.Add(RegisterTarget.Inputs.ToString());
            cbOutputForward.Items.Add(RegisterTarget.Inputs.ToString());
            cbOutputBack.Items.Add(RegisterTarget.Inputs.ToString());

            cbBackTarg.Items.Add(RegisterTarget.Output.ToString());
            cbForwardTarg.Items.Add(RegisterTarget.Output.ToString());
            cbOutputForward.Items.Add(RegisterTarget.Output.ToString());
            cbOutputBack.Items.Add(RegisterTarget.Output.ToString());

            cbBackTarg.SelectedIndex = 2;
            cbForwardTarg.SelectedIndex = 1;
            cbOutputBack.SelectedIndex = 1;
            cbOutputForward.SelectedIndex = 2;
            //InstrEngine.BackResTarget = RegisterTarget.Inputs;
            //InstrEngine.ForwardResTarget = RegisterTarget.Output;
            cbBackTarg.SelectedIndexChanged += CbBackTarg_SelectedIndexChanged;
            cbForwardTarg.SelectedIndexChanged += CbForwardTarg_SelectedIndexChanged;
            cbOutputBack.SelectedIndexChanged += CbOutputBack_SelectedIndexChanged;
            cbOutputForward.SelectedIndexChanged += CbOutputForward_SelectedIndexChanged;
            //cbVectorValueList.SelectedIndexChanged += CbVectorValueList_SelectedIndexChanged;
            //Do this here as if it's stuck into the designer, it keeps defaulting out
            //cbVectorLimiterList.DataSource = new datasou
            //cbVectorValueList.DataSource = RtcCore.ValueListBindingSource;

        }

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

        private void ClbFilters_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            UpdateCheckedFilters();
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

        private EngineMethod EMFromString(string str)
        {
            switch (str)
            {
                case "ReggieRotate":
                    return EngineMethod.ReggieRotate;
                case "Bleed":
                    return EngineMethod.Bleed;
                default:
                    return EngineMethod.Bleed;
            }
        }

        void LoadInstructions()
        {
            
            if (Directory.Exists(PluginCore.DataPath))
            {
                var instructionDefs = new List<InstructionDef>();
                var files = Directory.GetFiles(PluginCore.DataPath);
                foreach (var file in files)
                {
                    var entries = FilterReader.ReadEntries(file);
                    if (entries != null)
                    {
                        instructionDefs.AddRange(entries);
                    }
                }

                foreach (var item in instructionDefs)
                {
                    clbFilters.Items.Add(item, true);
                }
                UpdateCheckedFilters();
            }
        }

        void UpdateCheckedFilters()
        {
            List<InstructionDef> instrs = new List<InstructionDef>();
            foreach (var instr in clbFilters.CheckedItems)
            {
                instrs.Add((InstructionDef)instr);
            }
            InstrEngine.FilterInstructions = instrs;
        }


        private void CbInstrMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = ((ComboBox)sender).SelectedItem.ToString();
            if (item != null)
            {
                InstrEngine.Method = EMFromString(item);
            }
        }

        public void ResyncEngineUI()
        {
            //throw new NotImplementedException();
        }

        private void cbSubtract_CheckedChanged(object sender, EventArgs e)
        {
            InstrEngine.Smart = cbOutputFuture.Checked;
        }

        private void nmBleedBack_ValueChanged(object sender, EventArgs e)
        {
            InstrEngine.BleedBackwards = (int)nmBleedBack.Value;
        }

        private void nmBleedForward_ValueChanged(object sender, EventArgs e)
        {
            InstrEngine.BleedForwards = (int)nmBleedForward.Value;
        }

        private void bPushUpdate_Click(object sender, EventArgs e)
        {
            EngineSpec.Push();
        }

        private void bDebugPush_Click(object sender, EventArgs e)
        {
            EngineSpec.Suspend();
            InstrEngine.BleedForwards = (int)nmBleedForward.Value;
            InstrEngine.BleedBackwards = (int)nmBleedBack.Value;
            InstrEngine.Smart = cbOutputFuture.Checked;
            InstrEngine.UseUniqueRegisters = cbUniqueRegisters.Checked;
            InstrEngine.ExcludeMatchedRegs = cbExclusive.Checked;
            InstrEngine.Method = EngineMethod.Bleed;

            InstrEngine.ForwardTarget = RTFromString(cbForwardTarg.SelectedItem.ToString());
            InstrEngine.BackTarget = RTFromString(cbBackTarg.SelectedItem.ToString());
            InstrEngine.ForwardResTarget = RTFromString(cbOutputForward.SelectedItem.ToString());
            InstrEngine.BackResTarget = RTFromString(cbOutputBack.SelectedItem.ToString());
            UpdateCheckedFilters();

            EngineSpec.ResumeAndPush();
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
