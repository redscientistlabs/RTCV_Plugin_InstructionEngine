using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Data.Json
{
    public class FormFactorFieldConverter : JsonConverter
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;
        public override bool CanConvert(Type type) => type == typeof(long) || type == typeof(HashSet<string>);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
            //try
            //{
            //    if (value is long)
            //    {
            //        long number = (long)value;
            //        string numRep = Convert.ToString((long)number, 2);
            //        if (numRep.Length <= 32) numRep = numRep.PadLeft(32, '0');
            //        else { numRep = numRep.PadLeft(64, '0'); }
            //        writer.WriteValue("0b" + numRep);
            //    }
            //    else
            //    {
            //        writer.WriteValue((string)value);
            //    }
            //}
            //catch
            //{
            //    writer.WriteValue((string)value);
            //}
        }

        public override object ReadJson(JsonReader reader, Type type, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Null)
            {
                if (reader.TokenType == JsonToken.StartArray)
                {
                    JToken token = JToken.Load(reader);
                    //if(token.)
                    List<string> items = token.ToObject<List<string>>();
                    try
                    {
                        return items.Select(x => Convert.ToInt64(x.Replace("_", "").Substring(2), 2)).ToArray();
                    }
                    catch(FormatException)
                    {
                        HashSet<string> hs = new HashSet<string>();
                        foreach (var item in items)
                        {
                            hs.Add(item);
                        }
                        return hs;
                    }
                }
                else
                {
                    JValue jValue = new JValue(reader.Value);
                    switch (reader.TokenType)
                    {
                        case JsonToken.String:
                            try
                            {
                                return Convert.ToInt64(((string)jValue).Replace("_","").Substring(2), 2);
                            }
                            catch
                            {
                                return (string)jValue;
                            }
                        default:
                            Console.WriteLine("Default case");
                            Console.WriteLine(reader.TokenType.ToString());
                            break;
                    }
                }
            }
            return "What";
        }
        
    }
}
