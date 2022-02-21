using Ceras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Data
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class RegisterFormFactor
    {
        [Ceras.Exclude]
        static Random rand = new Random();

        public const int OUTPUT_REG = 0;

        private FFE[] FFEs;
        public RegisterFormFactor() { }
        public RegisterFormFactor(ulong outputRegister, params ulong[] registers)
        {
            if (registers != null)
            {
                FFEs = new FFE[registers.Length+1];
                FFEs[0] = new FFE(outputRegister);
                for (int i = 0; i < registers.Length; i++)
                {
                    FFEs[i+1] = new FFE(registers[i]);
                }
            }
            else 
            {
                FFEs = new FFE[1] { new FFE(outputRegister) };
            }

        }

        public ulong ExtractRandomRegister(ulong data)
        {
            return FFEs[rand.Next(FFEs.Length)].Extract(data);
        }

        public ulong RandomInject(ulong data, ulong register)
        {
            var ffe = FFEs[rand.Next(FFEs.Length)];
            return ffe.Inject(data, register);
        }

        public ulong InjectAt(int registerId, ulong data, ulong registerData)
        {
            try
            {
                var ffe = FFEs[registerId];
                return ffe.Inject(data, registerData);
            }
            catch
            {
                return data;
            }
        }

        public ulong InjectAll(ulong data, ulong[] registers)
        {
            for (int i = 0; i < FFEs.Length; i++)
            {
                data = FFEs[i].Inject(data, registers[rand.Next(registers.Length)]);
            }
            return data;
        }

        public ulong InjectOutput(ulong data, ulong[] registers)
        {
            data = FFEs[OUTPUT_REG].Inject(data, registers[rand.Next(registers.Length)]);
            return data;
        }
        public ulong InjectInputs(ulong data, ulong[] registers)
        {
            for (int i = 1; i < FFEs.Length; i++)
            {
                data = FFEs[i].Inject(data, registers[rand.Next(registers.Length)]);
            }
            return data;
        }

        public ulong InjectAllUnique(ulong data, ulong[] registersIn)
        {
            List<ulong> registers = new List<ulong>(registersIn);
            for (int i = 0; i < FFEs.Length; i++)
            {
                if (registers.Count == 0) return data;
                int ind = rand.Next(registers.Count);
                data = FFEs[i].Inject(data, registers[ind]);
                registers.RemoveAt(ind);
            }
            return data;
        }

        public ulong InjectInputsUnique(ulong data, ulong[] registersIn)
        {
            List<ulong> registers = new List<ulong>(registersIn);
            for (int i = 1; i < FFEs.Length; i++)
            {
                if (registers.Count == 0) return data;
                int ind = rand.Next(registers.Count);
                data = FFEs[i].Inject(data, registers[ind]);
                registers.RemoveAt(ind);
            }
            return data;
        }


        public ulong Inject(ulong data, RegisterTarget target, ulong[] registers, bool unique = false)
        {
            switch (target)
            {
                case RegisterTarget.All:
                    return unique ? InjectAll(data, registers) : InjectAllUnique(data, registers);
                case RegisterTarget.Output:
                    return InjectOutput(data, registers);
                case RegisterTarget.Inputs:
                    return unique ? InjectInputs(data, registers) : InjectInputsUnique(data, registers);
                default:
                    return 0xDEADBEEF;
            }
        }

        public ulong[] ExtractAll(ulong data, ulong sub = 0)
        {
            ulong[] ret = new ulong[FFEs.Length];
            for (int i = 0; i < FFEs.Length; i++)
            {
                ret[i] = FFEs[i].Extract(data, sub);
            }
            return ret;
        }

        public ulong[] ExtractInputs(ulong data, ulong sub = 0)
        {
            if(FFEs.Length <= 1)
            {
                return null;
            }

            ulong[] ret = new ulong[FFEs.Length-1];
            for (int i = 1; i < FFEs.Length; i++)
            {
                ret[i] = FFEs[i].Extract(data, sub);
            }
            return ret;
        }

        public ulong[] Extract(ulong data, RegisterTarget targetRegisters, ulong sub = 0)
        {
            ulong[] ret = null;
            switch (targetRegisters)
            {
                case RegisterTarget.All:
                    ret = new ulong[FFEs.Length];
                    for (int i = 0; i < FFEs.Length; i++)
                    {
                        ret[i] = FFEs[i].Extract(data, sub);
                    }
                    break;
                case RegisterTarget.Output:
                    if (FFEs.Length < 1)
                    {
                        return null;
                    }
                    ret = new ulong[1] { FFEs[OUTPUT_REG].Extract(data, sub) };
                    break;
                case RegisterTarget.Inputs:
                    ret = new ulong[FFEs.Length - 1];

                    //var retList = new List<ulong>();
                    if (FFEs.Length <= 1)
                    {
                        return null;
                    }
                    for (int i = 1; i < FFEs.Length; i++)
                    {
                        ret[i - 1] = FFEs[i].Extract(data, sub);
                        //retList.Add(FFEs[i].Extract(data, sub));
                    }
                    //ret = retList.ToArray();
                    break;
                default:
                    break;
            }
           
            return ret;
        }

    }
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class FFE
    {
        private int shift;
        private ulong registerMask;
        private ulong inverseRegisterMask;
        public FFE() { }

        public FFE(ulong registerMask) 
        { 
            this.registerMask = registerMask;
            inverseRegisterMask = ~registerMask;
            shift = CountBitsRight(registerMask);
        }
        private static int CountBitsRight(ulong rMask)
        {
            int shift = 0;
            ulong mask = 0b1;
            for (int i = 0; i < 64; i++)
            {
                if((rMask & mask) > 0)
                {
                    return shift;
                }
                else
                {
                    mask <<= 1;
                }
            }
            //No match
            return 64;
        }

        public ulong Extract(ulong other, ulong sub = 0)
        {
            return ((other & registerMask) >> shift) - sub;
        }

        public ulong Inject(ulong data, ulong registerData)
        {
            return (data & inverseRegisterMask) | (registerData << shift);
        }

        public ulong InjectMasked(ulong data, ulong registerData)
        {
            return (data & inverseRegisterMask) | ((registerData  << shift) & registerMask);
        }
    }
}
