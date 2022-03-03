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
    public class FieldFormFactor
    {
        [Exclude]
        static Random rand = new Random();

        int outputRegisterIndex = -1;
        public FieldInfo OutputRegister => outputRegisterIndex > -1 ? registers[outputRegisterIndex] : null;
        int[] inputRegisterIndices = null;

        public HashSet<string> Tags { get; private set; } = new HashSet<string>();

        public List<FieldInfo> InputRegisters
        {
            get
            {
                if (inputRegisterIndices != null)
                {
                    List<FieldInfo> inputRegisters = new List<FieldInfo>();
                    foreach (var ind in inputRegisterIndices)
                    {
                        inputRegisters.Add(registers[ind]);
                    }
                    return inputRegisters;
                }
                else
                {
                    return null;
                }
            }

        }

        private FieldInfo[] registers;
        private FieldInfo[] fields;

        public FieldInfo[] Registers => registers;
        public FieldFormFactor() { }
        //public RegisterFormFactor(ulong outputRegister, params ulong[] registers)
        //{
        //    if (registers != null)
        //    {
        //        FFEs = new RegisterInfo[registers.Length+1];
        //        FFEs[0] = new RegisterInfo(outputRegister);
        //        for (int i = 0; i < registers.Length; i++)
        //        {
        //            FFEs[i+1] = new RegisterInfo(registers[i]);
        //        }
        //    }
        //    else 
        //    {
        //        FFEs = new RegisterInfo[1] { new RegisterInfo(outputRegister) };
        //    }
        //}

        public FieldFormFactor(FieldInfo[] fieldInfos)
        {
            registers = fieldInfos.Where(x=>x.HasTag("Register")).ToArray();
            fields = fieldInfos.Where(x => !x.HasTag("Register")).ToArray();
            int ind = -1;
            var output = registers.FirstOrDefault(x => { ind++; return x.HasTag("Output"); });
            if(output != default(FieldInfo))
            {
                outputRegisterIndex = ind;
            }

            ind = 0;
            List<int> inputIndices = new List<int>();
            var inputs = registers.Where(x => 
            { 
                bool isInput = x.HasTag("Input");
                if (isInput)
                {
                    inputIndices.Add(ind);
                }
                ind++;
                return isInput;
            });

            if (inputIndices.Count > 0)
            {
               inputRegisterIndices = inputIndices.ToArray();
            }
        }

        public ulong ExtractRandomRegister(ulong data)
        {
            return registers[rand.Next(registers.Length)].Extract(data);
        }

        public ulong RandomInject(ulong data, ulong register)
        {
            var ffe = registers[rand.Next(registers.Length)];
            return ffe.Inject(data, register);
        }

        public FieldInfo[] GetWithTag(string tag)
        {
            return registers.Where(x => x.HasTag(tag)).ToArray();
        }

        public bool HasTag(string tag)
        {
            return Tags.Contains(tag);
        }

        public bool RegistersHasTag(string tag)
        {
            foreach (var register in registers)
            {
                if (register.HasTag(tag)) return true;
            }
            return false;
        }

        public ulong InjectAt(int registerId, ulong data, ulong registerData)
        {
            try
            {
                var ffe = registers[registerId];
                return ffe.Inject(data, registerData);
            }
            catch
            {
                return data;
            }
        }

        public ulong InjectAll(ulong data, ulong[] registers)
        {
            for (int i = 0; i < this.registers.Length; i++)
            {
                data = this.registers[i].Inject(data, registers[rand.Next(registers.Length)]);
            }
            return data;
        }

        public ulong InjectOutput(ulong data, ulong[] registers)
        {
            if (outputRegisterIndex == -1) return data;
            //data = this.registers[OUTPUT_REG].Inject(data, registers[rand.Next(registers.Length)]);
            data = this.registers[outputRegisterIndex].Inject(data, registers[rand.Next(registers.Length)]);
            return data;
        }
        public ulong InjectInputs(ulong data, ulong[] registers)
        {
            //for (int i = 1; i < this.registers.Length; i++)
            //{
            //    data = this.registers[i].Inject(data, registers[rand.Next(registers.Length)]);
            //}
            if (inputRegisterIndices == null) return data;
            for (int i = 0; i < inputRegisterIndices.Length; i++)
            {
                data = this.registers[i].Inject(data, registers[rand.Next(registers.Length)]);
            }
            return data;
        }

        public ulong InjectAllUnique(ulong data, ulong[] registersIn)
        {
            List<ulong> registers = new List<ulong>(registersIn);
            for (int i = 0; i < this.registers.Length; i++)
            {
                if (registers.Count == 0) return data;
                int ind = rand.Next(registers.Count);
                data = this.registers[i].Inject(data, registers[ind]);
                registers.RemoveAt(ind);
            }
            return data;
        }

        public ulong InjectInputsUnique(ulong data, ulong[] registersIn)
        {
            List<ulong> registers = new List<ulong>(registersIn);
            for (int i = 1; i < this.registers.Length; i++)
            {
                if (registers.Count == 0) return data;
                int ind = rand.Next(registers.Count);
                data = this.registers[i].Inject(data, registers[ind]);
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
            ulong[] ret = new ulong[registers.Length];
            for (int i = 0; i < registers.Length; i++)
            {
                ret[i] = registers[i].Extract(data);
            }
            return ret;
        }

        public ulong[] ExtractInputs(ulong data, ulong sub = 0)
        {
            if(registers.Length <= 1)
            {
                return null;
            }

            ulong[] ret = new ulong[registers.Length-1];
            for (int i = 1; i < registers.Length; i++)
            {
                ret[i] = registers[i].Extract(data);
            }
            return ret;
        }

        public ulong[] ExtractWithTag(ulong data, string tag)
        {
            var regs = registers.Where(x => x.HasTag(tag)).ToArray();
            ulong[] ret = new ulong[regs.Length];
            for (int i = 0; i < regs.Length; i++)
            {
                ret[i] = regs[i].Extract(data);
            }
            return ret;
        }

        public ulong? ExtractByName(ulong data, string name)
        {
            var regs = registers.FirstOrDefault(x => x.Name == name);
            if(regs != null)
            {
                return regs.Extract(data);
            }
            else
            {
                return null;
            }
        }

        public ulong[] ExtractAllMatching(ulong data, string tag, int matchSize)
        {
            var regs = registers.Where(x => x.HasTag(tag) && x.BitsTotal == matchSize).ToArray();
            ulong[] ret = new ulong[regs.Length];
            for (int i = 0; i < regs.Length; i++)
            {
                ret[i] = regs[i].Extract(data);
            }
            return ret;
        }

        public ulong[] ExtractWithSize(ulong data, int matchSize)
        {
            var regs = registers.Where(x => x.BitsTotal == matchSize).ToArray();
            ulong[] ret = new ulong[regs.Length];
            for (int i = 0; i < regs.Length; i++)
            {
                ret[i] = regs[i].Extract(data);
            }
            return ret;
        }

        public ulong[] Extract(ulong data, RegisterTarget targetRegisters, int matchSize = -1)
        {
            ulong[] ret = null;
            switch (targetRegisters)
            {
                case RegisterTarget.All:
                    if (matchSize > 0)
                    {
                        var validRegs = registers.Where(x => x.BitsTotal == matchSize);
                        List<ulong> extrMatchSize = new List<ulong>();
                        foreach (var r in validRegs)
                        {
                            extrMatchSize.Add(r.Extract(data));
                        }
                        if(extrMatchSize.Count > 0)
                        {
                            ret = extrMatchSize.ToArray();
                        }
                    }
                    else
                    {
                        ret = new ulong[registers.Length];
                        for (int i = 0; i < registers.Length; i++)
                        {
                            ret[i] = registers[i].Extract(data);
                        }
                    }
                    break;
                case RegisterTarget.Output:
                    if (outputRegisterIndex < 0)
                    {
                        return null;
                    }
                    ret = new ulong[1] { registers[outputRegisterIndex].Extract(data) };
                    break;
                case RegisterTarget.Inputs:
                    ret = new ulong[registers.Length - 1];

                    //var retList = new List<ulong>();
                    if (registers.Length <= 1)
                    {
                        return null;
                    }
                    for (int i = 1; i < registers.Length; i++)
                    {
                        ret[i - 1] = registers[i].Extract(data);
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

}
