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

        public static void Init()
        {
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
