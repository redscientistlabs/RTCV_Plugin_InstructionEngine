using InstructionEngine.Data;
using InstructionEngine.Engines;
using InstructionEngine.UI;
using RTCV.Common;
using RTCV.NetCore;
using RTCV.PluginHost;
using RTCV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows.Forms;

namespace InstructionEngine
{
    [Export(typeof(IPlugin))]
    public class PluginCore : IPlugin, IDisposable
    {
        public string Name => "Instruction Engines";
        public string Description => "Description";

        public string Author => "NullShock78";

        public Version Version => new Version(1, 0, 0);

        /// <summary>
        /// Defines which sides will call Start
        /// </summary>
        public RTCSide SupportedSide => RTCSide.Both;
        internal static RTCSide CurrentSide = RTCSide.Both;

        internal static PluginConnectorEMU connectorEMU = null;
        internal static PluginConnectorRTC connectorRTC = null;

        internal static string DataPath => Path.Combine(RTCV.CorruptCore.RtcCore.PluginDir, "InstructionEngine");
        internal static string InstructionPath => Path.Combine(DataPath, "Instructions");
        internal static string FormFactorPath => Path.Combine(DataPath, "FormFactors");

        public void Dispose()
        {
        }

        public bool Start(RTCSide side)
        {
            Lua.LuaManager.EnsureInitialized();

            if (side == RTCSide.Client)
            {
                LoadInstructions();
                FormFactors.Init();
                connectorEMU = new PluginConnectorEMU();

                LocalNetCoreRouter.Route(PluginRouting.Endpoints.RTC_SIDE, PluginRouting.Commands.REQUEST_RESYNC, true);


                //var instructionDefs = new List<InstructionDef>();
                //if (Directory.Exists(PluginCore.DataPath))
                //{
                //    var files = Directory.GetFiles(DataPath);
                //    foreach (var file in files)
                //    {
                //        var entries = FilterReader.ReadEntries(file);
                //        if (entries != null)
                //        {
                //            instructionDefs.AddRange(entries);
                //        }
                //    }
                //    InstrEngine.FilterInstructions = instructionDefs;
                //}



            }
            else if (side == RTCSide.Server)
            {
                LoadInstructions();
                FormFactors.Init();
                connectorRTC = new PluginConnectorRTC();
                var form = new InstrEngineHolder();
                S.SET<InstrEngineHolder>(form);
                form.TopLevel = false;

                S.GET<CorruptionEngineForm>().RegisterPluginEngine(new InstrEngine(form));

                //Get connection status
                //Regather settings
            }
            CurrentSide = side;



            return true;
        }


        void LoadInstructions()
        {
            if (Directory.Exists(PluginCore.InstructionPath))
            {
                var files = Directory.GetFiles(PluginCore.InstructionPath);
                List<string> arcNames = new List<string>();
                foreach (var file in files)
                {
                    var entries = FilterReader.ReadEntries(file);
                    if (entries != null)
                    {
                        string arcName = Path.GetFileNameWithoutExtension(file).Trim('_', ' ');
                        arcNames.Add(arcName);
                        InstructionLib.Add(arcName, entries);
                    }
                }
            }
        }


        internal static string GetOtherSide()
        {
            return (PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Server ? PluginRouting.Endpoints.EMU_SIDE : PluginRouting.Endpoints.RTC_SIDE);
        }

        public bool StopPlugin()
        {
            return true;
        }
    }
}
