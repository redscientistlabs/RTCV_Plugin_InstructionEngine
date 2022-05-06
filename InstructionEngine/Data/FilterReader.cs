using Ceras;
using RTCV.CorruptCore;
using RTCV.CorruptCore.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InstructionEngine.Engines;
using InstructionEngine.Data;

namespace InstructionEngine.Data
{ 
    internal static class FilterReader
    {
        //Could be moved to a common location
        const char CHAR_WILD = '?';
        const char CHAR_PASS = '#';
        const char CHAR_FLAG = '@';

        
        //private HashSet<long> usedRegisters = new HashSet<long>();

        //private int searchback = 8;

        //private static Regex ffRegex = new Regex(@"\(([a-zA-Z0-9_]+)\)(.*)");
        private static Regex ffRegex = new Regex(@"\(([a-zA-Z0-9_ ]+),([a-zA-Z0-9_]+)\)\s*(.*)");

        public static List<InstructionDef> ReadEntries(string filePath)
        {
            try
            {
                string[] dataLines = File.ReadAllLines(filePath);

                bool inHeader = true;
                bool doFlipBytes = Path.GetFileName(filePath).StartsWith("_");

                if (dataLines == null)
                {
                    throw new ArgumentNullException(nameof(dataLines));
                }
                List<InstructionDef> entries = new List<InstructionDef>();


                for (int j = 0; j < dataLines.Length; j++)
                {
                    if (inHeader && ((!string.IsNullOrWhiteSpace(dataLines[j])) && dataLines[j].Length > 1 && dataLines[j][0] == CHAR_FLAG))
                    {
                        string flagOrig = dataLines[j];
                        string flag = dataLines[j].Substring(1).Trim().ToLower();

                        if (flag == "v1.0")
                        {
                            doFlipBytes = true;
                        }
                    }
                    else
                    {
                        if (inHeader) { inHeader = false; }
                        var e = ParseLine(j + 2, filePath, dataLines[j], doFlipBytes); //Parse lines individually
                        if (e != null)
                        {
                            entries.Add(e);
                        }
                    }
                }

                if (entries.Count == 0)
                {
                    throw new Exception($"Error reading list {Path.GetFileName(filePath)}, list was empty or contained no valid lines"); //show message to user
                }

                return entries;
            }
            catch (Exception e)
            {
                _ = e;
                return null;
            }
        }

        //Valid chars for lines (not including prefix)
        private static char[] validCharListHex = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', CHAR_WILD, CHAR_PASS };
        private static char[] validCharListBinary = new char[] { '0', '1', CHAR_WILD, CHAR_PASS };

        private static InstructionDef ParseLine(int lineNum, string filePath, string line, bool doFlipBytes)
        {
            bool flipBytes = !doFlipBytes; //to maintain sanity

            line = line.Trim(); //remove whitespace on both sides
            string originalLine = line;

            //Ignore empty lines and comments
            if (string.IsNullOrWhiteSpace(line) || (line.Length > 2 && (line.Substring(0, 2) == "//")))
            {
                return null; //do not throw error
            }


            line = line.ToUpper(); //All alphabetical characters to upper case for easier handling

            var match = ffRegex.Match(line);
            if (!match.Success) return null;
            string name = match.Groups[1].Value;
            string formFactor = match.Groups[2].Value;

            line = match.Groups[3].Value.Trim();



            bool isHex = true; //Line defaults to hex

            //Check prefix
            if (line.Length > 2)
            {
                string prefix = line.Substring(0, 2);
                if (prefix == "0B")
                {
                    line = line.Substring(2); //remove 0b
                    isHex = false; //Set type to binary
                }
                else if (prefix == "0X")
                {
                    line = line.Substring(2); //Remove 0x
                    //Hex is default, don't need to set
                }
            }

            //Check for invalid characters
            char[] validChars = isHex ? validCharListHex : validCharListBinary; //Get ref to correct valid char list
            foreach (char c in line)
            {
                if (!validChars.Contains(c))
                {
                    //return null;
                    throw new Exception($"Error reading list {Path.GetFileName(filePath)} (Line {lineNum}), line contains invalid character"); //Warn user about invalid characters
                }
            }

            //Check for line sizes that may be too big
            //Note: May have to move and rework this depending on future formats
            if ((!isHex && line.Length > 64) || (isHex && line.Length > 16))
            {
                throw new Exception($"Error reading list {Path.GetFileName(filePath)} (Line {lineNum}), total number of bits must be 64 or less (8 bytes)"); //Warn user about line size too big
            }

            //Discard non-byte divisible lines
            if ((!isHex && (line.Length % 8 != 0)) || (isHex && (line.Length % 2 != 0)))
            {
                throw new Exception($"Error reading list {Path.GetFileName(filePath)} (Line {lineNum}), lines must be byte sized"); //Warn user about line not byte sized
            }


            //Flip bytes, simulating manual flipping
            if (flipBytes)
            {
                if (isHex)
                {
                    line = FlipBytesStr(line, 2);
                }
                else
                {
                    line = FlipBytesStr(line, 8);
                }
            }

            //=========================== At this point it *should* be valid ===========================

            //Parse with the correct method
            if (isHex)
            {
                var ret = ParseHex(line, formFactor, name);
                return ret;
            }
            else
            {
                var ret = ParseBin(line, formFactor, name);
                return ret;
            }
        }


