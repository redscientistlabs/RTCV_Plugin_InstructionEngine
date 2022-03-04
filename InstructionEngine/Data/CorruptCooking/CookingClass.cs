using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace InstructionEngine.Data.CorruptCooking
{
    //Screw it, full shitpost class names
    public static class CookingClass
    {
        //TODO: test performance of switch vs dictionary for this use case
        //static Dictionary<string, RecipeStep> cookbook = new Dictionary<string, RecipeStep>();
        //public static void Init()
        //{
        //    //cookbook
        //}

        /// <summary>
        /// "Compile" into delegate list
        /// </summary>
        /// <param name="shoppingLists"></param>
        /// <returns></returns>
        public static List<Recipe> MakeRecipes(List<IngredientList> shoppingLists)
        {
            List<Recipe> recipes = new List<Recipe>();
            foreach (var list in shoppingLists)
            {
                List<RecipeStep> steps = new List<RecipeStep>();
                //steps.Add(new RecipeStep(Chef.ResetRecipe, null)); //Make state clean

                foreach (var ingredient in list.Ingredients)
                {
                    switch (ingredient.StepName)
                    {
                        case nameof(Chef.ResetRecipe):
                            steps.Add(new RecipeStep(Chef.ResetRecipe, null));
                            continue;
                        case nameof(Chef.FindAround):
                            steps.Add(new RecipeStep(Chef.FindAround, ingredient.Parameters.ToArray()));
                            continue;
                        case nameof(Chef.AddressWarp):
                            steps.Add(new RecipeStep(Chef.AddressWarp, ingredient.Parameters.ToArray()));
                            continue;
                        default:
                            break;
                    }
                }
                recipes.Add(new Recipe(list.Name, steps.ToArray(), list.NamedFilters));
            }

            return recipes;
        }

        //[RActionDef]


    }
}
