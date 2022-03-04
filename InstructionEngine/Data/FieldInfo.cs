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
        private long fieldMask;
        private long fieldMaskRAlign;
        private long inverseRegisterMask;
        public HashSet<string> Tags { get; private set; } = new HashSet<string>();
        public bool Signed { get; private set; }
        //const long SIGN_BIT = 0b1ul << 63;

        public int BitsTotal { get; private set; }
        public FieldInfo() { }

        public FieldInfo(string name, long registerMask, string[] tags)
        {
            Name = name;
            this.fieldMask = registerMask;
            inverseRegisterMask = ~registerMask;
            rAlignShift = CountBitsRight(registerMask);
            fieldMaskRAlign = fieldMask >> rAlignShift;
            BitsTotal = CountBitsTotal(rAlignShift, fieldMask);

            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    this.Tags.Add(tag);
                }
            }
            Signed = this.Tags.Contains("Signed");
        }

        ////dat = SIGN_BIT | (dat & (~(0b1ul << BitsTotal))); dat = (dat & (~(0b1ul << BitsTotal)));
        //AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA

        public long ExtractSignedValue(long instruction, int appendBits = 2) //shift correction is because some fields are appended with extra empty bits on the right
        {
            long extractedField = ((instruction & fieldMask) >> (rAlignShift-appendBits)); //Extract field, shift to align right, apply appended bits
            int signBitPos = (BitsTotal - 1)+appendBits; //Position of sign bit after shifting. BitsTotal is the width of the field in bits
            long signBit = (0b1 << signBitPos) & extractedField; //Extracts sign bit.

            //From here, I'm not sure.

            if (signBit > 0) //If negative
            {

                //List<string> final = new List<string>();
                //string sbp = Convert.ToString((long)signBitPos, 2);
                //string sb = Convert.ToString((long)signBit, 2);
                //string instr = Convert.ToString((long)instruction, 2);
                //string extr = Convert.ToString((long)extractedField, 2);
                //string extr2 = Convert.ToString(extractedField | ((~fieldMaskRAlign) << appendBits), 2);
                //final.Add(sbp);
                //final.Add(sb);
                //final.Add(instr);
                //final.Add(extr);
                //final.Add(extr2);
                return extractedField | ((~fieldMaskRAlign) << appendBits); //Fill in bits on the left. 
            }
            else
            {
                return extractedField;
            }
        }

        public long InjectSigned(long dataOut, long fieldValue, int appendBits = 2)
        {
            //List<string> final = new List<string>();
            //string datO = Convert.ToString((long)dataOut, 2);
            //string fv = Convert.ToString((long)fieldValue, 2);
            //string fv2 = Convert.ToString((long)(fieldValue >> appendBits), 2);
            //string fv3 = Convert.ToString((long)fieldMaskRAlign & (fieldValue >> appendBits), 2);

            //final.Add(datO);
            //final.Add(fv);
            //final.Add(fv2);
            //final.Add(fv3);

            return Inject(dataOut, fieldMaskRAlign & (fieldValue >> appendBits));
        }

        /// <summary>
        /// Assumes same register size
        /// </summary>
        //public long InjectSignedValue(long dataOut, long registerValue)
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


        private static int CountBitsRight(long rMask)
        {
            int shift = 0;
            long mask = 0b1;
            for (int i = 0; i < 64; i++)
            {
                if ((rMask & mask) > 0)
                {
                    return shift;
                }
                else
                {
                    mask <<= 1;
                    shift++; //oops
                }
            }
            //No match
            return 64;
        }

        private static int CountBitsTotal(int shift, long rMask)
        {
            long mask = 0b1L << shift;
            int bitsTotal = 0;
            for (int i = shift; i < 64; i++)
            {
                if ((rMask & mask) > 0)
                {
                    bitsTotal++;
                }
                mask <<= 1;             
            }

            return bitsTotal;
        }

        public bool HasTag(string tag)
        {
            return Tags.Contains(tag);
        }

        public long Extract(long other)
        {
            return ((other & fieldMask) >> rAlignShift);
        }

        public long Inject(long data, long registerData)
        {
            return (data & inverseRegisterMask) | (registerData << rAlignShift);
        }

        public long InjectMasked(long data, long registerData)
        {
            return (data & inverseRegisterMask) | ((registerData << rAlignShift) & fieldMask);
        }
    }
}
