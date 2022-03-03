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
        public static ulong AddressDifference(long addrFrom, long addrTo, ulong offset)
        {
            return (ulong)((long)offset + (addrTo-addrFrom));
        }




    }
}
