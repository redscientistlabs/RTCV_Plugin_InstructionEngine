using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine
{
    public static class MathExtensions
    {
        public static int AssureNonZero(this int a, int defaultValue = 1)
        {
            return a != 0 ? a : defaultValue;
        }
    }
}
