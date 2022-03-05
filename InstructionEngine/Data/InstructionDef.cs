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
        long template;
        long reserved;

        public string FormFactorString { get; private set; }

        public FormFactor FormFactor => FormFactors.GetFormFactor(FormFactorString);

        public int Precision { get; private set; }

        public string Name { get; private set; }

        public long Template => template;
        public long ReservedMask => template;

        public InstructionDef(string name, string formFactor, long template, long reserved, int precision)
        {
            this.Name = name;
            this.FormFactorString = formFactor.ToUpper();
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

        public bool Matches(long data)
        {
            return template == (data & reserved);
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
