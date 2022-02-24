using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Data.Json
{
    [Serializable]
    public class FieldSavable
    {
        public string Name { get; set; }
        public ulong Mask { get; set; }
        public string[] Tags { get; set; }
        public FieldSavable() { }

        public static explicit operator FieldInfo(FieldSavable a)
        {
            return new FieldInfo(a.Name, a.Mask, a.Tags);
        }
    }
}