        //assumes s.Length is evenly divisible by chunksize, should be the case always
        private static string FlipBytesStr(string s, int chunkSize)
        {
            //StringBuilder sb = new StringBuilder();
            string res = "";
            int div = s.Length / chunkSize;
            for (int j = div - 1; j >= 0; j--)
            {
                res += s.Substring(j * chunkSize, chunkSize);
            }
            return res;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)] //Inline hint (does it do anything here? idk)
        private static long CharTolongHex(char c)
        {
            //Ascii format
            int i = (int)c;
            return (long)(i - ((i <= 57) ? 48 : 55)); //optimized, values are guaranteed
        }

        ////Gets precision of a line, uncomment if non-byte sized lists become a thing
        //private int GetPrecision(string s, int incr = 1)
        //{
        //    int ret = s.Length * incr;

        //    if (ret % 8 > 0)
        //    {
        //        return (ret / 8) + 1;
        //    }
        //    else
        //    {
        //        return ret / 8;
        //    }
        //}

        private static int GetPrecision(string s, int incr = 1)
        {
            return (s.Length * incr) / 8;
        }

        private static InstructionDef ParseHex(string line, string formFactor, string name)
        {
            //Could be refactored and merged with ParseBin probably
            const long digitMask = 0b1111; //long mask for one hex digit

            long template = 0L;

            long reserved = 0L;

            //At this point we know only valid characters are in the line

            //Fill in the fields
            int curLeftShift = 0; //Additional variable to maintain my sanity
            for (int j = line.Length - 1; j >= 0; j--)
            {
                if (line[j] == CHAR_WILD) { }
                else if (line[j] == CHAR_PASS) { }
                else //is a Constant
                {
                    //line[j] is guaranteed to be Hex characters here
                    template |= CharTolongHex(line[j]) << curLeftShift; //Convert char to long and shift
                    reserved |= digitMask << curLeftShift; //Also add to reserved mask
                }
                curLeftShift += 4; //add half byte shift
            }
            return new InstructionDef(name, formFactor, template, reserved, GetPrecision(line, 4));
        }

        private static InstructionDef ParseBin(string line, string formFactor, string name)
        {
            long template = 0;
            long reserved = 0;

            //At this point we know only valid characters are in the line

            //Fill in the fields
            int curLeftShift = 0; //Additional variable to maintain my sanity
            for (int j = line.Length - 1; j >= 0; j--)
            {
                if (line[j] == CHAR_WILD) { } //Wildcard
                else if (line[j] == CHAR_PASS) { } //Passthrough
                else //Constant
                {
                    //line[j] is guaranteed to be '1' or '0' here
                    template |= ((long)line[j] - 48) << curLeftShift; //Convert char to long and shift
                    reserved |= 1L << curLeftShift; //Also add to reserved mask
                }

                curLeftShift++;
            }
            return new InstructionDef(name, formFactor, template, reserved, GetPrecision(line, 1));
        }
    }

 
}
