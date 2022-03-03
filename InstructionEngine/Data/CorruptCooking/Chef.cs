using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Data.CorruptCooking
{
    public static class Chef
    {
        static Random rand = new Random();
        public const char SPLIT_PEAS = '|';

        public static string FreezeArray<T>(T[] input)
        {
            return string.Join(Chef.SPLIT_PEAS.ToString(), Array.ConvertAll(input, x => x.ToString()));
        }

        public static int[] DefrostIntArray(string input)
        {
            return Array.ConvertAll(input.Split(Chef.SPLIT_PEAS), x => int.Parse(x));
        }


        [ChefActionInfo("Reset Recipe")]
        public static bool ResetRecipe(Recipe recipe, string[] parameters)
        {
            recipe.Reset();
            return true;
        }


        [ChefActionInfo("Gather Around")]
        [ChefActionParam(0, "Back Amt.", typeof(int))]
        [ChefActionParam(1, "Forward Amt.", typeof(int))]
        [ChefActionParam(2, "Back Filter", typeof(List<InstructionDef>))] //Basically string
        [ChefActionParam(3, "Forward Filter", typeof(List<InstructionDef>))] //Basically string
        //[ChefActionParam(4, "With FF Tags", typeof(string[]))] //Basically string
        //[ChefActionParam(5, "With Reg. Tags", typeof(string[]))] //Basically string
        //[ChefActionParam(6, "With Reg. Name", typeof(string[]))] //Basically string

        public static bool FindAround(Recipe recipe, string[] parameters)
        {
            int back = int.Parse(parameters[0]);
            int forward = int.Parse(parameters[1]);
            string backFilter = parameters[2];
            string forwardFilter = parameters[3];
            //string[] withTags = parameters[4].Split(SPLIT_PEAS);
            //string[] withRegisterTags = parameters[5].Split(SPLIT_PEAS);
            //string[] withRegisterName = parameters[6].Split(SPLIT_PEAS);
            bool failOnNone = false;

            var backTargs = ChefAssistant.GatherTargets(recipe.FilterTarget.Address, recipe.NamedFilters[backFilter], recipe.FilterTarget.MemoryInterface, back, recipe.FilterTarget.Precision, false);
            var forwardTargs = ChefAssistant.GatherTargets(recipe.FilterTarget.Address, recipe.NamedFilters[forwardFilter], recipe.FilterTarget.MemoryInterface, forward, recipe.FilterTarget.Precision, true);

            if (failOnNone && (backTargs.Count + forwardTargs.Count == 0))
            {
                return false;
            }
            else
            {
                recipe.FoundTargets.AddRange(backTargs);
                recipe.FoundTargets.AddRange(forwardTargs);
                return true;
            }
        }


        //public static bool FindAround(Recipe recipe, string[] parameters)
        //{

        //}

        //[ChefActionInfo("Found (Reg Name) To Target (Reg Names)")]
        //public static bool MoveFoundToTargetByName(Recipe recipe, string[] parameters)
        //{
        //    //string targetNames =

        //    recipe.Reset();
        //    return true;
        //}

        [ChefActionInfo("AddressWarp")]
        [ChefActionParam(0, "From Reg.", typeof(string))]
        [ChefActionParam(1, "To Reg", typeof(string))]
        //[ChefActionParam(2, "Warp Min", typeof(int))]
        //[ChefActionParam(3, "Warp Max", typeof(int))]
        public static bool AddressWarp(Recipe recipe, string[] parameters)
        {
            int targCount = recipe.FoundTargets.Count;
            if (targCount == 0) return false;

            string fromRegTag = parameters[0];

            string toRegTag = parameters[1];
            if (!recipe.FilterTarget.HasRegister(toRegTag)) return false;

            var ftarg = recipe.FoundTargets.Where(x => x.HasRegister(fromRegTag)).ToArray();
            if (ftarg.Length == 0) return false;
            targCount = ftarg.Length;

            //int min = int.Parse(parameters[2]);
            //int max = int.Parse(parameters[3]);


            var from = ftarg[rand.Next(targCount)];
            ulong extr = from.FormFactor.ExtractByName(from.Data, fromRegTag);
            if(0b10000000000000ul )
            
            long diff = recipe.FilterTarget


            return true;
        }

        public static bool ResetFoundTargets(Recipe recipe, string[] parameters)
        {
            recipe.FoundTargets.Clear();
            return true;
        }

        public static bool ResetAllTargets(Recipe recipe, string[] parameters)
        {
            recipe.FoundTargets.Clear();
            recipe.NamedTargets.Clear();
            return true;
        }


    }
}
