﻿using InstructionEngine.Data;
using InstructionEngine.Data.CorruptCooking;
using InstructionEngine.Engines;
using RTCV.CorruptCore;
using RTCV.CorruptCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine
{
    public static class InstrHelper
    {
        public static List<TargetData> GatherTargets(long addr, List<InstructionDef> filter, MemoryInterface mi, int lookAmt, int precision, bool forwards)
        {
            var outData = new List<TargetData>();
            for (int i = 0; i < lookAmt; i++)
            {
                addr += forwards ? precision : -precision;
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

                    long convertedData = InstrEngine.BytesTolong(bytes);
                    TargetData targData = null;
                    for (int j = 0; j < filter.Count; j++)
                    {
                        if (filter[j].Matches(convertedData))
                        {
                            targData = new TargetData(filter[j], convertedData, bytes, mi, addr);
                            break;
                        }
                    }

                    if (targData != null)
                    {
                    //    if(ffTags != null && ffTags.Length > 0)
                    //    {
                    //        foreach (var tag in ffTags)
                    //        {
                    //            if (targData.HasTag(tag)) goto Success;
                    //        }
                    //    }

                    //    if(registerTags != null && registerTags.Length > 0)
                    //    {
                    //        foreach (var tag in registerTags)
                    //        {
                    //            if (targData.HasTag(tag)) goto Success;
                    //        }
                    //    }

                    //    if (regNames != null && regNames.Length > 0)
                    //    {
                    //        foreach (var name in regNames)
                    //        {
                    //            if (targData.HasRegister(name)) goto Success;
                    //        }
                    //    }

                    //    continue;

                    //Success:
                        outData.Add(targData);
                    }

                    
                }
            }
            return outData;
        }


        public static byte[] LongToBytes(long data, int precision)
        {
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

        public static TargetData Filter(List<InstructionDef> filter, MemoryInterface mi, long addr, int precision)
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
            long convertedData = InstrEngine.BytesTolong(bytes);

            TargetData targData = null;
            for (int j = 0; j < filter.Count; j++)
            {
                if (filter[j].Matches(convertedData))
                {
                    targData = new TargetData(filter[j], convertedData, bytes, mi, addr);
                    break;
                }
            }

            return targData;
        }

    }
}
