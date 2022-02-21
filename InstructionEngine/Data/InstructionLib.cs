using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Data
{
    internal static class InstructionLib
    {
        static Dictionary<string, List<InstructionDef>> InstructionGroups = new Dictionary<string, List<InstructionDef>>();


        //public static void Sync(Dictionary<string, List<InstructionDef>> instrGroups)
        //{
        //    InstructionGroups = instrGroups;
        //}

        public static void Add(string architecture, List<InstructionDef> instructions)
        {
            InstructionGroups[architecture] = new List<InstructionDef>(instructions);
        }

        public static List<InstructionDef> GetArc(string architecture)
        {
            if(InstructionGroups.TryGetValue(architecture, out List<InstructionDef> value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

    }
}
