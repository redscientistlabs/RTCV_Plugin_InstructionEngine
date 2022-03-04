using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstructionEngine.Data;

namespace InstructionEngine
{
    internal static class RHelper
    {
        public static long AddressDifference(long addrFrom, long addrTo, long offset)
        {
            return (long)((long)offset + (addrTo-addrFrom));
        }




    }
}
