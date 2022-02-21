using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Data
{
    internal static class InstructionLib
    {
        static Dictionary<string, List<InstructionDef>> InstructionGroups = new Dictionary<string, List<InstructionDef>>();

        //public static void Push()
        //{
        //    //Route a push
        //}
        public static void Sync(Dictionary<string, List<InstructionDef>> instrGroups)
        {
            InstructionGroups = instrGroups;
        }

    }
}
