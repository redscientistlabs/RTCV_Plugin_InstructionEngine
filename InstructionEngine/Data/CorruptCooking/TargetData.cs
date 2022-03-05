using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceras;
using RTCV.CorruptCore;

namespace InstructionEngine.Data.CorruptCooking
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class TargetData
    {
        public FormFactor OriginalFormFactor { get; private set; }
        public long OriginalData { get; private set; }
        public byte[] OriginalBytes { get; private set; }
        public FormFactor FormFactor { get; private set; }
        /// <summary>
        /// The thing you work on
        /// </summary>
        public long Data;
        public int Precision { get; private set; }
        public long Address { get; private set; }
        public MemoryInterface MemoryInterface { get; private set; }

        private InstructionDef instructionDef = null;

        public long? GetFieldData(string field)
        {
            return FormFactor.ExtractByName(Data, field);
        }

        [Exclude]
        public InstructionDef Instruction {
            get
            {
                return instructionDef;
            }
            set
            {
                Instruction = value;
                FormFactor = FormFactors.GetFormFactor(value.FormFactorString);
            }
        }

        public TargetData() { }

        public TargetData(InstructionDef instruction, long data, byte[] originalBytes, MemoryInterface memoryInterface, long address)
        {
            OriginalFormFactor = FormFactors.GetFormFactor(instruction.FormFactorString);
            FormFactor = OriginalFormFactor;
            instructionDef = instruction;
            Data = data;
            OriginalData = data;
            OriginalBytes = originalBytes;
            MemoryInterface = memoryInterface;
            Address = address;
            Precision = instruction.Precision;
        }

        public bool HasTag(string tag)
        {
            return FormFactor.HasTag(tag);
        }
        public bool RegistersHasTag(string tag)
        {
            return FormFactor.HasTag(tag);
        }

        internal bool HasRegisterNamed(string name)
        {
            return FormFactor.GetFieldByName(name) != null;
            //var regs = FormFactor.Fields;
            //foreach (var reg in regs)
            //{
            //    if (reg.Name == name) return true;
            //}
            ////else
            //return false;
        }
        internal FieldInfo GetRegisterNamed(string name)
        {
            return FormFactor.GetFieldByName(name);
        }
    }
}
