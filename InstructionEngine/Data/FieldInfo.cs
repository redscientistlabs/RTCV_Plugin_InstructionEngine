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
        private int rAlignShift;
        private ulong fieldMask;
        private ulong inverseRegisterMask;
        private HashSet<string> tags = new HashSet<string>();
        public bool Signed { get; private set; }
        //const ulong SIGN_BIT = 0b1ul << 63;

        public int BitsTotal { get; private set; }
        public FieldInfo() { }

        public FieldInfo(string name, ulong registerMask, string[] tags)
        {
            Name = name;
            this.fieldMask = registerMask;
            inverseRegisterMask = ~registerMask;
            rAlignShift = CountBitsRight(registerMask);
            BitsTotal = CountBitsTotal(rAlignShift, registerMask);

            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    this.tags.Add(tag);
                }
            }
            Signed = this.tags.Contains("Signed");
        }

        ////dat = SIGN_BIT | (dat & (~(0b1ul << BitsTotal))); dat = (dat & (~(0b1ul << BitsTotal)));
        //AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA

        public long ExtractSignedValue(ulong instruction, int appendBits = 2) //shift correction is because some fields are appended with extra empty bits on the right
        {
            ulong extractedField = ((instruction & fieldMask) >> rAlignShift) << appendBits; //Extract field, shift to align right, apply appended bits
            int signBitPos = (BitsTotal - 1)+appendBits; //Position of sign bit after shifting
            ulong signBit = (0b1ul << signBitPos) & extractedField; //Extracts sign bit. BitsTotal is the width of the field in bits

            //From here, I'm not sure.

            if (signBit > 0) //If negative
            {
                ulong valMask = ulong.MaxValue >> (63 - signBitPos); //Mask out new value
                return unchecked((long)(extractedField | (~valMask))); //Fill in bits on the left. 
            }
            else
            {
                return unchecked((long)extractedField);
            }
        }
        public ulong InjectSignedValue(ulong dataOut, ulong registerValue)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Assumes same register size
        /// </summary>
        //public ulong InjectSignedValue(ulong dataOut, ulong registerValue)
        //{
        //    bool negative = (SIGN_BIT & registerValue) > 0;
        //    if (negative)
        //    {
        //        registerValue = ~registerValue & (fieldMask >> rAlignShift); //Two's compliment
        //        registerValue = ((0b1ul << BitsTotal) | registerValue) & ~SIGN_BIT; // put the bit back
        //    }

        //    //return (workingData & inverseFieldMask) | (registerData << alignShift) //Inject
        //    return Inject(dataOut, registerValue);
        //}


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
            return ((other & fieldMask) >> rAlignShift);
        }

        public ulong Inject(ulong data, ulong registerData)
        {
            return (data & inverseRegisterMask) | (registerData << rAlignShift);
        }

        public ulong InjectMasked(ulong data, ulong registerData)
        {
            return (data & inverseRegisterMask) | ((registerData << rAlignShift) & fieldMask);
        }
    }
}
