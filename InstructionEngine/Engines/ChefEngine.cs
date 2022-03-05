using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunarBind;
using LunarBind.Standards;
using LunarBind.Yielding;
using LunarBind.Documentation;
using InstructionEngine.Data.CorruptCooking;
using InstructionEngine.Data;
using RTCV.CorruptCore;

namespace InstructionEngine.Engines
{

    class ChefEngine
    {
        internal static HookedStateScriptRunner Runner { get; private set; } = null;
        internal static LuaScriptStandard Standard { get; private set; } = null;
        internal static ScriptBindings Bindings { get; set; } = null;



        internal static void InitializeLua()
        {

            //GlobalScriptBindings.RegisterUserDataType(typeof(InstructionLib));

            if (PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Client)
            {
                Bindings = new ScriptBindings();
                GlobalScriptBindings.RegisterUserDataType(typeof(Recipe));
                GlobalScriptBindings.RegisterUserDataType(typeof(ChefData));
                GlobalScriptBindings.RegisterUserDataType(typeof(TargetData));
                GlobalScriptBindings.RegisterUserDataType(typeof(FormFactor));
                GlobalScriptBindings.RegisterUserDataType(typeof(Data.FieldInfo));
                GlobalScriptBindings.RegisterUserDataType(typeof(InstructionDef));
                GlobalScriptBindings.RegisterUserDataType(typeof(BlastUnit));
                GlobalScriptBindings.RegisterUserDataType(typeof(BlastLayer));
                GlobalScriptBindings.RegisterUserDataType(typeof(MemoryInterface));

                Bindings.BindTypeFuncs(typeof(Chef));


                Standard = new LuaScriptStandard(
                    new LuaFuncStandard("GenerateUnit", LuaFuncType.Function, true)
                    );
                Runner = new HookedStateScriptRunner(Standard, Bindings);
            }
            else
            {
                Standard = null;
            }


            if (PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Client) {
                Lua.LuaManager.EmulatorOnlyBindings.InitializeRunner(Runner);
            }
            else {
                Lua.LuaManager.UIOnlyBindings.InitializeRunner(Runner);
            }
        }

        public static void Defrost(List<InstrEngine> filter)
        {

        }

        public static byte[] Cook(MemoryInterface mi, long address, long precision)
        {



            return null;
            //var filterTarg = InstrEngine.PeekAndGetData(InstrEngine.FilterInstructions, mi, address, address + precision);
            //if (filterTarg == null) return null;
            //else
            //{
            //    Reset();
            //    FilterTarget = filterTarg;
            //    for (int i = 0; i < steps.Length; i++)
            //    {
            //        if (!steps[i].Execute(this))
            //        {
            //            return null;
            //        }
            //    }
            //}

            //return InstrEngine.LongToBytes(FilterTarget.Data, FilterTarget.Precision);
        }


    }

    
    public class ChefData
    {
        public TargetData Target { get; private set; } = null;
        public List<InstructionDef> Filter { get; private set; }
        public int Precision { get; private set; }
        public int MatchedAddress { get; private set; }
        public MemoryInterface MemoryInterface { get; private set; }
        internal Dictionary<string, List<InstructionDef>> NamedFilters { get; private set; } = new Dictionary<string, List<InstructionDef>>();

        //public void UpdateExternal(TargetData )
        //{
        //}
        

    }

}
