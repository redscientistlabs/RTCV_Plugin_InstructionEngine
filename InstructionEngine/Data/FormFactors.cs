using InstructionEngine.Data.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Data
{
    internal class FormFactors
    {
        private static Dictionary<string, FieldFormFactor> formFactors =
            new Dictionary<string, FieldFormFactor>();

        //private static Dictionary<string, List<RegisterFormFactor>> groups =
        //    new Dictionary<string, string>();



        public static void Init()
        {
            //formFactors["PPC_A"] =
            //        new RegisterFormFactor(
            //            0b000000_11111_00000_00000_00000_000000, //D
            //            0b000000_00000_11111_00000_00000_000000, //A
            //            0b000000_00000_00000_11111_00000_000000);//B
            //groupByInfo["PPC_A"] = "PPC";

            //formFactors["PPC_AMUL"] =
            //        new RegisterFormFactor(
            //            0b000000_11111_00000_00000_00000_000000, //D
            //            0b000000_00000_11111_00000_00000_000000, //A
            //            0b000000_00000_00000_00000_11111_000000);//B
            //groupByInfo["PPC_AMUL"] = "PPC";

            //formFactors["PPC_A2"] =
            //        new RegisterFormFactor(
            //            0b000000_11111_00000_00000_00000_000000, //D
            //            0b000000_00000_11111_00000_00000_000000, //A
            //            0b000000_00000_00000_11111_00000_000000, //B
            //            0b000000_00000_00000_00000_11111_000000);//C
            //groupByInfo["PPC_A2"] = "PPC";

            //formFactors["PPC_X"] =
            //    new RegisterFormFactor(
            //            0b000000_11111_00000_00000_00000_000000,
            //            0b000000_00000_00000_11111_00000_000000);
            //groupByInfo["PPC_X"] = "PPC";

            //formFactors["PPC_I"] =
            //       new RegisterFormFactor(0b000000_111111111111111111111111_00);
            //groupByInfo["PPC_I"] = "PPC";
            if (Directory.Exists(PluginCore.FormFactorPath))
            {
                var files = Directory.GetFiles(PluginCore.FormFactorPath);
                //List<string> arcNames = new List<string>();
                foreach (var file in files)
                {
                    //string groupName = Path.GetFileNameWithoutExtension(file);
                    var entries = JsonConvert.DeserializeObject<JsArr>(File.ReadAllText(file), new FormFactorConverter());
                    foreach (var entry in entries.FormFactors)
                    {
                        formFactors[entry.Name] = (FieldFormFactor)entry;
                        //groupByInfo[entry.Name] = groupName;
                    }
                    //if (entries != null)
                    //{
                    //    string arcName = Path.GetFileNameWithoutExtension(file).Trim('_', ' ');
                    //    arcNames.Add(arcName);
                    //    InstructionLib.Add(arcName, entries);
                    //}
                }
            }
        }

        public static FieldFormFactor GetFormFactor(string key)
        {
            if(formFactors.TryGetValue(key, out FieldFormFactor value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public static bool TryGetValue(string key, out FieldFormFactor value)
        {
            return formFactors.TryGetValue(key, out value);
        }

    }
}
