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

    public partial class InstrEngineHolder : ComponentForm, IColorize
    {

        BleedEngineControl bleedEngineControl;
        InsertEngineControl insertEngineControl;

        UserControl curSettingsC = null;
        IInstrEngineSettings currentSettings = null;
        public InstrEngineHolder() 
        {
            InitializeComponent();

            cbInstrMethod.DataSource = null;
            var methodNames = Enum.GetNames(typeof(EngineMethod));
            foreach (var mname in methodNames)
            {
                cbInstrMethod.Items.Add(mname);
            }
            cbInstrMethod.SelectedIndex = 0;


            cbInstrMethod.SelectedIndexChanged += CbInstrMethod_SelectedIndexChanged;

            InstrEngine.Method = EngineMethod.Bleed;
            bleedEngineControl = new BleedEngineControl();
            insertEngineControl = new InsertEngineControl();

            SetCurrentEngine(bleedEngineControl, bleedEngineControl);

        }

        private void SetCurrentEngine(Control ctrl, IInstrEngineSettings settings)
        {
            pSettings.SuspendLayout();
            pSettings.Controls.Clear();
            pSettings.Controls.Add(ctrl);
            ctrl.Dock = DockStyle.Fill;
            currentSettings = settings;
            EngineSpec.Suspend();
            currentSettings.Resync();
            EngineSpec.ResumeAndPush();
            Recolor();
        }

        internal void Resync()
        {
            EngineSpec.Suspend();
            InstrEngine.Method = EMFromString(cbInstrMethod.SelectedItem.ToString());
            //Resync current method
            currentSettings?.Resync();
            EngineSpec.ResumeAndPush();
        }


        private EngineMethod EMFromString(string str)
        {
            return (EngineMethod)Enum.Parse(typeof(EngineMethod), str);
        }

        private void CbInstrMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = ((ComboBox)sender).SelectedItem.ToString();
            if (item != null)
            {
                EngineSpec.Suspend();
                var method = EMFromString(item);
                InstrEngine.Method = method;
                pSettings.SuspendLayout();
                switch (method)
                {
                    case EngineMethod.Bleed:
                        SetCurrentEngine(bleedEngineControl, bleedEngineControl);
                        break;
                    case EngineMethod.ReggieRotate:
                        SetCurrentEngine(bleedEngineControl, bleedEngineControl);
                        break;
                    case EngineMethod.Chef:
                        SetCurrentEngine(bleedEngineControl, bleedEngineControl);
                        break;
                    case EngineMethod.Inserter:
                        SetCurrentEngine(insertEngineControl, insertEngineControl);
                        break;
                    default:
                        break;
                }
                Resync();

                pSettings.ResumeLayout();

            }
        }

        private void bDebugPush_Click(object sender, EventArgs e)
        {
            Resync();
        }
    }
}
