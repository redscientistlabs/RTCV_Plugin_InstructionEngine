﻿using Ceras;
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

        private byte[] BleedCorrupt(List<InstructionDef> fi, MemoryInterface mi, long address, int precision, int bleedBack, int bleedForward, bool smart, bool unique, bool exclude)
        {
            var data = PeekAndGetBytes(fi, mi, address, address + precision);
            if(data.formFactor == null || data.bytes == null)
            {
                return null;
            }
            else
            {
                return Bleed(fi, mi, data.formFactor, smart, unique, exclude, bleedBack, bleedForward, address, precision, data.bytes);
            }
        }


        public static MemoryInterface mi;
        static HashSet<ulong> usedRegisters = new HashSet<ulong>();
        //TODO:Refactor
        private byte[] Bleed(List<InstructionDef> bleedEntries, MemoryInterface mi, RegisterFormFactor initialFormFactor, bool smart, bool unique, bool exclude, int bleedBack, int bleedForward, long addr, int precision, byte[] passthrough)
        {
            usedRegisters.Clear();
            ulong data = BytesToUlong(passthrough); 

            if (smart)
            {
                ulong[] prev = GatherRegisters(addr, bleedEntries, mi, bleedBack, backTarget, precision, false);
                ulong[] future = GatherRegisters(addr, bleedEntries, mi, bleedForward, forwardTarget, precision, true);
                if (exclude)
                {
                    var excl = initialFormFactor.Extract(data, RegisterTarget.Inputs);
                    prev = prev.Where(x => !(excl.Contains(x))).ToArray();
                    future = future.Where(x => !(excl.Contains(x))).ToArray();
                }

                if (prev.Length == 0 || future.Length == 0)
                {
                    return null;
                }
                if(backResTarget == forwardResTarget)
                {
                    HashSet<ulong> output = new HashSet<ulong>();
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
                ulong[] prev = GatherRegisters(addr, bleedEntries, mi, bleedBack, backTarget, precision, false);
                ulong[] future = GatherRegisters(addr, bleedEntries, mi, bleedForward, forwardTarget, precision, true);
                
                if (exclude)
                {
                    var excl = initialFormFactor.Extract(data, RegisterTarget.Inputs);
                    prev = prev.Where(x => !(excl.Contains(x))).ToArray();
                    future = future.Where(x => !(excl.Contains(x))).ToArray();
                }

                HashSet<ulong> output = new HashSet<ulong>();
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

        private static ulong[] GatherRegisters(long addr, List<InstructionDef> bleedEntries, MemoryInterface mi, int bleedAmt, RegisterTarget targets, int precision, bool forwards)
        {
            var outRegs = new HashSet<ulong>();
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

                    ulong bytesAtAddr = BytesToUlong(bytes);
                    string ff = null;
                    for (int j = 0; j < bleedEntries.Count; j++)
                    {
                        if (bleedEntries[j].Matches(bytesAtAddr))
                        {
                            ff = bleedEntries[j].FormFactor;
                            break;
                        }
                    }

                    if (ff != null)
                    {
                        if (FormFactors.TryGetValue(ff, out RegisterFormFactor matchedFF))
                        {
                            if (matchedFF != null)
                            {
                                var regs = //OutputFuture?
                                           matchedFF.Extract(bytesAtAddr, targets);//, Engines.InstrEngine.Subtract ? (ulong)((i + 1) * precision) : 0); //:
                                //matchedFF.ExtractAll(bytesAtAddr, Engines.InstrEngine.Subtract ? (ulong)((i + 1) * precision) : 0);
                                foreach (var reg in regs)
                                {
                                    outRegs.Add(reg);
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