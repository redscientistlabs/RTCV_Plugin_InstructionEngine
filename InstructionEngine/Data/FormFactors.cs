using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Data
{
    internal class FormFactors
    {
        private static Dictionary<string, RegisterFormFactor> formFactors =
            new Dictionary<string, RegisterFormFactor>();

        private static Dictionary<string, string> groupByInfo =
            new Dictionary<string, string>();



        public static void InitPPC()
        {
            formFactors["PPC_A"] =
                    new RegisterFormFactor(
                        0b000000_11111_00000_00000_00000_000000, //D
                        0b000000_00000_11111_00000_00000_000000, //A
                        0b000000_00000_00000_11111_00000_000000);//B
            groupByInfo["PPC_A"] = "PPC";

            formFactors["PPC_AMUL"] =
                    new RegisterFormFactor(
                        0b000000_11111_00000_00000_00000_000000, //D
                        0b000000_00000_11111_00000_00000_000000, //A
                        0b000000_00000_00000_00000_11111_000000);//B
            groupByInfo["PPC_AMUL"] = "PPC";

            formFactors["PPC_A2"] =
                    new RegisterFormFactor(
                        0b000000_11111_00000_00000_00000_000000, //D
                        0b000000_00000_11111_00000_00000_000000, //A
                        0b000000_00000_00000_11111_00000_000000, //B
                        0b000000_00000_00000_00000_11111_000000);//C
            groupByInfo["PPC_A2"] = "PPC";

            formFactors["PPC_X"] =
                new RegisterFormFactor(
                        0b000000_11111_00000_00000_00000_000000,
                        0b000000_00000_00000_11111_00000_000000);
            groupByInfo["PPC_X"] = "PPC";

            formFactors["PPC_I"] =
                   new RegisterFormFactor(0b000000_111111111111111111111111_00);
            groupByInfo["PPC_I"] = "PPC";

        }

        public static RegisterFormFactor GetFormFactor(string key)
        {
            if(formFactors.TryGetValue(key, out RegisterFormFactor value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public static bool TryGetValue(string key, out RegisterFormFactor value)
        {
            return formFactors.TryGetValue(key, out value);
        }

    }
}
