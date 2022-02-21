using Ceras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Data
{
    [Serializable]
    public enum RegisterTarget
    {
        All = 0,
        Output = 1,
        Inputs = 2,
    }
}
