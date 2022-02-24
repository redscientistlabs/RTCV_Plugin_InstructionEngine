using InstructionEngine.Data.Json;
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
        public FieldSavable[] Fields { get; set; }


        public FormFactorSavable() { }
        //public FormFactorSavable(string name, ulong outputRegister, params ulong[] inputRegisters)
        //{
        //    Name = name;
        //    OutputRegister = outputRegister;
        //    InputRegisters = inputRegisters;
        //}

        public static explicit operator FieldFormFactor(FormFactorSavable a)
        {
            return new FieldFormFactor(a.Fields.Select(x => (FieldInfo)x).ToArray());
        }
    }
}
