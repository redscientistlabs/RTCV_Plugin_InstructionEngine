using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceras;

namespace InstructionEngine.Data.CorruptCooking
{
    public delegate bool RecipeAction(Recipe recipe, string[] parameters);

    public class RecipeStep
    {
        //public List<>
        private string[] parameters;
        private RecipeAction action;
        public RecipeStep() { }
        public RecipeStep(RecipeAction action, string[] parameters) 
        {
            this.action = action;
            this.parameters = parameters;
        }


        public bool Execute(Recipe recipe)
        {
            return action(recipe, parameters);
        }
    }
}
