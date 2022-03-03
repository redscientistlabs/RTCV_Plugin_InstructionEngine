﻿using System;
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
        public FieldFormFactor OriginalFormFactor { get; private set; }
        public ulong OriginalData { get; private set; }
        public byte[] OriginalBytes { get; private set; }
        public FieldFormFactor FormFactor { get; private set; }
        public ulong Data { get; set; }
        public int Precision { get; private set; }
        public long Address { get; private set; }
        public MemoryInterface MemoryInterface { get; private set; }

        private InstructionDef instructionDef = null;

        public ulong? GetFieldData(string field)
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
                FormFactor = FormFactors.GetFormFactor(value.FormFactor);
            }
        }

        public TargetData() { }

        public TargetData(InstructionDef instruction, ulong data, byte[] originalBytes, MemoryInterface memoryInterface, long address)
        {
            OriginalFormFactor = FormFactors.GetFormFactor(instruction.FormFactor);
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

        internal bool HasRegister(string name)
        {
            var regs = FormFactor.Registers;
            foreach (var reg in regs)
            {
                if (reg.Name == name) return true;
            }
            //else
            return false;
        }
    }
}