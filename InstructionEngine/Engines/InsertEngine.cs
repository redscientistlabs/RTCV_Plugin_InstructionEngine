using InstructionEngine.Data;
using RTCV.CorruptCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Engines
{
    public class InsertEngine
    {
        public static List<InstructionDef> Filter
        {
            get { return EngineSpec.Get<List<InstructionDef>>($"{nameof(InsertEngine)}_{nameof(Filter)}"); } //Mooch off of instr engine for now
            set { EngineSpec.Set($"{nameof(InsertEngine)}_{nameof(Filter)}", value); }
        }
        public static List<InstructionDef> Filter2
        {
            get { return EngineSpec.Get<List<InstructionDef>>($"{nameof(InsertEngine)}_{nameof(Filter2)}"); } //Mooch off of bleed engine for now
            set { EngineSpec.Set($"{nameof(InsertEngine)}_{nameof(Filter2)}", value); }
        }

        //public static int AddressOffset
        //{
        //    get { return EngineSpec.Get<int>(nameof(AddressOffset)); } //Mooch off of instr engine for now
        //    set { EngineSpec.Set(nameof(AddressOffset), value); }
        //}

        //public static int LookBehind
        //{
        //    get { return EngineSpec.Get<int>(nameof(InstrEngine.BleedBackwards)); } //Mooch off of instr engine for now
        //    set { EngineSpec.Set(nameof(InstrEngine.BleedBackwards), value); }
        //}

        //public static bool LookFromNewAddr
        //{
        //    get { return EngineSpec.Get<bool>(nameof(InstrEngine.Smart)); } //Mooch off of instr engine for now
        //    set { EngineSpec.Set(nameof(InstrEngine.Smart), value); }
        //}

        public static bool Shuffle
        {
            get { return EngineSpec.Get<bool>($"{nameof(InsertEngine)}_{nameof(Shuffle)}"); } //Mooch off of instr engine for now
            set { EngineSpec.Set($"{nameof(InsertEngine)}_{nameof(Shuffle)}", value); }
        }

        static Random rand = new Random();
        public static byte[] Corrupt(MemoryInterface mi, long address, int precision)
        {
            List<InstructionDef> filter = Filter; 
            var orig = InstrEngine.PeekAndGetData(filter, mi, address, address + precision);
            if (orig != null)
            {
                List<InstructionDef> filter2 = Filter2;

                //TODO: AddressOffset
                //var addr = address;// + AddressOffset*precision;
                //if (addr >= mi.Size - precision) return null;

                //var orig = InstrHelper.Filter(filter2, mi, addr, precision);
                if (orig != null)
                {
                    var replacement = filter2[rand.Next(filter2.Count)];
                    long template = replacement.Template;
                    var origOutputs = orig.FormFactor.Extract(orig.Data, RegisterTarget.Output);
                    if(origOutputs == null || origOutputs.Length == 0) return null;
                    var origInputs = orig.FormFactor.Extract(orig.Data, RegisterTarget.Inputs);
                    var newInputs = replacement.FormFactor.GetFieldsByTag("Input");
                    if (origInputs is null || newInputs is null || origInputs.Length != newInputs.Count) return null;

                    template = replacement.FormFactor.Inject(template, RegisterTarget.Output, origOutputs);
                    template = replacement.FormFactor.Inject(template, RegisterTarget.Inputs, origInputs, true, !Shuffle);

                    return InstrHelper.LongToBytes(template, orig.Precision);
                    //InstrHelper.GatherTargets(LookFromNewAddr ? addr : address, f, mi, LookBehind, precision, false);
                    //template = replacement.FormFactor.Inject(template, RegisterTarget.Inputs, o
                    //f[ind].FormFactorString.
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

    }
}
