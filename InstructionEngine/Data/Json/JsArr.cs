using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Data.Json
{
    [Serializable]
    public class JsArr
    {
        public List<FormFactorSavable> FormFactors { get; set; }
    }
}
