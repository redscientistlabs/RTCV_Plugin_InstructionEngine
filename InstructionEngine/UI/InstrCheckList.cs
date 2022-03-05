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
    public partial class InstrCheckList : UserControl//, IColorize
    {
        public event Action<InstrCheckList> ItemCheck;

        ContextMenuStrip gearMenu = null;
        bool updatingCheckbox = false;
        public InstrCheckList()
        {
            InitializeComponent();
            gearMenu = new ContextMenuStrip();

            gearMenu.Items.Add(new ToolStripLabel("Checkbox Options")
            {
                 Font = new Font("Segoe UI", 8)
            });

            gearMenu.Items.Add("Check All", null, new EventHandler((ob, ev) =>
            {
                SetAllChecked(true);
            }));
            gearMenu.Items.Add("Uncheck All", null, new EventHandler((ob, ev) =>
            {
                SetAllChecked(false);
            }));
            //gearMenu.Items.Add("3D Checkboxes", null, new EventHandler((ob, ev) =>
            //{
            //    clbItems.ThreeDCheckBoxes = !clbItems.ThreeDCheckBoxes;
            //}));


            bGear.ContextMenuStrip = gearMenu;

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

        public void Init()
        {
            InitInstructions();
            cbArchitecture.SelectedIndexChanged -= CbArchitecture_SelectedIndexChanged;
            cbArchitecture.SelectedIndexChanged += CbArchitecture_SelectedIndexChanged;
            clbItems.ItemCheck -= ClbItems_ItemCheck;
            clbItems.ItemCheck += ClbItems_ItemCheck;
        }

        public void SetAllChecked(bool check = true)
        {
            updatingCheckbox = true;
            for (int i = 0; i < clbItems.Items.Count; i++)
            {
                clbItems.SetItemChecked(i, check);
            }
            updatingCheckbox = false;

            ItemCheck?.Invoke(this);
        }

        public List<InstructionDef> GetCheckedInstructions()
        {
            List<InstructionDef> instrs = new List<InstructionDef>();
            foreach (var instr in clbItems.CheckedItems)
            {
                instrs.Add((InstructionDef)instr);
            }
            return instrs;
        }


        public void InitInstructions()
        {

            foreach (var arcName in InstructionLib.InstructionGroups.Keys)
            {
                cbArchitecture.Items.Add(arcName);
            }

            if (InstructionLib.InstructionGroups.Count > 0)
            {
                var arc = InstructionLib.InstructionGroups.FirstOrDefault();
                UpdateCheckedListBoxes(arc.Value);
            }

            
            if(cbArchitecture.Items.Count > 0) 
            { 
                cbArchitecture.SelectedIndex = 0; 
            }

        }

        private void CbArchitecture_SelectedIndexChanged(object sender, EventArgs e)
        {
            var arc = InstructionLib.GetArc(cbArchitecture.SelectedItem.ToString());
            if (arc != null)
            {
                UpdateCheckedListBoxes(arc);
            }
        }

        private void ClbItems_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!updatingCheckbox)
            {
                ItemCheck?.Invoke(this);
            }
        }

        void UpdateCheckedListBoxes(List<InstructionDef> instructionDefs)
        {
            updatingCheckbox = true;
            clbItems.SuspendLayout();
            clbItems.Items.Clear();
            foreach (var item in instructionDefs)
            {
                clbItems.Items.Add(item, true);
            }
            clbItems.ResumeLayout();

            updatingCheckbox = false;
            ItemCheck?.Invoke(this);
        }

        void UpdateCheckAll()
        {

        }

        private void bGear_MouseDown(object sender, MouseEventArgs e)
        {
            gearMenu.Show(bGear, e.Location);//MouseDown
        }
    }
}
