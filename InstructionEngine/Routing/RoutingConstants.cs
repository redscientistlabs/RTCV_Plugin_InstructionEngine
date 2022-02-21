using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine
{
    internal static class PluginRouting
    {
        internal const string PREFIX = "InstructionEngine";
        internal static class Endpoints
        {

            public const string EMU_SIDE = PREFIX + "_" + "EMU";
            public const string RTC_SIDE = PREFIX + "_" + "RTC";
        }

        /// <summary>
        /// Add your commands here
        /// </summary>
        internal static class Commands
        {
            public const string SET_LIMITER = PREFIX + "_" + nameof(SET_LIMITER);
            public const string SET_VALUE = PREFIX + "_" + nameof(SET_VALUE);
            public const string SET_SUB = PREFIX + "_" + nameof(SET_SUB);
            public const string REQUEST_RESYNC = PREFIX + "_" + nameof(REQUEST_RESYNC);
            public const string SYNC = PREFIX + "_" + nameof(SYNC);
        }
    }
}
