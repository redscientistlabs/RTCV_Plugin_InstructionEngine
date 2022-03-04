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

        private int outputRegisterIndex = -1;
        public FieldInfo OutputRegister => outputRegisterIndex > -1 ? fields[outputRegisterIndex] : null;
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
                        inputRegisters.Add(fields[ind]);
                    }
                    return inputRegisters;
                }
                else
                {
                    return null;
                }
            }

        }

        private FieldInfo[] fields;
        private FieldInfo[] registers;
        //Number of fields will be low
        private Dictionary<string, FieldInfo> fieldNameDict = new Dictionary<string, FieldInfo>();
        private Dictionary<string, List<FieldInfo>> fieldTagDict = new Dictionary<string, List<FieldInfo>>();

        public FieldInfo[] Fields => fields;
        public FieldFormFactor() { }

        public FieldFormFactor(FieldInfo[] fieldInfos)
        {
            fields = fieldInfos;
            registers = fieldInfos.Where(x=> x.HasTag("Register")).ToArray();
            int ind = -1;
            var output = registers.FirstOrDefault(x => { ind++; return x.HasTag("Output"); });
            if (output != null)
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

            for (int i = 0; i < fieldInfos.Length; i++)
            {
                fieldNameDict[fieldInfos[i].Name] = fieldInfos[i]; //Dictionary for quick lookup
                string[] ftags = fieldInfos[i].Tags.ToArray();
                for (int j = 0; j < ftags.Length; j++)
                {
                    if(fieldTagDict.TryGetValue(ftags[j], out List<FieldInfo> fieldsWithTag))
                    {
                        fieldsWithTag.Add(fieldInfos[i]);
                    }
                    else
                    {
                        fieldTagDict[ftags[j]] = new List<FieldInfo>() { fieldInfos[i] };
                    }
                }
            }

        }

        public long ExtractRandomRegister(long data)
        {
            return fields[rand.Next(fields.Length)].Extract(data);
        }

        public long RandomInject(long data, long register)
        {
            var ffe = fields[rand.Next(fields.Length)];
            return ffe.Inject(data, register);
        }

        public FieldInfo[] GetWithTag(string tag)
        {
            return fields.Where(x => x.HasTag(tag)).ToArray();
        }

        public bool HasTag(string tag)
        {
            return Tags.Contains(tag);
        }

        public bool RegistersHasTag(string tag)
        {
            foreach (var register in fields)
            {
                if (register.HasTag(tag)) return true;
            }
            return false;
        }

        public long InjectAt(int registerId, long data, long registerData)
        {
            try
            {
                var ffe = fields[registerId];
                return ffe.Inject(data, registerData);
            }
            catch
            {
                return data;
            }
        }

        public long InjectAll(long data, long[] registers)
        {
            for (int i = 0; i < this.fields.Length; i++)
            {
                data = this.fields[i].Inject(data, registers[rand.Next(registers.Length)]);
            }
            return data;
        }

        public long InjectOutput(long data, long[] registers)
        {
            if (outputRegisterIndex == -1) return data;
            //data = this.registers[OUTPUT_REG].Inject(data, registers[rand.Next(registers.Length)]);
            data = this.fields[outputRegisterIndex].Inject(data, registers[rand.Next(registers.Length)]);
            return data;
        }
        public long InjectInputs(long data, long[] registers)
        {
            //for (int i = 1; i < this.registers.Length; i++)
            //{
            //    data = this.registers[i].Inject(data, registers[rand.Next(registers.Length)]);
            //}
            if (inputRegisterIndices == null) return data;
            for (int i = 0; i < inputRegisterIndices.Length; i++)
            {
                data = this.fields[i].Inject(data, registers[rand.Next(registers.Length)]);
            }
            return data;
        }

        public long InjectAllUnique(long data, long[] registersIn)
        {
            List<long> registers = new List<long>(registersIn);
            for (int i = 0; i < this.fields.Length; i++)
            {
                if (registers.Count == 0) return data;
                int ind = rand.Next(registers.Count);
                data = this.fields[i].Inject(data, registers[ind]);
                registers.RemoveAt(ind);
            }
            return data;
        }

        public long InjectInputsUnique(long data, long[] registersIn)
        {
            List<long> registers = new List<long>(registersIn);
            for (int i = 1; i < this.fields.Length; i++)
            {
                if (registers.Count == 0) return data;
                int ind = rand.Next(registers.Count);
                data = this.fields[i].Inject(data, registers[ind]);
                registers.RemoveAt(ind);
            }
            return data;
        }


        public long Inject(long data, RegisterTarget target, long[] registers, bool unique = false)
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

        public long[] ExtractAll(long data, long sub = 0)
        {
            long[] ret = new long[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                ret[i] = fields[i].Extract(data);
            }
            return ret;
        }

        public long[] ExtractInputs(long data, long sub = 0)
        {
            if(fields.Length <= 1)
            {
                return null;
            }

            long[] ret = new long[fields.Length-1];
            for (int i = 1; i < fields.Length; i++)
            {
                ret[i] = fields[i].Extract(data);
            }
            return ret;
        }

        public long[] ExtractWithTag(long data, string tag)
        {
            var regs = fields.Where(x => x.HasTag(tag)).ToArray();
            long[] ret = new long[regs.Length];
            for (int i = 0; i < regs.Length; i++)
            {
                ret[i] = regs[i].Extract(data);
            }
            return ret;
        }

        public long? ExtractByName(long data, string name)
        {
            var regs = fields.FirstOrDefault(x => x.Name == name);
            if(regs != null)
            {
                return regs.Extract(data);
            }
            else
            {
                return null;
            }
        }

        public long[] ExtractAllMatching(long data, string tag, int matchSize)
        {
            var regs = fields.Where(x => x.HasTag(tag) && x.BitsTotal == matchSize).ToArray();
            long[] ret = new long[regs.Length];
            for (int i = 0; i < regs.Length; i++)
            {
                ret[i] = regs[i].Extract(data);
            }
            return ret;
        }

        public long[] ExtractWithSize(long data, int matchSize)
        {
            var regs = fields.Where(x => x.BitsTotal == matchSize).ToArray();
            long[] ret = new long[regs.Length];
            for (int i = 0; i < regs.Length; i++)
            {
                ret[i] = regs[i].Extract(data);
            }
            return ret;
        }

        public long[] Extract(long data, RegisterTarget targetRegisters, int matchSize = -1)
        {
            long[] ret = null;
            switch (targetRegisters)
            {
                case RegisterTarget.All:
                    if (matchSize > 0)
                    {
                        var validRegs = fields.Where(x => x.BitsTotal == matchSize);
                        List<long> extrMatchSize = new List<long>();
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
                        ret = new long[fields.Length];
                        for (int i = 0; i < fields.Length; i++)
                        {
                            ret[i] = fields[i].Extract(data);
                        }
                    }
                    break;
                case RegisterTarget.Output:
                    if (outputRegisterIndex < 0)
                    {
                        return null;
                    }
                    ret = new long[1] { fields[outputRegisterIndex].Extract(data) };
                    break;
                case RegisterTarget.Inputs:
                    ret = new long[fields.Length - 1];

                    //var retList = new List<long>();
                    if (fields.Length <= 1)
                    {
                        return null;
                    }
                    for (int i = 1; i < fields.Length; i++)
                    {
                        ret[i - 1] = fields[i].Extract(data);
                        //retList.Add(FFEs[i].Extract(data, sub));
                    }
                    //ret = retList.ToArray();
                    break;
                default:
                    break;
            }
           
            return ret;
        }

        public FieldInfo GetFieldByName(string name)
        {
            if(fieldNameDict.TryGetValue(name, out FieldInfo value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public List<FieldInfo> GetFieldsByTag(string tag)
        {
            if (fieldTagDict.TryGetValue(tag, out List<FieldInfo> fieldsWithTag))
            {
                return fieldsWithTag;
            }
            else
            {
                return null;
            }
        }

        public FieldInfo this[string name]
        {
            // get and set accessors
            get
            {
                if (fieldNameDict.TryGetValue(name, out FieldInfo value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
        }

    }

}
