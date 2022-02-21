using InstructionEngine;
using InstructionEngine.Data;
using InstructionEngine.Engines;
using InstructionEngine.UI;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine
{
    /// <summary>
    /// This lies on the RTC(Server) side
    /// </summary>
    class PluginConnectorRTC : IRoutable
    {
        bool didInitialConnect = false;
        public PluginConnectorRTC()
        {
            LocalNetCoreRouter.registerEndpoint(this, PluginRouting.Endpoints.RTC_SIDE);
        }

        public object OnMessageReceived(object sender, NetCoreEventArgs e)
        {
            NetCoreAdvancedMessage message = e.message as NetCoreAdvancedMessage;
            switch (message.Type)
            {
                case PluginRouting.Commands.SYNC:
                    var data = message.objectValue as Dictionary<string, object>;
                    EngineSpec.Sync(data);
                    break;
                case PluginRouting.Commands.REQUEST_RESYNC:
                    if (didInitialConnect) { EngineSpec.Push(); }
                    else
                    {
                        didInitialConnect = true;

                        //Initial settings
                        EngineSpec.Suspend();
                        InstrEngine.Method = EngineMethod.Bleed;
                        InstrEngine.BleedBackwards = 3;
                        InstrEngine.BleedForwards = 3;
                        InstrEngine.Smart = true;
                        InstrEngine.UseUniqueRegisters = false;
                        InstrEngine.ExcludeMatchedRegs = false;
                        InstrEngine.ForwardTarget = RegisterTarget.Inputs;
                        InstrEngine.BackTarget = RegisterTarget.Output;
                        InstrEngine.BackResTarget = RegisterTarget.Inputs;
                        InstrEngine.ForwardResTarget = RegisterTarget.Output;
                        EngineSpec.ResumeAndPush();
                        //EngineSpec.Push();
                    }
                    break;
                default:
                    break;
            }
            return e.returnMessage;
        }
    }
}
