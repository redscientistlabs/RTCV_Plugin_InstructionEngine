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
using InstructionEngine.Data.CorruptCooking;

namespace InstructionEngine.Engines
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    partial class InstrEngine : ICorruptionEngine
    {
        [Exclude]
        static Random rand = new Random();

        bool ICorruptionEngine.SupportsCustomPrecision => false;
        bool ICorruptionEngine.SupportsAutoCorrupt => false;
        bool ICorruptionEngine.SupportsGeneralParameters => true;
        bool ICorruptionEngine.SupportsMemoryDomains => true;


        [Exclude]
        Form ICorruptionEngine.Control { get { return control; } }

        [Exclude]
        Form control { get; set; } = null;

        public InstrEngine() { }
        public InstrEngine(Form form) { control = form; }

        public static string LimiterListHash
        {
            get { return EngineSpec.Get<string>(nameof(LimiterListHash)); }
            set { EngineSpec.Set(nameof(LimiterListHash), value); }
        }

        public static string ValueListHash
        {
            get { return EngineSpec.Get<string>(nameof(ValueListHash)); }
            set { EngineSpec.Set(nameof(ValueListHash), value); }
        }

        public static EngineMethod Method
        {
            get { return EngineSpec.Get<EngineMethod>(nameof(Method)); }
            set { EngineSpec.Set(nameof(Method), value); }
        }

        public static List<InstructionDef> FilterInstructions
        {
            get { return EngineSpec.Get<List<InstructionDef>>(nameof(FilterInstructions)); }
            set { EngineSpec.Set(nameof(FilterInstructions), value); }
        }

        public static bool Subtract
        {
            get { return EngineSpec.Get<bool>(nameof(Subtract)); }
            set { EngineSpec.Set(nameof(Subtract), value); }
        }

        public static IngredientList ChefParams
        {
            get { return EngineSpec.Get<IngredientList>(nameof(ChefParams)); }
            set { EngineSpec.Set(nameof(ChefParams), value); }
        }


        private static InstructionDef ContainsValue(List<InstructionDef> entries, byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            try
            {
                long data = BytesTolong(bytes); //Convert bytes to long
                foreach (var e in entries)
                {
                    if (e.Matches(data))
                    {
                        return e;// FormFactors.GetFormFactor(e.FormFactor);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _ = ex;
                return null;
            }
        }

        public static long BytesTolong(byte[] byteRef)
        {
            var bytes = (byte[])byteRef.Clone();
            //Fun switch of fun, but is faster for most paths than resizing to 8 bytes
            switch (bytes.Length)
            {
                case 1:
                    return (long)bytes[0];
                case 2:
                    return (long)BitConverter.ToUInt16(bytes, 0);
                case 3:
                    byte[] new3byte = new byte[8];
                    Array.Copy(byteRef, 0, new3byte, 5, 3);
                    //bytes.FlipBytes();
                    //Array.Resize(ref bytes, 4);
                    //bytes.FlipBytes();
                    return BitConverter.ToInt64(new3byte, 0);
                case 4:
                    return (long)BitConverter.ToUInt32(bytes, 0);
                case 5:
                case 6:
                case 7:
                    byte[] new7byte = new byte[8];
                    Array.Copy(byteRef, 0, new7byte, 1, 7);
                    //bytes.FlipBytes();
                    //Array.Resize(ref bytes, 8);
                    //bytes.FlipBytes();
                    return BitConverter.ToInt64(new7byte, 0);
                case 8:
                    return BitConverter.ToInt64(bytes, 0);
                default:
                    throw new Exception("Invalid byte count in BitLogicListFilter. Limiter must be less than 64 bits (8 bytes)");
            }
        }

        public static TargetData PeekAndGetData(List<InstructionDef> entries, MemoryInterface mi, long startAddress, long endAddress)
        {
            if (mi == null)
            {
                throw new ArgumentNullException(nameof(mi));
            }

            //If we go outside of the domain, just return false
            if (endAddress > mi.Size)
            {
                return null;
            }

            //Find the precision
            long precision = endAddress - startAddress;
            byte[] values = new byte[precision];

            //Peek the memory
            for (long i = 0; i < precision; i++)
            {
                values[i] = mi.PeekByte(startAddress + i);
            }

            //The compare is done as little endian
            if (mi.BigEndian)
            {
                values = values.FlipBytes();
            }

            //If the limiter contains the value we peeked, return true
            var instr =  ContainsValue(entries, values);


            if (instr != null)
            {
                return new TargetData(instr, BytesTolong(values), values, mi, startAddress);
            }

             return null;
        }


        BlastLayer ICorruptionEngine.GetBlastLayer(long intensity)
        {
            var domains = (string[])AllSpec.UISpec[UISPEC.SELECTEDDOMAINS];
            if (domains == null || domains.Length == 0)
            {
                MessageBox.Show("Can't corrupt with no domains selected.");
                return null;
            }

            int precision = 4;

            List<BlastUnit> blastUnits = new List<BlastUnit>();
            var filter = FilterInstructions;
            var filter2 = BleedFilterInstructions;
            int bleedBack = BleedBackwards;
            int bleedForward = BleedForwards;
            var method = Method;
            bool smart = Smart;
            bool unique = UseUniqueRegisters;
            bool exclude = ExcludeMatchedRegs;
            backTarget = BackTarget;
            forwardTarget = ForwardTarget;
            backResTarget = BackResTarget;
            forwardResTarget = ForwardResTarget;
            //bool insertShuffle = InsertEngine.Shuffle;

            //var recipe = CookingClass.MakeRecipes(new List<IngredientList>()
            //{
            //    new IngredientList("Test Ingredient List", FilterInstructions, new List<Ingredient>()
            //    { 
            //        new Ingredient(nameof(Chef.FindAround), bleedBack.ToString(), bleedForward.ToString(), IngredientList.MAIN, IngredientList.MAIN),
            //        new Ingredient(nameof(Chef.AddressWarp),"BD","BD","2","1")
            //    })
            //})[0];
            Recipe recipe = null;
            switch (method)
            {
                case EngineMethod.Bleed:
                    break;

                case EngineMethod.Chef:
                    recipe = CookingClass.MakeRecipes(new List<IngredientList>() { ChefParams })[0];
                    break;
                default:
                    break;
            }

            

            for (int i = 0; i < intensity; i++)
            {
                var domain = domains[rand.Next(domains.Length)];
                MemoryInterface mi = MemoryDomains.GetInterface(domain);
                if (mi == null)
                {
                    return null;
                }
                var address = rand.Next((int)mi.Size - 1);

                long safeAddress = address - (address % precision); //32-bit trunk

                if (safeAddress >= mi.Size - precision*4)
                {
                    safeAddress = mi.Size - (5 * precision); //If we're out of range, hit the last aligned address
                }

                byte[] matchBytes = null;
                switch (method)
                {
                    case EngineMethod.Bleed:
                        try
                        {
                            matchBytes = BleedCorrupt(filter, filter2, mi, safeAddress, precision, bleedBack, bleedForward, smart, unique, exclude);
                        }
                        catch (Exception ex)
                        {
                            SyncObjectSingleton.FormExecute(() =>
                            {
                                MessageBox.Show(ex.ToString());
                            });
                            throw ex;
                        }
                        break;
                    case EngineMethod.ReggieRotate:
                        var data = PeekAndGetData(filter, mi, address, address + precision);
                        if (data != null)
                        {
                            var data2 = data.Data;

                            data2 = data.FormFactor.Inject(data2, RegisterTarget.All, data.FormFactor.ExtractAll(data2), true);


                            byte[] outValue = BitConverter.GetBytes(data2);

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
                            matchBytes = outValue;
                        }
                        break;
                    case EngineMethod.Chef:
                        matchBytes = recipe?.Cook(mi, address, precision);
                        //matchBytes = ChefEngine.Cook(filter, mi, safeAddress, precision);
                        //matchBytes = recipe.Cook(mi, safeAddress, precision);
                        break;
                    case EngineMethod.Inserter:
                        matchBytes = InsertEngine.Corrupt(mi, address, precision);
                        break;
                    default:
                        break;
                }

                if (matchBytes != null)
                {
                    blastUnits.Add(new BlastUnit(matchBytes, domain, safeAddress, precision,
                        mi.BigEndian, 0, 1, null, true, false, true));
                }

            }
            return blastUnits.Count > 0? new BlastLayer(blastUnits) : null;
        }




        string ICorruptionEngine.ToString() => "Misassembly Engine";

        void ICorruptionEngine.OnSelect()
        {
            //do nothing
        }

        void ICorruptionEngine.OnDeselect()
        {
            //resync other engines
            //S.GET<CorruptionEngineForm>().ResyncAllEngines();
        }


    }
}
