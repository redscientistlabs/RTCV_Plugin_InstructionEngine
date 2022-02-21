using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Data
{
    [Serializable]
    public class FormFactorSavable
    {
        public string Name { get; set; }
        public ulong OutputRegister { get; set; }
        public ulong[] InputRegisters { get; set; }
        public FormFactorSavable() { }
        public FormFactorSavable(string name, ulong outputRegister, params ulong[] inputRegisters)
        {
            Name = name;
            OutputRegister = outputRegister;
            InputRegisters = inputRegisters;
        }

        public static explicit operator RegisterFormFactor(FormFactorSavable a)
        {
            return new RegisterFormFactor(a.OutputRegister, a.InputRegisters);
        }
    }
}
