using Ceras;
using InstructionEngine.Data;
using RTCV.CorruptCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Data
{
    /// <summary>
    /// Represents an entry for bit filter list.
    /// </summary>
    [Serializable]
    [MemberConfig(TargetMember.All)]
    public class InstructionDef
    {
        ulong template;
        ulong reserved;

        public string FormFactor { get; private set; }
        public int Precision { get; private set; }

        public string Name { get; private set; }

        public InstructionDef(string name, string formFactor, ulong template, ulong reserved, int precision)
        {
            this.Name = name;
            this.FormFactor = formFactor.ToUpper();
            this.template = template;
            this.reserved = reserved;
            this.Precision = precision;
        }

        //Gotta do this to satisfy Ceras
        public InstructionDef()
        {
            this.template = 0;
            this.reserved = 0;
            Precision = 0;
        }

        public bool Matches(ulong data)
        {
            return template == (data & reserved);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
