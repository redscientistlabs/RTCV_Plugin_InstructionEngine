using Ceras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Data.CorruptCooking
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class Ingredient
    {
        public string StepName { get; set; } = null;
        public List<string> Parameters { get; set; } = null;
        //public List<InstructionDef> Filter { get; set; } = null;

        public Ingredient() { }

        public Ingredient(string stepName, List<string> parameters)
        {
            StepName = stepName;
            Parameters = parameters;
        }
    }
}
