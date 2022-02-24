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
    public class FieldInfo
    {
        public string Name { get; private set; }
        private int shift;
        private ulong registerMask;
        private ulong inverseRegisterMask;
        private HashSet<string> tags = new HashSet<string>();

        public int BitsTotal { get; private set; }
        public FieldInfo() { }

        public FieldInfo(string name, ulong registerMask, string[] tags)
        {
            Name = name;
            this.registerMask = registerMask;
            inverseRegisterMask = ~registerMask;
            shift = CountBitsRight(registerMask);
            BitsTotal = CountBitsTotal(shift, registerMask);

            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    this.tags.Add(tag);
                }
            }
        }
        private static int CountBitsRight(ulong rMask)
        {
            int shift = 0;
            ulong mask = 0b1;
            for (int i = 0; i < 64; i++)
            {
                if ((rMask & mask) > 0)
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

        private static int CountBitsTotal(int shift, ulong rMask)
        {
            ulong mask = 0b1UL << shift;
            int bitsTotal = 0;
            for (int i = shift; i < 64; i++)
            {
                if ((rMask & mask) > 0)
                {
                    bitsTotal++;
                }
                else
                {
                    mask <<= 1;
                }
            }

            return bitsTotal;
        }

        public bool HasTag(string tag)
        {
            return tags.Contains(tag);
        }

        public ulong Extract(ulong other)
        {
            return ((other & registerMask) >> shift);
        }

        public ulong Inject(ulong data, ulong registerData)
        {
            return (data & inverseRegisterMask) | (registerData << shift);
        }

        public ulong InjectMasked(ulong data, ulong registerData)
        {
            return (data & inverseRegisterMask) | ((registerData << shift) & registerMask);
        }
    }
}
