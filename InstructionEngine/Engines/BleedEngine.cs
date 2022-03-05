using Ceras;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.UI;
using RTCV.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InstructionEngine.Data;
using RTCV.CorruptCore.Extensions;

namespace InstructionEngine.Engines
{
    partial class InstrEngine
    {
        public static int BleedForwards
        {
            get { return EngineSpec.Get<int>(nameof(BleedForwards)); }
            set { EngineSpec.Set(nameof(BleedForwards), value); }
        }

        public static int BleedBackwards
        {
            get { return EngineSpec.Get<int>(nameof(BleedBackwards)); }
            set { EngineSpec.Set(nameof(BleedBackwards), value); }
        }
        public static bool Smart
        {
            get { return EngineSpec.Get<bool>(nameof(Smart)); }
            set { EngineSpec.Set(nameof(Smart), value); }
        }
        public static bool UseUniqueRegisters
        {
            get { return EngineSpec.Get<bool>(nameof(UseUniqueRegisters)); }
            set { EngineSpec.Set(nameof(UseUniqueRegisters), value); }
        }
        public static bool ExcludeMatchedRegs
        {
            get { return EngineSpec.Get<bool>(nameof(ExcludeMatchedRegs)); }
            set { EngineSpec.Set(nameof(ExcludeMatchedRegs), value); }
        }
        static RegisterTarget backTarget = RegisterTarget.Output;
        public static RegisterTarget BackTarget
        {
            get { return EngineSpec.Get<RegisterTarget>(nameof(BackTarget)); }
            set { EngineSpec.Set(nameof(BackTarget), value); }
        }
        static RegisterTarget forwardTarget = RegisterTarget.Inputs;
        public static RegisterTarget ForwardTarget
        {
            get { return EngineSpec.Get<RegisterTarget>(nameof(ForwardTarget)); }
            set { EngineSpec.Set(nameof(ForwardTarget), value); }
        }


        static RegisterTarget backResTarget = RegisterTarget.Inputs;
        public static RegisterTarget BackResTarget
        {
            get { return EngineSpec.Get<RegisterTarget>(nameof(BackResTarget)); }
            set { EngineSpec.Set(nameof(BackResTarget), value); }
        }
        static RegisterTarget forwardResTarget = RegisterTarget.Output;
        public static RegisterTarget ForwardResTarget
        {
            get { return EngineSpec.Get<RegisterTarget>(nameof(ForwardResTarget)); }
            set { EngineSpec.Set(nameof(ForwardResTarget), value); }
        }

        public static List<InstructionDef> BleedFilterInstructions
        {
            get { return EngineSpec.Get<List<InstructionDef>>(nameof(BleedFilterInstructions)); }
            set { EngineSpec.Set(nameof(BleedFilterInstructions), value); }
        }

        private byte[] BleedCorrupt(List<InstructionDef> filter, List<InstructionDef> filter2, MemoryInterface mi, long address, int precision, int bleedBack, int bleedForward, bool smart, bool unique, bool exclude)
        {
            var data = PeekAndGetData(filter, mi, address, address + precision);
            if(data == null)
            {
                return null;
            }
            else
            {
                return Bleed(filter2, mi, data.FormFactor, smart, unique, exclude, bleedBack, bleedForward, address, precision, data.OriginalBytes);
            }
        }

