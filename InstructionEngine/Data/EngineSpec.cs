using Ceras;
using RTCV.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Data
{
    internal static class EngineSpec
    {
        //public static InstrEngineSettings Settings { get; set; } = new InstrEngineSettings();
        static Dictionary<string, object> spec = new Dictionary<string, object>();
        static bool suspended = false;

        public static T Get<T>(string key)
        {
            try
            {
                return (T)spec[key];
            }
            catch
            {
                return default(T);
            }
        }

        public static void Suspend()
        {
            suspended = true;
        }

        public static void Resume()
        {
            suspended = false;
        }
        public static void ResumeAndPush()
        {
            suspended = false;
            Push();
        }

        public static void SetWithoutPush(string key, object data)
        {
            spec[key] = data;
        }

        public static void Push()
        {
            LocalNetCoreRouter.Route(PluginCore.GetOtherSide(), PluginRouting.Commands.SYNC, spec, true);
        }

        public static void Sync(Dictionary<string,object> newSpec)
        {
            spec = newSpec;
        }

        public static void Set(string key, object data)
        {
            spec[key] = data;
            if(!suspended) Push();
        }


    }
}
