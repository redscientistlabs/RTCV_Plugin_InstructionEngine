using Ceras;
using InstructionEngine.Engines;
using RTCV.CorruptCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Data.CorruptCooking
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class Recipe
    {
        public string Name { get; private set; }

        public TargetData FilterTarget { get; private set; } = null;

        public List<InstructionDef> Filter { get; private set; } = new List<InstructionDef>();
        internal Dictionary<string, List<InstructionDef>> NamedFilters { get; private set; } = new Dictionary<string, List<InstructionDef>>();

        internal Dictionary<string, TargetData> NamedTargets { get; private set; } = new Dictionary<string, TargetData>();
        internal List<TargetData> FoundTargets { get; private set; } = new List<TargetData>();

        //Filter
        //Strategy
        RecipeStep[] steps = null;

        public Recipe(string name, RecipeStep[] steps, Dictionary<string, List<InstructionDef>> namedFilters)
        {
            this.Name = name;
            this.Filter = namedFilters[IngredientList.MAIN];
            this.steps = steps;
            NamedFilters = namedFilters;
            NamedFilters.Remove(IngredientList.MAIN);
        }

        public void Reset()
        {
            NamedTargets.Clear();
            FoundTargets.Clear();
        }

        /// <summary>
        /// If it returns null, sorry, you burnt your food.
        /// </summary>
        public byte[] Cook(MemoryInterface mi, long address, long precision)
        {
            var filterTarg = InstrEngine.PeekAndGetData(Filter, mi, address, address + precision);
            if (filterTarg == null) return null;
            else
            {
                FilterTarget = filterTarg;
                for (int i = 0; i < steps.Length; i++)
                {
                    if (!steps[i].Execute(this))
                    {
                        return null;
                    }
                }
            }

            return InstrEngine.UlongToBytes(FilterTarget.Data, FilterTarget.Precision);
        }


    }
}
