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
        private static Dictionary<string, FormFactor> formFactors =
            new Dictionary<string, FormFactor>();

        public static void Init()
        {
            if (Directory.Exists(PluginCore.FormFactorPath))
            {
                var files = Directory.GetFiles(PluginCore.FormFactorPath);
                //List<string> arcNames = new List<string>();
                foreach (var file in files)
                {
                    //string groupName = Path.GetFileNameWithoutExtension(file);
                    var entries = JsonConvert.DeserializeObject<JsArr>(File.ReadAllText(file));//, new FormFactorFieldConverter());
                    foreach (var entry in entries.FormFactors)
                    {
                        formFactors[entry.Name] = (FormFactor)entry;
                    }
                }
            }
        }

        public static FormFactor GetFormFactor(string key)
        {
            if(formFactors.TryGetValue(key, out FormFactor value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public static bool TryGetValue(string key, out FormFactor value)
        {
            return formFactors.TryGetValue(key, out value);
        }

    }
}
