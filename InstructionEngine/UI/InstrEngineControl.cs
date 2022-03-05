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
    using Newtonsoft.Json;
    using InstructionEngine.Data.CorruptCooking;


    /// <summary>
    /// Old crappy code, do not use
    /// </summary>
    public partial class InstrEngineControl : ComponentForm, IColorize
    {

        bool updatingCheckboxes = false;

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
            var methodNames = Enum.GetNames(typeof(EngineMethod));
            foreach (var mname in methodNames)
            {
                cbInstrMethod.Items.Add(mname);
            }
            //cbInstrMethod.Items.Add(EngineMethod.Bleed.ToString());
            //cbInstrMethod.Items.Add(EngineMethod.ReggieRotate.ToString());
            //cbInstrMethod.Items.Add(EngineMethod.ReggieRotate.ToString());
            cbInstrMethod.SelectedIndex = 0;


            cbInstrMethod.SelectedIndexChanged += CbInstrMethod_SelectedIndexChanged;

            InstrEngine.Method = EngineMethod.Bleed;
            InstrEngine.Smart = true;


            LoadInstructions();
            cbArchitecture.SelectedIndexChanged += CbArchitecture_SelectedIndexChanged;


            clbFilters.CheckOnClick = true;
            clbFilters.ItemCheck += ClbFilters_ItemCheck;

            clbFilters2.CheckOnClick = true;
            clbFilters2.ItemCheck += ClbFilters2_ItemCheck;


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

            //string test = JsonConvert.SerializeObject(new FormFactorSavable("PPC_A", 0b000000_11111_00000_00000_00000_000000,
            //    0b000000_00000_11111_00000_00000_000000,
            //    0b000000_00000_00000_11111_00000_000000), Formatting.Indented, new FormFactorConverter());
            //Clipboard.SetText(test);
            //FormFactorSavable test2 = JsonConvert.DeserializeObject<FormFactorSavable>(test, new FormFactorConverter());
            //object o = new object();
            tbChefData.KeyPress += TbChefData_KeyPress;
        }

        

        internal void Resync()
        {
            EngineSpec.Suspend();
            InstrEngine.BleedForwards = (int)nmBleedForward.Value;
            InstrEngine.BleedBackwards = (int)nmBleedBack.Value;
            InstrEngine.Smart = cbOutputFuture.Checked;
            InstrEngine.UseUniqueRegisters = cbUniqueRegisters.Checked;
            InstrEngine.ExcludeMatchedRegs = cbExclusive.Checked;
            InstrEngine.Method = EMFromString(cbInstrMethod.SelectedItem.ToString());

            InstrEngine.ForwardTarget = RTFromString(cbForwardTarg.SelectedItem.ToString());
            InstrEngine.BackTarget = RTFromString(cbBackTarg.SelectedItem.ToString());
            InstrEngine.ForwardResTarget = RTFromString(cbOutputForward.SelectedItem.ToString());
            InstrEngine.BackResTarget = RTFromString(cbOutputBack.SelectedItem.ToString());
            UpdateCheckedFilters();
            UpdateCheckedFilters2();
            PushChef();
            EngineSpec.ResumeAndPush();
        }

        private void CbArchitecture_SelectedIndexChanged(object sender, EventArgs e)
        {
            var arc = InstructionLib.GetArc(cbArchitecture.SelectedItem.ToString());
            if(arc != null)
            {
                UpdateCheckedListBoxes(arc);
            }
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
            if (!updatingCheckboxes)
            {
                UpdateCheckedFilters();
            }
        }

        private void ClbFilters2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!updatingCheckboxes)
            {
                UpdateCheckedFilters2();
            }
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
            return (EngineMethod)Enum.Parse(typeof(EngineMethod), str);
            //switch (str)
            //{
            //    case "ReggieRotate":
            //        return EngineMethod.ReggieRotate;
            //    case "Bleed":
            //        return EngineMethod.Bleed;
            //    default:
            //        return EngineMethod.Bleed;
            //}
        }

        void LoadInstructions()
        {          
            //if (Directory.Exists(PluginCore.InstructionPath))
            //{
            //    var files = Directory.GetFiles(PluginCore.InstructionPath);
            //    List<string> arcNames = new List<string>();
            //    foreach (var file in files)
            //    {
            //        var entries = FilterReader.ReadEntries(file);
            //        if (entries != null)
            //        {
            //            string arcName = Path.GetFileNameWithoutExtension(file).Trim('_',' ');
            //            arcNames.Add(arcName);
            //            InstructionLib.Add(arcName, entries);                       
            //        }
            //    }

            //    foreach (var arc in arcNames)
            //    {
            //        cbArchitecture.Items.Add(arc);
            //    }

            //    cbArchitecture.SelectedIndex = 0;
            //    UpdateCheckedListBoxes(InstructionLib.GetArc(cbArchitecture.SelectedItem.ToString()));
            //    UpdateCheckedFilters();
            //}
        }

        void UpdateCheckedListBoxes(List<InstructionDef> instructionDefs)
        {
            updatingCheckboxes = true;
            //Filters 1
            clbFilters.SuspendLayout();
            clbFilters.Items.Clear();
            foreach (var item in instructionDefs)
            {
                clbFilters.Items.Add(item, true);
            }
            clbFilters.ResumeLayout();

            //Filters 2
            clbFilters2.SuspendLayout();
            clbFilters2.Items.Clear();
            foreach (var item in instructionDefs)
            {
                clbFilters2.Items.Add(item, true);
            }
            clbFilters2.ResumeLayout();

            updatingCheckboxes = false;
            UpdateCheckedFilters();
            UpdateCheckedFilters2();
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

        void UpdateCheckedFilters2()
        {
            List<InstructionDef> instrs = new List<InstructionDef>();
            foreach (var instr in clbFilters2.CheckedItems)
            {
                instrs.Add((InstructionDef)instr);
            }
            InstrEngine.BleedFilterInstructions = instrs;
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
            Resync();
        }

        private void cbUniqueRegisters_CheckedChanged(object sender, EventArgs e)
        {
            InstrEngine.UseUniqueRegisters = cbUniqueRegisters.Checked;
        }

        private void cbExclusive_CheckedChanged(object sender, EventArgs e)
        {
            InstrEngine.ExcludeMatchedRegs = cbExclusive.Checked;
        }

        private void TbChefData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                PushChef();
            }
        }


        void PushChef()
        {
            try
            {
                List<Ingredient> ingredients = new List<Ingredient>();

                var ingrStrs = tbChefData.Text.Split('|');
                foreach (var ingr in ingrStrs)
                {
                    var p = ingr.Split(',');
                    string[] pars = new string[p.Length - 1];
                    Array.Copy(p, 1, pars, 0, pars.Length);
                    ingredients.Add(new Ingredient(p[0], pars));
                }
                List<InstructionDef> instrs = new List<InstructionDef>();
                foreach (var instr in clbFilters.CheckedItems)
                {
                    instrs.Add((InstructionDef)instr);
                }

                InstrEngine.ChefParams = new IngredientList("Test", instrs, ingredients);
            }
            catch (Exception ex)
            {
                new object();
            }
        }
    }
}
