using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine
{
    public static class BitHelper
    {
        public static bool IsSet(long data, int bitFromLeft)
        {
            return ((data & (1L << bitFromLeft)) > 0);
        }

        public static long SetBit(long data, int bitFromLeft)
        {
            return data | 1L << bitFromLeft;
        }

    }
}