        static HashSet<long> usedRegisters = new HashSet<long>();
        //TODO:Refactor
        private byte[] Bleed(List<InstructionDef> bleedEntries, MemoryInterface mi, FormFactor initialFormFactor,
            bool smart, bool unique, bool exclude,
            int bleedBack, int bleedForward, long addr, int precision, byte[] passthrough)
        {
            usedRegisters.Clear();
            long data = BytesTolong(passthrough); 

            if (smart)
            {
                long[] prev = GatherRegisters(addr, bleedEntries, mi, bleedBack, backTarget, precision, false);
                long[] future = GatherRegisters(addr, bleedEntries, mi, bleedForward, forwardTarget, precision, true);
                if (exclude)
                {
                    var excl = initialFormFactor.Extract(data, RegisterTarget.All);
                    if (excl == null) return null;

                    prev = prev.Where(x => !(excl.Contains(x))).ToArray();
                    future = future.Where(x => !(excl.Contains(x))).ToArray();
                }

                if (prev.Length == 0 || future.Length == 0)
                {
                    return null;
                }
                if(backResTarget == forwardResTarget)
                {
                    //Output to all
                    HashSet<long> output = new HashSet<long>();
                    foreach (var item in prev)
                    {
                        output.Add(item);
                    }
                    foreach (var item in future)
                    {
                        output.Add(item);
                    }


                    if (output.Count == 0)
                    {
                        return null;
                    }

                    data = initialFormFactor.Inject(data, RegisterTarget.All, output.ToArray(), unique);
                }
                else
                {
                    data = initialFormFactor.Inject(data, backResTarget, prev, unique);
                    data = initialFormFactor.Inject(data, forwardResTarget, future, unique);
                }
                
            }
            else
            {
                long[] prev = GatherRegisters(addr, bleedEntries, mi, bleedBack, backTarget, precision, false);
                long[] future = GatherRegisters(addr, bleedEntries, mi, bleedForward, forwardTarget, precision, true);
                
                if (exclude)
                {
                    var excl = initialFormFactor.Extract(data, RegisterTarget.All);
                    if (excl == null) return null;
                    prev = prev.Where(x => !(excl.Contains(x))).ToArray();
                    future = future.Where(x => !(excl.Contains(x))).ToArray();
                }

                HashSet<long> output = new HashSet<long>();
                foreach (var item in prev)
                {
                    output.Add(item);
                }
                foreach (var item in future)
                {
                    output.Add(item);
                }


                if (output.Count == 0 || prev.Length == 0 || future.Length == 0)
                {
                    return null;
                }
               
                data = initialFormFactor.Inject(data, RegisterTarget.All, output.ToArray(), unique);
            }

            byte[] outValue = BitConverter.GetBytes(data);

            Array.Resize(ref outValue, precision);

            if (outValue.Length < precision)
            {
                outValue = outValue.PadLeft(precision);
            }
            else if (outValue.Length > precision)
            {
                outValue.FlipBytes(); //Flip the bytes (stored as little endian)
                Array.Resize(ref outValue, precision); //Truncate
                outValue.FlipBytes(); //Flip them back
            }
            return outValue;
        }

        private static long[] GatherRegisters(long addr, List<InstructionDef> bleedEntries, MemoryInterface mi, int bleedAmt, RegisterTarget targets, int precision, bool forwards)
        {
            var outRegs = new HashSet<long>();
            for (int i = 0; i < bleedAmt; i++)
            {
                addr += forwards? precision : -precision;
                if (addr <= 0 || addr + precision >= mi.Size) break;
                else
                {
                    byte[] bytes = new byte[precision];

                    for (long j = 0; j < precision; j++)
                    {
                        bytes[j] = mi.PeekByte(addr + j);
                    }
                    if (mi.BigEndian)
                    {
                        bytes = bytes.FlipBytes();
                    }

                    long bytesAtAddr = BytesTolong(bytes);
                    string ff = null;
                    for (int j = 0; j < bleedEntries.Count; j++)
                    {
                        if (bleedEntries[j].Matches(bytesAtAddr))
                        {
                            ff = bleedEntries[j].FormFactorString;
                            break;
                        }
                    }

                    if (ff != null)
                    {
                        if (FormFactors.TryGetValue(ff, out FormFactor matchedFF))
                        {
                            if (matchedFF != null)
                            {
                                var regs = matchedFF.Extract(bytesAtAddr, targets);
                                if (regs != null)
                                {
                                    foreach (var reg in regs)
                                    {
                                        outRegs.Add(reg);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return outRegs.ToArray();
        }

    }
}
