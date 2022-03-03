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
    public class IngredientList
    {
        public const string MAIN = nameof(MAIN);
        public string Name { get; set; }

        public Dictionary<string, List<InstructionDef>> NamedFilters { get; set; } = new Dictionary<string, List<InstructionDef>>();

        public List<Ingredient> Ingredients { get; set; }

        public IngredientList() { }

        public IngredientList(string name, List<InstructionDef> mainFilter, List<Ingredient> ingredients)
        {
            Name = name;
            NamedFilters[MAIN] = mainFilter;
            Ingredients = ingredients;
        }
    }
}
